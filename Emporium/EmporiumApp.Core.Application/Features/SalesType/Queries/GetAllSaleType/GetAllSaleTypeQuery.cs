using AutoMapper;
using EmporiumApp.Core.Application.Dtos.SalesType;
using EmporiumApp.Core.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Features.SalesType.Queries.GetAllSaleType
{
    public class GetAllSaleTypeQuery : IRequest<IList<SalesTypeDto>>
    {
        public int? id { get; set; }
    }
    public class GetAllSaleTypeQueryHandler : IRequestHandler<GetAllSaleTypeQuery, IList<SalesTypeDto>>
    {
        private readonly ISaleTypeRepository _repo;
        private readonly IMapper _mapper;

        public GetAllSaleTypeQueryHandler(ISaleTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IList<SalesTypeDto>> Handle(GetAllSaleTypeQuery request, CancellationToken cancellationToken)
        {
            var saleTypeList = await _repo.GetAllAsync();
            var saleTypeListDto = _mapper.Map<List<SalesTypeDto>>(saleTypeList);
            return saleTypeListDto;
        }
    }
}
