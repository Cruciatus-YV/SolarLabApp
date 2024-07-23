using Microsoft.AspNetCore.Mvc;
using Pozdravlyator.Domain;
using Pozdravlyator.WebApp.Models;
using Pozdravlyator.WebApp.Repository.Interfaces;
using System.Diagnostics;

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
            if (!users.Any()) 
            {

                _userRepository.Create(new User 
                {
                    FirstName = "����",
                    LastName = "��������",
                    Birthday = new DateTime(1998,9,1),
                    Adress = "�.��������",
                });
                _userRepository.Create(new User
                {
                    FirstName = "����",
                    LastName = "�������",
                    Birthday = new DateTime(2000, 1, 14),
                    Adress = "�.����",
                });
                _userRepository.Create(new User
                {
                    FirstName = "������",
                    LastName = "������",
                    Birthday = new DateTime(1998, 7, 11),
                    Adress = "�.��������",
                });
                _userRepository.Create(new User
                {
                    FirstName = "����",
                    LastName = "�������",
                    Birthday = new DateTime(1985, 8, 16),
                    Adress = "�.��������",
                });
                users = _userRepository.GetAll();
            }
            if (!users.Any(x=>x.Birthday.Month == DateTime.UtcNow.Month && x.Birthday.Day == DateTime.UtcNow.Day))
            {
                _userRepository.Create(new User
                {
                    FirstName = Guid.NewGuid().ToString(),
                    LastName = Guid.NewGuid().ToString(),
                    Birthday = DateTime.UtcNow.Date,
                    Adress = Guid.NewGuid().ToString(),
                });
                users = _userRepository.GetAll();
            }
            var result = users.GroupBy(x => x.Birthday.Month).Select(x => 
            {
                return new MonthWithUsersViewModel
                {
                    Month = GetMonthName(x.Key),
                    Color = GetColor(x.Key),
                    Users = x.ToList(),
                };
            }).ToList();
            return View(result);
        }
        public IActionResult Edit(int id) 
        {
            var user = _userRepository.GetById(id);
            return View(user);
        }
        private string GetColor(int month)
        {
            var currentMonth = DateTime.UtcNow.Month;
            if (month < currentMonth) return "#f00";
            else if (month == currentMonth) return "#54e31c";
            else if (month == currentMonth + 1) return "#c867cd";
            else return "#979797";
        }
        private string GetMonthName(int month)
        {
            return month == 1 ? "������"
                : month == 2 ? "�������"
                : month == 3 ? "����"
                : month == 4 ? "������"
                : month == 5 ? "���"
                : month == 6 ? "����"
                : month == 7 ? "����"
                : month == 8 ? "������"
                : month == 9 ? "��������"
                : month == 10 ? "�������"
                : month == 11 ? "������"
                : "�������";
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
