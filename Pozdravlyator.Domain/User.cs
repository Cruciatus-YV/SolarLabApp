using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace Pozdravlyator.Domain
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Adress { get; set; }
        public DateTime Birthday { get; set; }
        public string? Avatar { get; set; }
        public string? AvatarExtention { get; set; }
    }
}
