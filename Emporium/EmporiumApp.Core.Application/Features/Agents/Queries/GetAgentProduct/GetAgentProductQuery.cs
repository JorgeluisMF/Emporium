using AutoMapper;
using EmporiumApp.Core.Application.Dtos.Product;
using EmporiumApp.Core.Application.Interfaces.Repositories;
using EmporiumApp.Core.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Features.Agents.Queries.GetAgentProduct
{
    public class GetAgentProductQuery : IRequest<List<ProductDto>>
    {
        public string id { get; set; }
    }
    public class GetAgentPropertyQueryHanler : IRequestHandler<GetAgentProductQuery, List<ProductDto>>
    {
        private readonly IAccountService _svc;
        private readonly IProductRepository _propertyRepository;
        private readonly IMapper _mapper;
        public GetAgentPropertyQueryHanler(IAccountService svc, IMapper mapper, IProductRepository property)
        {
            _svc = svc;
            _mapper = mapper;
            _propertyRepository = property;
        }

        public async Task<List<ProductDto>> Handle(GetAgentProductQuery request, CancellationToken cancellationToken)
        {
            var listProp = await GetAgentProperty(request.id);
            if (listProp == null || listProp.Count == 0) throw new Exception("record not found");
            return listProp;
        }

        public async Task<List<ProductDto>> GetAgentProperty(string id)
        {
            var agent = await _svc.GetUserById(id);
            var listProperties = await _propertyRepository.GetAllAsync();
            listProperties = listProperties.Where(x => x.IdAgent == agent.Id).ToList();

            var agentListDto = _mapper.Map<List<ProductDto>>(listProperties);

            return agentListDto;
        }
    }
}
