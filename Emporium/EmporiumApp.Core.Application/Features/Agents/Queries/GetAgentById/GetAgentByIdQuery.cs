using AutoMapper;
using EmporiumApp.Core.Application.Dtos.UserAccounts;
using EmporiumApp.Core.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Features.Agents.Queries.GetAgentById
{
    public class GetAgentByIdQuery : IRequest<AgentDto>
    {
        public string Id { get; set; }
    }
    public class GetAgentByIdQueryHanler : IRequestHandler<GetAgentByIdQuery, AgentDto>
    {
        private readonly IAccountService _svc;
        private readonly IMapper _mapper;
        public GetAgentByIdQueryHanler(IAccountService svc, IMapper mapper)
        {
            _svc = svc;
            _mapper = mapper;
        }

        public async Task<AgentDto> Handle(GetAgentByIdQuery request, CancellationToken cancellationToken)
        {
            var agent = await GetByIdVm(request.Id);
            if (agent == null) throw new Exception("record not found");
            return agent;
        }

        public async Task<AgentDto> GetByIdVm(string id)
        {
            var agent = await _svc.GetUserById(id);
            var agentDto = _mapper.Map<AgentDto>(agent);
            return agentDto;
        }
    }
}
