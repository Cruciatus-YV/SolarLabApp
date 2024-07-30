using Pozdravlyator.Domain;

namespace Pozdravlyator.WebApp.Models
{
    public class MonthWithUsersViewModel
    {
        public string Month { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public List<UserViewModel> Users { get; set; }
    }
}
