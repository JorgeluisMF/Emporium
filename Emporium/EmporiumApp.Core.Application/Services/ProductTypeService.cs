using AutoMapper;
using EmporiumApp.Core.Application.Interfaces.Repositories;
using EmporiumApp.Core.Application.Interfaces.Services;
using EmporiumApp.Core.Application.ViewModels.ProductType;
using EmporiumApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Services
{
    public class ProductTypeService : GenericService<ProductTypeSaveViewModel, ProductTypeViewModel, ProductType>, IProductTypeService
    {
        private readonly IProductTypeRepository _propertyTypeRepo;
        private readonly IMapper _mapper;
        public ProductTypeService(IProductTypeRepository propertyTypeRepo, IMapper mapper) : base(propertyTypeRepo, mapper)
        {
            _propertyTypeRepo = propertyTypeRepo;
            _mapper = mapper;
        }
        public async Task<List<ProductTypeViewModel>> GetAllWithInclude()
        {
            List<ProductType> propertyTypes = await _propertyTypeRepo.GetAllWithIncludeAsync(new List<string> { "Products" });
            List<ProductTypeViewModel> propertyTypeViewModels = new();

            propertyTypeViewModels = propertyTypes.Select(prop => new ProductTypeViewModel()
            {
                Id = prop.Id,
                Name = prop.Name,
                Description = prop.Description,
                ProductQty = prop.Products.Count()

            }).ToList();

            return propertyTypeViewModels;
        }
    }
}
