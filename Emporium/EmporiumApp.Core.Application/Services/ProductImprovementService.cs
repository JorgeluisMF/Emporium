using AutoMapper;
using EmporiumApp.Core.Application.Interfaces.Repositories;
using EmporiumApp.Core.Application.Interfaces.Services;
using EmporiumApp.Core.Application.ViewModels.ProductImprovement;
using EmporiumApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Services
{
    public class ProductImprovementService : GenericService<ProductImprovementSaveViewModel, ProductImprovementViewModel, ProductImprovement>, IProductImprovementService
    {
        private readonly IProductImprovementRepository _repo;
        private readonly IMapper _mapper;
        public ProductImprovementService(IProductImprovementRepository repo, IMapper mapper) : base(repo, mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
    }
}
