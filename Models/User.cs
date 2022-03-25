using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [Column(TypeName = "Date")]
        public DateTime RegisterDate { get; set; }
        [Column(TypeName = "Date")]
        public DateTime LastLoginDate { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
    }
}
