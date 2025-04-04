using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrandsA.Domain.Entities.Common;

namespace BrandsA.Domain.Entities
{
    public class User : BaseEntity
    {

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string NameSurname { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public List<UserRole?>? Roles { get; set; } // Navigation property for roles

    }
}