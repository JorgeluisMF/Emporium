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

namespace EmporiumApp.Core.Application.Features.Improvements.Commands.CreateImprovement
{
    public class CreateImprovementCommand : IRequest<int>
    {
        [SwaggerParameter(Description = "Improvement name")]
        public string Name { get; set; }
        [SwaggerParameter(Description = "Improvement Description")]
        public string Description { get; set; }
    }
    public class CreateImprovementCommandHandler : IRequestHandler<CreateImprovementCommand, int>
    {
        private readonly IImprovementRepository _repo;
        private readonly IMapper _mapper;
        public CreateImprovementCommandHandler(IImprovementRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateImprovementCommand command, CancellationToken cancellationToken)
        {
            var improvement = _mapper.Map<Improvement>(command);
            improvement = await _repo.AddAsync(improvement);
            return improvement.Id;
        }
    }
}
