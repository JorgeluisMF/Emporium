using EmporiumApp.Core.Application.ViewModels.Carrito;
using EmporiumApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Interfaces.Services
{
    public interface IProductCarritoService : IGenericService<ProductCarritoSaveViewModel, ProductCarritoViewModel, Carrito>
    {
        Task ChangeFavouritePropStatus(string clientId, int propId);
    }
}
