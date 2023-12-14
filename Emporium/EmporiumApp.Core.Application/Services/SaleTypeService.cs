using AutoMapper;
using EmporiumApp.Core.Application.Interfaces.Repositories;
using EmporiumApp.Core.Application.Interfaces.Services;
using EmporiumApp.Core.Application.ViewModels.SalesType;
using EmporiumApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Services
{
    public class SaleTypeService : GenericService<SaleTypeSaveViewModel, SaleTypeViewModel, SaleType>, ISaleTypeService
    {
        private readonly ISaleTypeRepository _saleTypeRepo;
        private readonly IMapper _mapper;
        public SaleTypeService(ISaleTypeRepository saleTypeRepo, IMapper mapper) : base(saleTypeRepo, mapper)
        {
            _saleTypeRepo = saleTypeRepo;
            _mapper = mapper;
        }

        public async Task<List<SaleTypeViewModel>> GetAllWithInclude()
        {
            List<SaleType> propertyTypes = await _saleTypeRepo.GetAllWithIncludeAsync(new List<string> { "Products" });
            List<SaleTypeViewModel> saleTypeViewModels = new();

            saleTypeViewModels = propertyTypes.Select(prop => new SaleTypeViewModel()
            {
                Id = prop.Id,
                Name = prop.Name,
                Description = prop.Description,
                ProductQty = prop.Products.Count()

            }).ToList();

            return saleTypeViewModels;
        }
    }
}
