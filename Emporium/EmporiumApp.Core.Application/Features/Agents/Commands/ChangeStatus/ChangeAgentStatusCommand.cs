﻿
using AutoMapper;
using EmporiumApp.Core.Application.Interfaces.Services;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Features.Agents.Commands.ChangeStatus
{
    public class ChangeAgentStatusCommand : IRequest<ChangeAgentStatusResponse>
    {
        [SwaggerParameter(Description = "Agent Id")]
        public string Id { get; set; }
        [SwaggerParameter(Description = "Agent Status")]

        public bool Status { get; set; }
    }
    public class ChangeAgentStatusCommandHandler : IRequestHandler<ChangeAgentStatusCommand, ChangeAgentStatusResponse>
    {
        private readonly IAccountService _svc;
        private readonly IMapper _mapper;
        public ChangeAgentStatusCommandHandler(IAccountService svc, IMapper mapper)
        {
            _svc = svc;
            _mapper = mapper;
        }
        public async Task<ChangeAgentStatusResponse> Handle(ChangeAgentStatusCommand command, CancellationToken cancellationToken)
        {
            var agent = await _svc.GetUserById(command.Id);
            if (agent == null) throw new Exception("Record Not Found");

            var changeStatus = await _svc.ActivedAgentAsync(agent.Id, command.Status);
            var returnChangeStatus = _mapper.Map<ChangeAgentStatusResponse>(changeStatus);
            return returnChangeStatus;

        }

    }
}
