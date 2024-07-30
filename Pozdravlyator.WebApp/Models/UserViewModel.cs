using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Pozdravlyator.WebApp.Models
{
    public class UserViewModel
    {
        private static Dictionary<int, string> ageStringDict = new Dictionary<int, string>()
        {
            { 1, "год" },
            { 21, "год" },
            { 31, "год" },
            { 41, "год" },
            { 51, "год" },
            { 61, "год" },
            { 71, "год" },
            { 81, "год" },
            { 91, "год" },
            { 101, "год" },
            { 121, "год" },
            { 131, "год" },
            { 5, "лет" },
            { 6, "лет" },
            { 7, "лет" },
            { 8, "лет" },
            { 9, "лет" },
            { 10, "лет" },
            { 11, "лет" },
            { 12, "лет" },
            { 13, "лет" },
            { 14, "лет" },
            { 15, "лет" },
            { 16, "лет" },
            { 17, "лет" },
            { 18, "лет" },
            { 19, "лет" },
            { 20, "лет" },
            { 25, "лет" },
            { 26, "лет" },
            { 27, "лет" },
            { 28, "лет" },
            { 29, "лет" },
            { 30, "лет" },
            { 35, "лет" },
            { 36, "лет" },
            { 37, "лет" },
            { 38, "лет" },
            { 39, "лет" },
            { 40, "лет" },
            { 45, "лет" },
            { 46, "лет" },
            { 47, "лет" },
            { 48, "лет" },
            { 49, "лет" },
            { 50, "лет" },
            { 55, "лет" },
            { 56, "лет" },
            { 57, "лет" },
            { 58, "лет" },
            { 59, "лет" },
            { 60, "лет" },
            { 65, "лет" },
            { 66, "лет" },
            { 67, "лет" },
            { 68, "лет" },
            { 69, "лет" },
            { 70, "лет" },
            { 75, "лет" },
            { 76, "лет" },
            { 77, "лет" },
            { 78, "лет" },
            { 79, "лет" },
            { 80, "лет" },
            { 85, "лет" },
            { 86, "лет" },
            { 87, "лет" },
            { 88, "лет" },
            { 89, "лет" },
            { 90, "лет" },
            { 95, "лет" },
            { 96, "лет" },
            { 97, "лет" },
            { 98, "лет" },
            { 99, "лет" },
            { 100, "лет" },
            { 105, "лет" },
            { 106, "лет" },
            { 107, "лет" },
            { 108, "лет" },
            { 109, "лет" },
            { 110, "лет" },
            { 111, "лет" },
            { 112, "лет" },
            { 113, "лет" },
            { 114, "лет" },
            { 115, "лет" },
            { 116, "лет" },
            { 117, "лет" },
            { 118, "лет" },
            { 119, "лет" },
            { 120, "лет" },
            { 125, "лет" },
            { 126, "лет" },
            { 127, "лет" },
            { 128, "лет" },
            { 129, "лет" },
            { 130, "лет" },
            { 2, "года" },
            { 3, "года" },
            { 4, "года" },
            { 22, "года" },
            { 23, "года" },
            { 24, "года" },
            { 32, "года" },
            { 33, "года" },
            { 34, "года" },
            { 42, "года" },
            { 43, "года" },
            { 44, "года" },
            { 52, "года" },
            { 53, "года" },
            { 54, "года" },
            { 62, "года" },
            { 63, "года" },
            { 64, "года" },
            { 72, "года" },
            { 73, "года" },
            { 74, "года" },
            { 82, "года" },
            { 83, "года" },
            { 84, "года" },
            { 92, "года" },
            { 93, "года" },
            { 94, "года" },
            { 102, "года" },
            { 103, "года" },
            { 104, "года" },
            { 122, "года" },
            { 123, "года" },
            { 124, "года" },
        };
        public int? Id { get; set; }
        [Required(ErrorMessage = "Введите имя!")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Введите фамилию!")]
        public string? LastName { get; set; }
        public string? Adress { get; set; }
        [Required(ErrorMessage = "Введите дату!")]
        public string BirthdayString { get; set; }
        public DateTime Birthday { get; set; }
        public IFormFile Avatar {  get; set; }
        public string? AvatarBase64 { get; set; }
        public bool HasAvatar { get; set; }
        public string? AvatarExtention { get; set; }

        public string GetAge()
        {
            DateTime now = DateTime.Today;
            int age = now.Year - Birthday.Year;
            if (Birthday > now.AddYears(-age)) 
                age--;
            var ageStr = ageStringDict.ContainsKey(age) ? ageStringDict[age] : "лет";
            return $"{age} {ageStr}";
        }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        public bool IsBirthdayToday()
        {
            return Birthday.Month == DateTime.UtcNow.Month && Birthday.Day == DateTime.UtcNow.Day;
        }
    }
}
