using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrandsA.Domain.Entities.Common;

namespace BrandsA.Domain.Entities
{
    public class Token : BaseEntity
    {
        public Guid UserId { get; set; } // Foreign key to User
        public User User { get; set; } // Navigation property
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
