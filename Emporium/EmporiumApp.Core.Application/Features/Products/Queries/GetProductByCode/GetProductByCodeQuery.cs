using AutoMapper;
using EmporiumApp.Core.Application.Dtos.Product;
using EmporiumApp.Core.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Features.Products.Queries.GetProductByCode
{
    public class GetProductByCodeQuery : IRequest<ProductDto>
    {
        public string Code { get; set; }
    }
    public class GetPropertyByCodeQueryHanler : IRequestHandler<GetProductByCodeQuery, ProductDto>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public GetPropertyByCodeQueryHanler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetProductByCodeQuery request, CancellationToken cancellationToken)
        {
            var property = await GetByCodeVm(request.Code);
            if (property == null) throw new Exception("record not found");
            return property;


        }
        public async Task<ProductDto> GetByCodeVm(string code)
        {
            var listProperty = await _repo.GetAllAsync();
            var property = listProperty.FirstOrDefault(x => x.Code == code);
            var propertyDto = _mapper.Map<ProductDto>(property);

            return propertyDto;
        }


    }
}
