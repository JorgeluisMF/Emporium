using AutoMapper;
using EmporiumApp.Core.Application.Helpers;
using EmporiumApp.Core.Application.Interfaces.Repositories;
using EmporiumApp.Core.Application.Interfaces.Services;
using EmporiumApp.Core.Application.ViewModels.Filters;
using EmporiumApp.Core.Application.ViewModels.Product;
using EmporiumApp.Core.Domain.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Services
{
    public class ProductService : GenericService<ProductSaveViewModel, ProductViewModel, Product>, IProductService
    {
        private readonly ProductCodeGenerator _codeGenerator = new();
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        private readonly IProductImprovementRepository _propImprovementRepo;

        public ProductService(IProductRepository repo, IMapper mapper, IProductImprovementRepository propImprovementRepo) : base(repo, mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _propImprovementRepo = propImprovementRepo;
        }
        public List<ProductViewModel> GetAll()
        {
            List<Product> properties = _repo.GetAll();
            return properties.Select(prop => new ProductViewModel()
            {
                Id = prop.Id,
                AgentName = prop.AgentName,
                Ubication = prop.Ubication,
                Code = prop.Code,
                IdAgent = prop.IdAgent,
                ParcelSize = prop.ParcelSize,
                Price = prop.Price,
                Description = prop.Description,
                RestRoomQty = prop.RestRoomQty,
                RoomQty = prop.RoomQty,
                ProductImgUrl1 = prop.ProductImgUrl1,
                ProductImgUrl2 = prop.ProductImgUrl2,
                ProductImgUrl3 = prop.ProductImgUrl3,
                ProductImgUrl4 = prop.ProductImgUrl4,

            }).ToList();
        }
        public async Task<List<ProductViewModel>> GetAllWithInclude()
        {
            List<Product> properties = await _repo.GetAllWithIncludeAsync(new List<string> { "Improvements", "ProductType", "SaleType" });

            return properties.Select(prop => new ProductViewModel()
            {
                Id = prop.Id,
                AgentName = prop.AgentName,
                Ubication = prop.Ubication,
                Code = prop.Code,
                IdAgent = prop.IdAgent,
                ParcelSize = prop.ParcelSize,
                ProductType = prop.ProductType.Name,
                SaleType = prop.SaleType.Name,
                Price = prop.Price,
                Description = prop.Description,
                RestRoomQty = prop.RestRoomQty,
                RoomQty = prop.RoomQty,
                ProductImgUrl1 = prop.ProductImgUrl1,
                ProductImgUrl2 = prop.ProductImgUrl2,
                ProductImgUrl3 = prop.ProductImgUrl3,
                ProductImgUrl4 = prop.ProductImgUrl4,
                // IsFavourite = prop.IsFavourite

            }).ToList();
        }
        public async Task<List<ProductViewModel>> GetAllWithFilters(FiltersViewModel filters)
        {
            List<Product> properties = await _repo.GetAllWithIncludeAsync(new List<string> { "Improvements", "ProductType", "SaleType" });

            var listVm = properties.OrderByDescending(prop => prop.Created).Select(prop => new ProductViewModel()
            {
                Id = prop.Id,
                AgentName = prop.AgentName,
                Ubication = prop.Ubication,
                Code = prop.Code,
                IdAgent = prop.IdAgent,
                ParcelSize = prop.ParcelSize,
                ProductTypeId = prop.ProductType.Id,
                ProductType = prop.ProductType.Name,
                SaleTypeId = prop.SaleType.Id,
                SaleType = prop.SaleType.Name,
                Price = prop.Price,
                Description = prop.Description,
                RestRoomQty = prop.RestRoomQty,
                RoomQty = prop.RoomQty,
                ProductImgUrl1 = prop.ProductImgUrl1,
                ProductImgUrl2 = prop.ProductImgUrl2,
                ProductImgUrl3 = prop.ProductImgUrl3,
                ProductImgUrl4 = prop.ProductImgUrl4,

            }).ToList();

            if (!string.IsNullOrWhiteSpace(filters.code))
            {
                listVm = listVm.Where(prop => prop.Code == filters.code).ToList();
                return listVm;
            }

            if (filters.productTypeId != 0 && filters.roomQty == 0 && filters.restRoomQty == 0)
            {
                listVm = listVm
                    .Where(prop => prop.ProductTypeId == filters.productTypeId)
                    .ToList();
                return listVm;
            }
            if (filters.productTypeId == 0 && filters.roomQty != 0 && filters.restRoomQty == 0)
            {
                listVm = listVm
                    .Where(prop => prop.RoomQty == filters.roomQty)
                    .ToList();
                return listVm;
            }
            if (filters.productTypeId == 0 && filters.roomQty == 0 && filters.restRoomQty != 0)
            {
                listVm = listVm
                    .Where(prop => prop.RestRoomQty == filters.restRoomQty)
                    .ToList();
                return listVm;
            }
            if (filters.productTypeId != 0 && filters.roomQty != 0 && filters.restRoomQty == 0)
            {
                listVm = listVm
                    .Where(prop => prop.ProductTypeId == filters.productTypeId
                    && prop.RoomQty == filters.roomQty)
                    .ToList();
                return listVm;
            }
            if (filters.productTypeId != 0 && filters.roomQty == 0 && filters.restRoomQty != 0)
            {
                listVm = listVm
                    .Where(prop => prop.ProductTypeId == filters.productTypeId
                    && prop.RestRoomQty == filters.restRoomQty)
                    .ToList();
                return listVm;
            }
            if (filters.productTypeId == 0 && filters.roomQty != 0 && filters.restRoomQty != 0)
            {
                listVm = listVm
                    .Where(prop => prop.RoomQty == filters.roomQty
                    && prop.RestRoomQty == filters.restRoomQty)
                    .ToList();
                return listVm;
            }
            if (filters.productTypeId != 0 && filters.roomQty != 0 && filters.restRoomQty != 0)
            {
                listVm = listVm
                    .Where(prop => prop.ProductTypeId == filters.productTypeId
                    && prop.RoomQty == filters.roomQty && prop.RestRoomQty == filters.restRoomQty)
                    .ToList();
                return listVm;
            }

            if (filters.MinPrice != 0 || filters.MaxPrice != 0)
            {
                if (filters.MinPrice != 0 && filters.MaxPrice != 0)
                {
                    listVm = listVm
                    .Where(prop => prop.Price >= filters.MinPrice && prop.Price <= filters.MaxPrice)
                    .ToList();
                    return listVm;
                }
                if (filters.MinPrice == 0 && filters.MaxPrice != 0)
                {
                    listVm = listVm
                    .Where(prop => prop.Price <= filters.MaxPrice)
                    .ToList();
                    return listVm;
                }
                if (filters.MaxPrice == 0 && filters.MinPrice != 0)
                {
                    listVm = listVm
                    .Where(prop => prop.Price >= filters.MinPrice)
                    .ToList();
                    return listVm;
                }
            }

            return listVm;
        }
        public async override Task<ProductSaveViewModel> AddAsync(ProductSaveViewModel vm)
        {
            vm.Code = _codeGenerator.ProductCodeGen();
            if (await ExistCodeNumber(vm.Code))
            {
                var newCodeNumber = _codeGenerator.ProductCodeGen();
                vm.Code = newCodeNumber;
            }

            Product prop = _mapper.Map<Product>(vm);
            var propCreated = await _repo.AddAsync(prop);

            foreach (var item in vm.ListImprovement)
            {
                var propImproment = new ProductImprovement()
                {
                    PropId = propCreated.Id,
                    ImprovementId = item,
                };
                await _propImprovementRepo.AddAsync(propImproment);
            }

            var property = _mapper.Map<ProductSaveViewModel>(propCreated);
            return property;
        }

        private async Task<bool> ExistCodeNumber(string code)
        {
            List<Product> properties = await _repo.GetAllAsync();
            bool exist = properties.Any(e => e.Code == code);
            return exist;
        }

    }
}
