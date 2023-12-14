using AutoMapper;
using EmporiumApp.Core.Application.Interfaces.Repositories;
using EmporiumApp.Core.Domain.Entities;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Features.ProductTypes.Commands.CreateProductType
{
    public class CreateProductTypeCommand : IRequest<int>
    {
        [SwaggerParameter(Description = "Property Type name")]
        public string Name { get; set; }
        [SwaggerParameter(Description = "Property Type Description")]
        public string Description { get; set; }
    }
    public class CreatePropertyTypeCommandHandler : IRequestHandler<CreateProductTypeCommand, int>
    {
        private readonly IProductTypeRepository _repo;
        private readonly IMapper _mapper;
        public CreatePropertyTypeCommandHandler(IProductTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateProductTypeCommand command, CancellationToken cancellationToken)
        {
            var propType = _mapper.Map<ProductType>(command);
            propType = await _repo.AddAsync(propType);
            return propType.Id;
        }
    }
}
