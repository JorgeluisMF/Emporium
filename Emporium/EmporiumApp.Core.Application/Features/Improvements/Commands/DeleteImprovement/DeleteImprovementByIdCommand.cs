﻿using EmporiumApp.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Features.Improvements.Commands.DeleteImprovement
{
    public class DeleteImprovementByIdCommand : IRequest<int>
    {
        [SwaggerParameter(Description = "Improvement id")]
        public int Id { get; set; }
    }
    public class DeleteImprovementByIdCommandHandler : IRequestHandler<DeleteImprovementByIdCommand, int>
    {
        private readonly IImprovementRepository _repo;
        public DeleteImprovementByIdCommandHandler(IImprovementRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> Handle(DeleteImprovementByIdCommand command, CancellationToken cancellationToken)
        {
            var improvement = await _repo.GetByIdAsync(command.Id);

            if (improvement == null) throw new Exception("Record Not Found");

            await _repo.DeleteAsync(improvement);

            return command.Id;

        }
    }
}
