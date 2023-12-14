using EmporiumApp.Core.Application.Helpers;
using EmporiumApp.Core.Application.Interfaces.Services;
using EmporiumApp.Core.Application.ViewModels.Filters;
using EmporiumApp.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace Emporium.Controllers
{
    public class ClientController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductCarritoService _productCarritoService;
        private readonly IProductTypeService _productTypeService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClientController(IProductService productService, IProductCarritoService productCarritoService, IProductTypeService productTypeService, IHttpContextAccessor httpContextAccessor)
        {
            _productService = productService;
            _productCarritoService = productCarritoService;
            _productTypeService = productTypeService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index(FiltersViewModel filters)
        {
            UserViewModel user = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");

            var filter = await _productService.GetAllWithFilters(filters);
            var favs = await _productCarritoService.GetAllVmAsync();

            filter.ForEach(prop =>
                prop.IsFavourite = favs.Any(fav => fav.ProductId == prop.Id && fav.ClientId == user.Id) ? true : false
            );

            ViewBag.ProductTypes = await _productTypeService.GetAllVmAsync();
            return View(filter);
        }

        public async Task<IActionResult> Carrito(FiltersViewModel filters)
        {
            UserViewModel user = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");

            var filter = await _productService.GetAllWithFilters(filters);
            var favs = await _productCarritoService.GetAllVmAsync();

            filter.ForEach(prop =>
                prop.IsFavourite = favs.Any(fav => fav.ProductId == prop.Id && fav.ClientId == user.Id) ? true : false
            );

            var favList = filter.Where(x => x.IsFavourite == true).ToList();

            ViewBag.ProductTypes = await _productTypeService.GetAllVmAsync();

            return View(favList);
        }

        [HttpPost]
        public async Task<IActionResult> SetCarrito(int id)
        {
            UserViewModel user = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");

            await _productCarritoService.ChangeFavouritePropStatus(user.Id, id);

            return RedirectToRoute(new { controller = "Client", action = "Index" });
        }
    }
}
