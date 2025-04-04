using BrandsA.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandsA.Application.IRepositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
    }
}
