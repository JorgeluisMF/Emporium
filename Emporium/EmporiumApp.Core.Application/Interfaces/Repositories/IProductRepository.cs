using EmporiumApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Interfaces.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        List<Product> GetAll();
        Task<Product> GetByCodeAsync(string Code);
        Task<Product> GetByNameAsync(string Name);
    }
}
