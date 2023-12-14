using AutoMapper;
using EmporiumApp.Core.Application.Dtos.Product;
using EmporiumApp.Core.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public int Id { get; set; }
    }
    public class GetPropertyByIdQueryHanler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public GetPropertyByIdQueryHanler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var property = await GetByIdVm(request.Id);
            if (property == null) throw new Exception("record not found");
            return property;
        }

        public async Task<ProductDto> GetByIdVm(int id)
        {
            var property = await _repo.GetByIdAsync(id);
            var propertyDto = _mapper.Map<ProductDto>(property);

            return propertyDto;
        }
    }
}
