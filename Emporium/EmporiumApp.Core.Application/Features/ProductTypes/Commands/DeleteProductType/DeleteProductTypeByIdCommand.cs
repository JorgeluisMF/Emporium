using EmporiumApp.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Features.ProductTypes.Commands.DeleteProductType
{
    public class DeleteProductTypeByIdCommand : IRequest<int>
    {
        [SwaggerParameter(Description = "Property Id")]
        public int Id { get; set; }
    }
    public class DeletePropertyTypeByIdCommandHandler : IRequestHandler<DeleteProductTypeByIdCommand, int>
    {
        private readonly IProductTypeRepository _repo;
        public DeletePropertyTypeByIdCommandHandler(IProductTypeRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> Handle(DeleteProductTypeByIdCommand command, CancellationToken cancellationToken)
        {
            var propType = await _repo.GetByIdAsync(command.Id);

            if (propType == null) throw new Exception("Record Not Found");

            await _repo.DeleteAsync(propType);

            return command.Id;

        }
    }
}
