using AutoMapper;
using EmporiumApp.Core.Application.Interfaces.Repositories;
using EmporiumApp.Core.Application.Interfaces.Services;
using EmporiumApp.Core.Application.ViewModels.Carrito;
using EmporiumApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Services
{
    public class ProductCarritoService : GenericService<ProductCarritoSaveViewModel, ProductCarritoViewModel, Carrito>, IProductCarritoService
    {
        private readonly IProductCarritoRepository _propertyFavoriteRepo;
        private readonly IProductRepository _propertyRepository;
        private readonly IMapper _mapper;
        public ProductCarritoService(IProductCarritoRepository propertyFavoriteRepo, IProductRepository propertyRepository, IMapper mapper) : base(propertyFavoriteRepo, mapper)
        {
            _propertyFavoriteRepo = propertyFavoriteRepo;
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }
        public async Task ChangeFavouritePropStatus(string clientId, int propId)
        {
            var favList = await _propertyFavoriteRepo.GetAllAsync();

            var favProp = favList.Where(x => x.ProductId == propId && x.ClientId == clientId).FirstOrDefault();

            if (favProp != null)
            {
                await _propertyFavoriteRepo.DeleteAsync(favProp);
                return;
            }

            Carrito item = new Carrito
            {
                ProductId = propId,
                ClientId = clientId,
            };

            await _propertyFavoriteRepo.AddAsync(item);
        }
    }
}
