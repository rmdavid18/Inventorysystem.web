using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenielDavidInventorySystem.Infrastructure.Domain.Models
{
    public class User
    {
        public Guid? UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public Enums.Sex Sex { get; set; }
        public Enums.Role Role { get; set; }
    }
}
