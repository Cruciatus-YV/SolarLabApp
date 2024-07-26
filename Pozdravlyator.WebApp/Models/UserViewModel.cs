using System.ComponentModel.DataAnnotations;

namespace Pozdravlyator.WebApp.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите имя")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Введите фамилию")]
        public string? LastName { get; set; }
        public string? Adress { get; set; }
        [Required(ErrorMessage = "Введите дату")]
        public string BirthdayString { get; set; }
        public DateTime Birthday { get; set; }
        public int GetAge()
        {
            return DateTime.UtcNow.Year - Birthday.Year;
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
