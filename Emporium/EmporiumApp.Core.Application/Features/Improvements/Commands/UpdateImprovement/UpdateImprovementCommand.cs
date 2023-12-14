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

namespace EmporiumApp.Core.Application.Features.Improvements.Commands.UpdateImprovement
{
    public class UpdateImprovementCommand : IRequest<ImprovementUpdateResponse>
    {
        [SwaggerParameter(Description = "Improvement id")]
        public int Id { get; set; }
        [SwaggerParameter(Description = "Improvement name")]
        public string Name { get; set; }
        [SwaggerParameter(Description = "Improvement description")]
        public string Description { get; set; }
    }
    public class UpdateImprovementCommandHandler : IRequestHandler<UpdateImprovementCommand, ImprovementUpdateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IImprovementRepository _repo;
        public UpdateImprovementCommandHandler(IMapper mapper, IImprovementRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<ImprovementUpdateResponse> Handle(UpdateImprovementCommand command, CancellationToken cancellationToken)
        {
            var improvement = await _repo.GetByIdAsync(command.Id);

            if (improvement == null) throw new Exception("Record Not Found");

            improvement = _mapper.Map<Improvement>(command);

            await _repo.UpdateAsync(improvement, improvement.Id);

            var improvementResponse = _mapper.Map<ImprovementUpdateResponse>(improvement);

            return improvementResponse;
        }
    }
}
