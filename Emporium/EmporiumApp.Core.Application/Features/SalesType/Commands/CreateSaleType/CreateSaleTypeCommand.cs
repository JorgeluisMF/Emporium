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

namespace EmporiumApp.Core.Application.Features.SalesType.Commands.CreateSaleType
{
    public class CreateSaleTypeCommand : IRequest<int>
    {
        [SwaggerParameter(Description = "Sale Type name")]
        public string Name { get; set; }
        [SwaggerParameter(Description = "Sale Type name")]
        public string Description { get; set; }
    }
    public class CreateSaleTypeCommandHandler : IRequestHandler<CreateSaleTypeCommand, int>
    {
        private readonly ISaleTypeRepository _repo;
        private readonly IMapper _mapper;
        public CreateSaleTypeCommandHandler(ISaleTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateSaleTypeCommand command, CancellationToken cancellationToken)
        {
            var saleType = _mapper.Map<SaleType>(command);
            saleType = await _repo.AddAsync(saleType);
            return saleType.Id;
        }
    }
}
