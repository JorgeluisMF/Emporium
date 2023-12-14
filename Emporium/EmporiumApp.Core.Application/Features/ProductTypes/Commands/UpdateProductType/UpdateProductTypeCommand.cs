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

namespace EmporiumApp.Core.Application.Features.ProductTypes.Commands.UpdateProductType
{
    public class UpdateProductTypeCommand : IRequest<ProductTypeUpdateResponse>
    {
        [SwaggerParameter(Description = "Property Type id")]
        public int Id { get; set; }
        [SwaggerParameter(Description = "Property Type name")]
        public string Name { get; set; }
        [SwaggerParameter(Description = "Property Type description")]
        public string Description { get; set; }
    }
    public class UpdatePropertyTypeCommandHandler : IRequestHandler<UpdateProductTypeCommand, ProductTypeUpdateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductTypeRepository _repo;
        public UpdatePropertyTypeCommandHandler(IMapper mapper, IProductTypeRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<ProductTypeUpdateResponse> Handle(UpdateProductTypeCommand command, CancellationToken cancellationToken)
        {
            var propType = await _repo.GetByIdAsync(command.Id);

            if (propType == null) throw new Exception("Record Not Found");

            propType = _mapper.Map<ProductType>(command);

            await _repo.UpdateAsync(propType, propType.Id);

            var propTypeResponse = _mapper.Map<ProductTypeUpdateResponse>(propType);

            return propTypeResponse;
        }
    }
}
