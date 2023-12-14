﻿using EmporiumApp.Core.Application.ViewModels.Improvement;
using EmporiumApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Interfaces.Services
{
    public interface IImprovementService : IGenericService<ImprovementSaveViewModel, ImprovementViewModel, Improvement>
    {
    }
}
