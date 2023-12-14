using AutoMapper;
using EmporiumApp.Core.Application.Enums;
using EmporiumApp.Core.Application.Helpers;
using EmporiumApp.Core.Application.Interfaces.Services;
using EmporiumApp.Core.Application.Services;
using EmporiumApp.Core.Application.ViewModels.Filters;
using EmporiumApp.Core.Application.ViewModels.Product;
using EmporiumApp.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace Emporium.Controllers
{
    public class AgentController : Controller
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductTypeService _productTypeService;
        private readonly IMapper _mapper;
        private readonly IUploadFileService _uploadFileService;

        public AgentController(
            IProductService productService,
            IUserService userService,
            IProductTypeService productTypeService,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IUploadFileService uploadFileService)
        {
            _productService = productService;
            _productTypeService = productTypeService;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _mapper = mapper;
            _uploadFileService = uploadFileService;
        }
        public async Task<IActionResult> Index(FiltersViewModel filters)
        {
            UserViewModel user = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            List<ProductViewModel> properties = await _productService.GetAllWithFilters(filters);
            properties = properties.Where(prop => prop.IdAgent == user.Id).ToList();

            ViewBag.ProductTypes = await _productTypeService.GetAllVmAsync();

            return View(properties);
        }
        public async Task<IActionResult> AgentList()
        {
            List<UserViewModel> users = await _userService.GetAllVmAsync();
            users = users.Where(user => user.TypeUser == (int)Roles.Agent).ToList();

            var listUsers = users.Select(user => new UserViewModel()
            {
                Id = user.Id,
                CardId = user.CardId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                ProfilePicture = user.ProfilePicture,
                HasError = user.HasError,
                Error = user.Error,
                IsVerified = user.IsVerified,
                PropQty = ProductQuantity(user.Id)
            }).ToList();

            return View(listUsers);
        }
        private int ProductQuantity(string agentId)
        {
            var listAgent = _userService.GetAllUsers();
            var agent = listAgent.FirstOrDefault(x => x.Id == agentId);

            var listProps = _productService.GetAll();
            var propsFiltered = listProps.Where(x => x.IdAgent == agentId);
            int result = propsFiltered.Count();
            return result;
        }
        public async Task<IActionResult> Agents(FiltersViewModel filters)
        {
            var agents = await _userService.GetAllAgentsWithFilters(filters);
            agents = agents.Where(user => user.IsVerified).OrderBy(user => user.FirstName).ToList();
            return View(agents);
        }
        public async Task<IActionResult> ActiveUser(string id)
        {
            return View("ActiveUser", await _userService.GetUserByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> ActiveUser(UserSaveViewModel vm)
        {
            await _userService.ActivedUserAsync(vm.Id);
            return RedirectToRoute(new { controller = "Agent", action = "AgentList" });
        }
        public async Task<IActionResult> MyProfile()
        {
            UserViewModel user = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            UserSaveViewModel userVm = await _userService.GetUserByIdAsync(user.Id);
            UpdateAgentViewModel agentVm = _mapper.Map<UpdateAgentViewModel>(userVm);
            return View("Update", agentVm);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAgent(UpdateAgentViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Update", vm);
            }
            UserSaveViewModel userVm = await _userService.GetUserByIdAsync(vm.Id);

            string basePath = $"/Images/Users/{userVm.Id}";
            vm.ProfilePicture = _uploadFileService.UploadFile(vm.ProfilePictureFile, basePath, true, userVm.ProfilePicture);


            var response = await _userService.UpdateAgentAsync(vm);

            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View("Update", vm);
            }

            return RedirectToRoute(new { controller = "Agent", action = "Index" });
        }

        public async Task<IActionResult> Delete(string id)
        {
            return View(await _userService.GetUserByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UserSaveViewModel vm)
        {
            await _userService.DeleteUserAsync(vm.Id);
            return RedirectToRoute(new { controller = "Agent", action = "AgentList" });
        }
    }
}
