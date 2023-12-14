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
    public class ProductImprovementRepository : GenericRepository<ProductImprovement>, IProductImprovementRepository
    {
        private readonly ApplicationContext _db;
        public ProductImprovementRepository(ApplicationContext db) : base(db)
        {
            _db = db;
        }
    }
}
