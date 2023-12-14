using EmporiumApp.Core.Application.ViewModels.ProductType;
using EmporiumApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Interfaces.Services
{
    public interface IProductTypeService : IGenericService<ProductTypeSaveViewModel, ProductTypeViewModel, ProductType>
    {
        Task<List<ProductTypeViewModel>> GetAllWithInclude();
    }
}
