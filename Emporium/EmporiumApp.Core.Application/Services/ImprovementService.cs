using AutoMapper;
using EmporiumApp.Core.Application.Interfaces.Repositories;
using EmporiumApp.Core.Application.Interfaces.Services;
using EmporiumApp.Core.Application.ViewModels.Improvement;
using EmporiumApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Services
{
    public class ImprovementService : GenericService<ImprovementSaveViewModel, ImprovementViewModel, Improvement>, IImprovementService
    {
        private readonly IImprovementRepository _improvementRepo;
        private readonly IMapper _mapper;
        public ImprovementService(IImprovementRepository improvementRepo, IMapper mapper) : base(improvementRepo, mapper)
        {
            _improvementRepo = improvementRepo;
            _mapper = mapper;
        }
    }
}
