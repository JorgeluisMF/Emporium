using Emporium.Models;
using EmporiumApp.Core.Application.Interfaces.Services;
using EmporiumApp.Core.Application.ViewModels.Filters;
using EmporiumApp.Core.Application.ViewModels.Improvement;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Emporium.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly IProductImprovementService _productImprovementService;
        private readonly IImprovementService _improvementService;
        private readonly IProductTypeService _productTypeService;

        public HomeController(IProductService propertyService, IProductTypeService propertyTypeService, IProductImprovementService propertyImprovementService, IImprovementService improvementService, IUserService userService)
        {
            _productService = propertyService;
            _productTypeService = propertyTypeService;
            _productImprovementService = propertyImprovementService;
            _improvementService = improvementService;
            _userService = userService;
        }

        public async Task<IActionResult> Index(FiltersViewModel filters)
        {
            ViewBag.ProductTypes = await _productTypeService.GetAllVmAsync();
            return View(await _productService.GetAllWithFilters(filters));
        }

        public async Task<IActionResult> Details(int id)
        {
            var props = await _productService.GetAllWithInclude();
            var prop = props.Where(p => p.Id == id).FirstOrDefault();

            var agent = await _userService.GetAllVmAsync();
            ViewBag.Agent = agent.Where(a => a.Id == prop.IdAgent);

            var imps = await _productImprovementService.GetAllVmAsync();
            var impsIds = imps.Where(i => i.PropId == prop.Id).ToList();

            List<ImprovementSaveViewModel> improvements = new();

            foreach (var item in impsIds)
            {
                improvements.Add(await _improvementService.GetByIdVmAsync(item.ImprovementId));
            }

            ViewBag.Imps = improvements;

            return View(prop);
        }
    }
}