using EmporiumApp.Core.Application.Interfaces.Repositories;
using EmporiumApp.Core.Domain.Entities;
using EmporiumApp.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Infraestructure.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationContext _db;
        public ProductRepository(ApplicationContext db) : base(db)
        {
            _db = db;
        }

        public List<Product> GetAll()
        {
            return _db.Set<Product>().ToList();
        }

        public async Task<Product> GetByCodeAsync(string Code)
        {
            return await _db.Set<Product>().FindAsync(Code);
        }

        public async Task<Product> GetByNameAsync(string Name)
        {
            return await _db.Set<Product>().FindAsync(Name);
        }
    }
}
