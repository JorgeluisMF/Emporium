using AutoMapper;
using EmporiumApp.Core.Application.Dtos.SalesType;
using EmporiumApp.Core.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Features.SalesType.Queries.GetSaleTypeById
{
    public class GetSaleTypeByIdQuery : IRequest<SalesTypeDto>
    {
        public int Id { get; set; }
    }
    public class GetSaleTypeByIdQueryHandler : IRequestHandler<GetSaleTypeByIdQuery, SalesTypeDto>
    {
        private readonly ISaleTypeRepository _repo;
        private readonly IMapper _mapper;
        public GetSaleTypeByIdQueryHandler(ISaleTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<SalesTypeDto> Handle(GetSaleTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var saleType = await GetByIdVm(request.Id);
            if (saleType == null) throw new Exception("record not found");
            return saleType;
        }

        public async Task<SalesTypeDto> GetByIdVm(int id)
        {
            var saleType = await _repo.GetByIdAsync(id);
            var saleTypeDto = _mapper.Map<SalesTypeDto>(saleType);

            return saleTypeDto;
        }
    }
}
