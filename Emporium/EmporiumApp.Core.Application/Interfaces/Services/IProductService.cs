using EmporiumApp.Core.Application.ViewModels.Filters;
using EmporiumApp.Core.Application.ViewModels.Product;
using EmporiumApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Interfaces.Services
{
    public interface IProductService : IGenericService<ProductSaveViewModel, ProductViewModel, Product>
    {
        List<ProductViewModel> GetAll();

        Task<List<ProductViewModel>> GetAllWithInclude();
        Task<List<ProductViewModel>> GetAllWithFilters(FiltersViewModel filters);
    }
}
