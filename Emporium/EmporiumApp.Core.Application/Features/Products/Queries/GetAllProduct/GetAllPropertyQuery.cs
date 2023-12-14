using AutoMapper;
using EmporiumApp.Core.Application.Dtos.Product;
using EmporiumApp.Core.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Features.Products.Queries.GetAllProduct
{
    public class GetAllPropertyQuery : IRequest<IList<ProductDto>>
    {
        public int? id { get; set; }
    }
    public class GetAllPropertyQueryHandler : IRequestHandler<GetAllPropertyQuery, IList<ProductDto>>
    {
        private readonly IProductRepository _repo;
        private readonly IImprovementRepository _repoImrpvement;
        private readonly IProductTypeRepository _propTypeRepository;
        private readonly ISaleTypeRepository _saleTypeRepository;
        private readonly IMapper _mapper;


        public GetAllPropertyQueryHandler(IProductRepository repo, IMapper mapper, IProductTypeRepository propertyTypeRepository, ISaleTypeRepository saleTypeRepostory)
        {
            _repo = repo;
            _mapper = mapper;
            _propTypeRepository = propertyTypeRepository;
            _saleTypeRepository = saleTypeRepostory;
        }

        public async Task<IList<ProductDto>> Handle(GetAllPropertyQuery request, CancellationToken cancellationToken)
        {
            var propertyList = await GetAllDtos();
            if (propertyList == null || propertyList.Count == 0) throw new Exception("No Record Found");
            return propertyList;
        }
        private async Task<List<ProductDto>> GetAllDtos()
        {
            var propertyList = await _repo.GetAllWithIncludeAsync(new List<string> { "Improvements" });
            var propListDto = propertyList.Select(p => new ProductDto
            {
                Id = p.Id,
                Code = p.Code,
                ProductTypeId = GetPropertyTypeName(p.ProductTypeId),
                SaleTypeId = GetSaleTypeName(p.SaleTypeId),
                Price = p.Price,
                ParcelSize = p.ParcelSize,
                RoomQty = p.RoomQty,
                RestRoomQty = p.RestRoomQty,
                Description = p.Description,
                AgentName = p.AgentName,
                IdAgent = p.IdAgent
            }).ToList();

            return propListDto;
        }

        private string GetPropertyTypeName(int id)
        {
            var propType = _propTypeRepository.GetById(id);
            return propType.Name;
        }
        private string GetSaleTypeName(int id)
        {
            var saleType = _saleTypeRepository.GetById(id);
            return saleType.Name;
        }
    }
}
