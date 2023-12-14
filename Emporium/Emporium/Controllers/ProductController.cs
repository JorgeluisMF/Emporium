using AutoMapper;
using EmporiumApp.Core.Application.Helpers;
using EmporiumApp.Core.Application.Interfaces.Services;
using EmporiumApp.Core.Application.ViewModels.Filters;
using EmporiumApp.Core.Application.ViewModels.Product;
using EmporiumApp.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace Emporium.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductTypeService _productTypeService;
        private readonly ISaleTypeService _saleTypeService;
        private readonly IImprovementService _improvementService;
        private readonly IMapper _mapper;
        private readonly IUploadFileService _uploadFileService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductImprovementService _propImproveSvc;

        public ProductController(
            IProductImprovementService propImproveSvc,
            IProductService productService,
            IProductTypeService productTypeService,
            ISaleTypeService saleTypeService,
            IImprovementService improvementService,
            IUserService userService,
            IUploadFileService uploadFileService,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _propImproveSvc = propImproveSvc;
            _productService = productService;
            _productTypeService = productTypeService;
            _saleTypeService = saleTypeService;
            _improvementService = improvementService;
            _httpContextAccessor = httpContextAccessor;
            _uploadFileService = uploadFileService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetAllVmAsync());
        }

        public async Task<IActionResult> AgentProducts(FiltersViewModel filters, string id)
        {
            UserViewModel user = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            var props = await _productService.GetAllWithFilters(filters);
            List<ProductViewModel> agentProps = new();

            if (id == null)
            {
                agentProps = props.Where(prop => prop.IdAgent == user.Id).ToList();
            }
            else
            {
                agentProps = props.Where(prop => prop.IdAgent == id).ToList();
            }

            ViewBag.ProductTypes = await _productTypeService.GetAllVmAsync();

            return View(agentProps);
        }

        public async Task<IActionResult> AddProduct()
        {
            ViewBag.ProductTypes = await _productTypeService.GetAllVmAsync();
            ViewBag.SaleTypes = await _saleTypeService.GetAllVmAsync();
            ViewBag.Improvements = await _improvementService.GetAllVmAsync();

            return View("Save", new ProductSaveViewModel());
        }

        [HttpPost]

        public async Task<IActionResult> AddProduct(ProductSaveViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProductTypes = await _productTypeService.GetAllVmAsync();
                ViewBag.SaleTypes = await _saleTypeService.GetAllVmAsync();
                ViewBag.Improvements = await _improvementService.GetAllVmAsync();
                return View("Save", vm);
            }

            UserViewModel user = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            vm.IdAgent = user.Id;
            vm.AgentName = $"{user.FirstName} {user.LastName}";
            vm.ProductImgUrl1 = "none";
            vm.ProductImgUrl2 = "none";
            vm.ProductImgUrl3 = "none";
            vm.ProductImgUrl4 = "none";
            ProductSaveViewModel prop = await _productService.AddAsync(vm);

            if (prop != null && prop.Id != 0)
            {
                string basePath = $"/Images/Products/{prop.Id}";
                prop.ProductImgUrl1 = _uploadFileService.UploadFile(vm.ProductImg1, basePath);
                prop.ProductImgUrl2 = _uploadFileService.UploadFile(vm.ProductImg2, basePath);
                prop.ProductImgUrl3 = _uploadFileService.UploadFile(vm.ProductImg3, basePath);
                prop.ProductImgUrl4 = _uploadFileService.UploadFile(vm.ProductImg4, basePath);

                await _productService.UpdateAsync(prop, prop.Id);
            }

            return RedirectToRoute(new { controller = "Product", action = "AgentProducts" });
        }

        public async Task<IActionResult> UpdateProduct(int id)
        {
            ViewBag.ProductTypes = await _productTypeService.GetAllVmAsync();
            ViewBag.SaleTypes = await _saleTypeService.GetAllVmAsync();
            ViewBag.Improvements = await _improvementService.GetAllVmAsync();

            return View("Save", await _productService.GetByIdVmAsync(id));
        }

        [HttpPost]

        public async Task<IActionResult> UpdateProduct(ProductSaveViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProductTypes = await _productTypeService.GetAllVmAsync();
                ViewBag.SaleTypes = await _saleTypeService.GetAllVmAsync();
                ViewBag.Improvements = await _improvementService.GetAllVmAsync();
                return View("Save", vm);
            }

            ProductSaveViewModel propVm = await _productService.GetByIdVmAsync(vm.Id);

            string basePath = $"/Images/Products/{propVm.Id}";

            vm.ProductImgUrl1 = _uploadFileService.UploadFile(vm.ProductImg1, basePath, true, propVm.ProductImgUrl1);
            vm.ProductImgUrl2 = _uploadFileService.UploadFile(vm.ProductImg2, basePath, true, propVm.ProductImgUrl2);
            vm.ProductImgUrl3 = _uploadFileService.UploadFile(vm.ProductImg3, basePath, true, propVm.ProductImgUrl3);
            vm.ProductImgUrl4 = _uploadFileService.UploadFile(vm.ProductImg4, basePath, true, propVm.ProductImgUrl4);

            await _productService.UpdateAsync(vm, vm.Id);
            return RedirectToRoute(new { controller = "Product", action = "AgentProducts" });
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            return View(await _productService.GetByIdVmAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(ProductSaveViewModel vm)
        {
            await _productService.DeleteAsync(vm.Id);

            //get directory path
            string basePath = $"/Images/Products/{vm.Id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //Create a folder if not exist
            if (Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new(path);
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo folder in directoryInfo.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(path);
            }
            return RedirectToRoute(new { controller = "Product", action = "AgentProducts" });
        }
    }
}