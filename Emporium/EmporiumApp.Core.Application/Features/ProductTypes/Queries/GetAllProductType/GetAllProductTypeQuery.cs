using AutoMapper;
using EmporiumApp.Core.Application.Dtos.ProductType;
using EmporiumApp.Core.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Features.ProductTypes.Queries.GetAllProductType
{
    public class GetAllProductTypeQuery : IRequest<IList<ProductTypeDto>>
    {
        public int? id { get; set; }
    }
    public class GetAllPropertyTypeQueryHandler : IRequestHandler<GetAllProductTypeQuery, IList<ProductTypeDto>>
    {
        private readonly IProductTypeRepository _repo;
        private readonly IMapper _mapper;

        public GetAllPropertyTypeQueryHandler(IProductTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IList<ProductTypeDto>> Handle(GetAllProductTypeQuery request, CancellationToken cancellationToken)
        {
            var propertyTypeList = await _repo.GetAllAsync();
            var propertyTypeListDto = _mapper.Map<List<ProductTypeDto>>(propertyTypeList);
            return propertyTypeListDto;
        }
    }
}
