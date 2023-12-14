using EmporiumApp.Core.Application.Interfaces.Repositories;
using EmporiumApp.Core.Domain.Entities;
using EmporiumApp.Infraestructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Infraestructure.Persistence.Repositories
{
    public class ProductTypeRepository : GenericRepository<ProductType>, IProductTypeRepository
    {
        private readonly ApplicationContext _db;
        public ProductTypeRepository(ApplicationContext db) : base(db)
        {
            _db = db;
        }
    }
}
