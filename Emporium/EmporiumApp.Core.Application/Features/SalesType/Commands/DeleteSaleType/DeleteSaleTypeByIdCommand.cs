﻿using EmporiumApp.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Features.SalesType.Commands.DeleteSaleType
{
    public class DeleteSaleTypeByIdCommand : IRequest<int>
    {
        [SwaggerParameter(Description = "Sale Type id")]
        public int Id { get; set; }
    }
    public class DeleteSaleTypeByIdCommandHandler : IRequestHandler<DeleteSaleTypeByIdCommand, int>
    {
        private readonly ISaleTypeRepository _repo;
        public DeleteSaleTypeByIdCommandHandler(ISaleTypeRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> Handle(DeleteSaleTypeByIdCommand command, CancellationToken cancellationToken)
        {
            var saleType = await _repo.GetByIdAsync(command.Id);

            if (saleType == null) throw new Exception("Record Not Found");

            await _repo.DeleteAsync(saleType);

            return command.Id;

        }
    }
}
