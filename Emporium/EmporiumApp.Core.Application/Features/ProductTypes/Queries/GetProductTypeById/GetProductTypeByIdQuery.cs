using AutoMapper;
using EmporiumApp.Core.Application.Dtos.ProductType;
using EmporiumApp.Core.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Features.ProductTypes.Queries.GetProductTypeById
{
    public class GetProductTypeByIdQuery : IRequest<ProductTypeDto>
    {
        public int Id { get; set; }
    }
    public class GetPropertyTypeByIdQueryHanler : IRequestHandler<GetProductTypeByIdQuery, ProductTypeDto>
    {
        private readonly IProductTypeRepository _repo;
        private readonly IMapper _mapper;
        public GetPropertyTypeByIdQueryHanler(IProductTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ProductTypeDto> Handle(GetProductTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var propType = await GetByIdVm(request.Id);
            if (propType == null) throw new Exception("record not found");
            return propType;
        }

        public async Task<ProductTypeDto> GetByIdVm(int id)
        {
            var propType = await _repo.GetByIdAsync(id);
            var propTypeDto = _mapper.Map<ProductTypeDto>(propType);

            return propTypeDto;
        }
    }
}
