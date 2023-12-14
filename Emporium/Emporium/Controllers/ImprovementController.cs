using AutoMapper;
using EmporiumApp.Core.Application.Interfaces.Services;
using EmporiumApp.Core.Application.ViewModels.Improvement;
using Microsoft.AspNetCore.Mvc;

namespace Emporium.Controllers
{
    public class ImprovementController : Controller
    {
        private readonly IImprovementService _improvementService;
        private readonly IMapper _mapper;
        public ImprovementController(IImprovementService improvementService, IMapper mapper)
        {
            _improvementService = improvementService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _improvementService.GetAllVmAsync());
        }

        public IActionResult Add()
        {
            return View("Save", new ImprovementSaveViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(ImprovementSaveViewModel vm)
        {
            if (!ModelState.IsValid) return View("Save", vm);

            await _improvementService.AddAsync(vm);
            return RedirectToRoute(new { controller = "Improvement", action = "Index" });

        }

        public async Task<IActionResult> Update(int id)
        {
            return View("Save", await _improvementService.GetByIdVmAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(ImprovementSaveViewModel vm)
        {
            await _improvementService.UpdateAsync(vm, vm.Id);
            return RedirectToRoute(new { controller = "Improvement", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _improvementService.GetByIdVmAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ImprovementSaveViewModel vm)
        {
            await _improvementService.DeleteAsync(vm.Id);
            return RedirectToAction("Index");
        }
    }
}
