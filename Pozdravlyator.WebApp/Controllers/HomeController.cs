using Microsoft.AspNetCore.Mvc;
using Pozdravlyator.Domain;
using Pozdravlyator.WebApp.Models;
using Pozdravlyator.WebApp.Repository.Interfaces;
using System.Diagnostics;
using System.Globalization;

namespace Pozdravlyator.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository _userRepository;

        public HomeController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            var users = _userRepository.GetAll();
           
            var result = users.GroupBy(x => x.Birthday.Month).Select(x => 
            {
                var (color, description) = GetColorAndDescription(x.Key);
                return new MonthWithUsersViewModel
                {
                    Month = GetMonthName(x.Key),
                    Color = color,
                    Description = description,
                    Users = x.Select(user => new UserViewModel
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        BirthdayString = user.Birthday.ToString("dd.MM.yyyy г."),
                        Adress = user.Adress,
                        Birthday = user.Birthday.Date,
                        AvatarBase64 = user.Avatar,
                        AvatarExtention = user.AvatarExtention,
                    }).ToList(),
                };
            }).ToList();
            return View(result);
        }

        [HttpGet]
        public IActionResult Edit(int id) 
        {
            var user = _userRepository.GetById(id);
            var model = new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthdayString = user.Birthday.ToString("yyyy-MM-dd"),
                Adress = user.Adress,
                HasAvatar = !string.IsNullOrEmpty(user.Avatar),
                AvatarBase64 = user.Avatar,
                AvatarExtention = user.AvatarExtention,
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(UserViewModel model) 
        {
            var user = _userRepository.GetById(model.Id.GetValueOrDefault());
            var isBirthdayCorrect = DateTime.TryParseExact(model.BirthdayString, "yyyy-MM-dd", null, DateTimeStyles.None, out var birthday);
            
            if (!isBirthdayCorrect)
            {
                ModelState.First(x => x.Key == nameof(model.BirthdayString)).Value.Errors.Add("Неверный формат даты!");
            }

            ModelState.Remove("Avatar");

            if (!ModelState.IsValid) 
                return View(model);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Birthday = birthday;
            user.Adress = model.Adress;

            if (model.HasAvatar)
            {
                var file = Request.Form.Files["Avatar"];

                if (file != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        var fileBytes = memoryStream.ToArray();
                        user.AvatarExtention = Path.GetExtension(file.FileName).Replace(".", "");
                        user.Avatar = Convert.ToBase64String(fileBytes);
                    }
                }
            }
            else
            {
                user.AvatarExtention = null;
                user.Avatar = null;
            }

            _userRepository.Update(user);
            return Redirect("/");
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(UserViewModel model)
        {
            var isBirthdayCorrect = DateTime.TryParseExact(model.BirthdayString, "yyyy-MM-dd", null, DateTimeStyles.None, out var birthday);

            if (!isBirthdayCorrect)
            {
                ModelState.First(x => x.Key == nameof(model.BirthdayString)).Value.Errors.Add("Неверный формат даты!");
            }

            ModelState.Remove("Avatar");

            if (!ModelState.IsValid)
                return View(model);

            var newUser = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Adress = model.Adress,
                Birthday = birthday,

            };

            var file = Request.Form.Files["Avatar"];

            if (file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    var fileBytes = memoryStream.ToArray();
                    newUser.AvatarExtention = Path.GetExtension(file.FileName).Replace(".", "");
                    newUser.Avatar = Convert.ToBase64String(fileBytes);
                }
            }
            _userRepository.Create(newUser);
            return Redirect("/");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _userRepository.Delete(id);
            return Redirect("/");
        }

        private (string, string) GetColorAndDescription(int month)
        {
            var currentMonth = DateTime.UtcNow.Month;
            if (month < currentMonth) return ("#f00", "Прошедшие дни рождения");
            else if (month == currentMonth) return ("#1ce3ad", "Текущий месяц");
            else if (month == currentMonth + 1) return ("#c867cd", "Следующий месяц");
            else return ("#979797", "");
        }

        private string GetMonthName(int month)
        {
            return month == 1 ? "Январь"
                : month == 2 ? "Февраль"
                : month == 3 ? "Март"
                : month == 4 ? "Апрель"
                : month == 5 ? "Май"
                : month == 6 ? "Июнь"
                : month == 7 ? "Июль"
                : month == 8 ? "Август"
                : month == 9 ? "Сентябрь"
                : month == 10 ? "Октябрь"
                : month == 11 ? "Ноябрь"
                : "Декабрь";
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
