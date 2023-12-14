using AutoMapper;
using EmporiumApp.Core.Application.Dtos.Improvements;
using EmporiumApp.Core.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Features.Improvements.Queries.GetImprovementById
{
    public class GetImprovementByIdQuery : IRequest<ImprovementDto>
    {
        public int Id { get; set; }
    }
    public class GetImprovementByIdQueryHanler : IRequestHandler<GetImprovementByIdQuery, ImprovementDto>
    {
        private readonly IImprovementRepository _repo;
        private readonly IMapper _mapper;
        public GetImprovementByIdQueryHanler(IImprovementRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ImprovementDto> Handle(GetImprovementByIdQuery request, CancellationToken cancellationToken)
        {
            var improvement = await GetByIdVm(request.Id);
            if (improvement == null) throw new Exception("record not found");
            return improvement;
        }

        public async Task<ImprovementDto> GetByIdVm(int id)
        {
            var improvement = await _repo.GetByIdAsync(id);
            var improvementDto = _mapper.Map<ImprovementDto>(improvement);

            return improvementDto;
        }
    }
}
