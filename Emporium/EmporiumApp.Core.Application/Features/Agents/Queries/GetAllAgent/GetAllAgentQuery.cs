using AutoMapper;
using EmporiumApp.Core.Application.Dtos.UserAccounts;
using EmporiumApp.Core.Application.Enums;
using EmporiumApp.Core.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Features.Agents.Queries.GetAllAgent
{
    public class GetAllAgentQuery : IRequest<IList<AgentDto>>
    {
        public int? id { get; set; }
    }
    public class GetAllAgentQueryHandler : IRequestHandler<GetAllAgentQuery, IList<AgentDto>>
    {
        private readonly IAccountService _svc;
        private readonly IMapper _mapper;

        public GetAllAgentQueryHandler(IAccountService svc, IMapper mapper)
        {
            _svc = svc;
            _mapper = mapper;
        }

        public async Task<IList<AgentDto>> Handle(GetAllAgentQuery request, CancellationToken cancellationToken)
        {
            var agentList = await _svc.GetAllUsers();

            agentList = agentList.Where(x => x.TypeUser == (int)Roles.Agent).ToList();

            var agentListDto = _mapper.Map<List<AgentDto>>(agentList);
            return agentListDto;
        }
    }
}
