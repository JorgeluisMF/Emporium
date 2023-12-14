using AutoMapper;
using EmporiumApp.Core.Application.Interfaces.Services;
using EmporiumApp.Core.Application.ViewModels.ProductType;
using Microsoft.AspNetCore.Mvc;

namespace Emporium.Controllers
{
    public class ProductTypeController : Controller
    {
        private readonly IProductTypeService _productTypeService;
        private readonly IMapper _mapper;
        public ProductTypeController(IProductTypeService productTypeService, IMapper mapper)
        {
            _productTypeService = productTypeService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productTypeService.GetAllWithInclude());
        }

        public IActionResult Add()
        {
            return View("Save", new ProductTypeSaveViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductTypeSaveViewModel vm)
        {
            if (!ModelState.IsValid) return View("Save", vm);

            await _productTypeService.AddAsync(vm);
            return RedirectToRoute(new { controller = "ProductType", action = "Index" });

        }

        public async Task<IActionResult> Update(int id)
        {
            return View("Save", await _productTypeService.GetByIdVmAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductTypeSaveViewModel vm)
        {
            await _productTypeService.UpdateAsync(vm, vm.Id);
            return RedirectToRoute(new { controller = "ProductType", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _productTypeService.GetByIdVmAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProductTypeSaveViewModel vm)
        {
            await _productTypeService.DeleteAsync(vm.Id);
            return RedirectToRoute(new { controller = "ProductType", action = "Index" });
        }
    }
}
