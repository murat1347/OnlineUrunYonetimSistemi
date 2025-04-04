using BrandsA.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandsA.Domain.Entities
{
    public class UserRole : BaseEntity
    {
        public int RoleNumber { get; set; }
        public string RoleName { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
