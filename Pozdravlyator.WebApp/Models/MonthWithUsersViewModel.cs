using Pozdravlyator.Domain;

namespace Pozdravlyator.WebApp.Models
{
    public class MonthWithUsersViewModel
    {
        public string Month { get; set; }
        public string Color { get; set; }
        public List<User> Users { get; set; }
    }
}
