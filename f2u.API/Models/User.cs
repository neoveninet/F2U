using System.ComponentModel.DataAnnotations;

namespace f2u.API.Models
{
    public class User
    {
        public string UserName { get; set; }

        [Key]
        public int Id { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
    }
}