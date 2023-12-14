﻿using AutoMapper;
using EmporiumApp.Core.Application.Dtos.Account;
using EmporiumApp.Core.Application.Enums;
using EmporiumApp.Core.Application.Interfaces.Repositories;
using EmporiumApp.Core.Application.Interfaces.Services;
using EmporiumApp.Core.Application.ViewModels.Filters;
using EmporiumApp.Core.Application.ViewModels.User;
using EmporiumApp.Core.Domain.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IProductRepository _propertyRepository;
        private readonly IMapper _mapper;

        public UserService(IAccountService accountService, IProductRepository propertyRepository, IMapper mapper)
        {
            _accountService = accountService;
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<List<UserViewModel>> GetAllVmAsync()
        {
            var users = await this.GetAllUsersAsync();
            var usersVm = _mapper.Map<List<UserViewModel>>(users);

            return usersVm;
        }
        public async Task<List<UserViewModel>> GetAllAgentsWithFilters(FiltersViewModel filters)
        {
            var users = await this.GetAllUsersAsync();
            var usersVm = _mapper.Map<List<UserViewModel>>(users);

            usersVm = usersVm.Where(user => user.TypeUser == (int)Roles.Agent).ToList();

            if (!string.IsNullOrWhiteSpace(filters.name))
            {
                List<UserViewModel> vm = usersVm.Where(user => user.FirstName.Trim().ToLower() == filters.name.Trim().ToLower()).ToList();
                return vm;
            }

            return usersVm;
        }
        public async Task<List<AuthenticationResponse>> GetAllUsersAsync()
        {
            List<AuthenticationResponse> users = await _accountService.GetAllUsers();
            return users;
        }
        public List<AuthenticationResponse> GetAllUsers()
        {
            List<AuthenticationResponse> users = _accountService.GetAll();
            return users;
        }
        public async Task<UserSaveViewModel> GetUserByIdAsync(string id)
        {
            AuthenticationResponse user = await _accountService.GetUserById(id);
            UserSaveViewModel userMap = _mapper.Map<UserSaveViewModel>(user);
            return userMap;
        }
        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel vm)
        {
            AuthenticationRequest request = _mapper.Map<AuthenticationRequest>(vm);
            AuthenticationResponse response = await _accountService.AuthenticationAsync(request);
            return response;
        }
        public async Task<AuthenticationResponse> RegisterAsync(UserSaveViewModel vm, string origin)
        {
            RegisterBasicRequest request = _mapper.Map<RegisterBasicRequest>(vm);
            return await _accountService.RegisterBasicUserAsync(request, origin);
        }

        public async Task<RegisterManagerResponse> RegisterAdminAsync(ManagerSaveViewModel vm)
        {
            RegisterManagerRequest request = _mapper.Map<RegisterManagerRequest>(vm);
            return await _accountService.RegisterAdminUserAsync(request);
        }

        public async Task<RegisterManagerResponse> RegisterDevAsync(ManagerSaveViewModel vm)
        {
            RegisterManagerRequest request = _mapper.Map<RegisterManagerRequest>(vm);
            var devRequest = _mapper.Map<RegisterManagerDevRequest>(request);
            return await _accountService.RegisterDevUserAsync(devRequest);
        }

        public async Task<UpdateResponse> UpdateUserAsync(UserSaveViewModel vm, string id)
        {
            UpdateRequest req = _mapper.Map<UpdateRequest>(vm);
            return await _accountService.UpdateUserAsync(req, id);
        }

        public async Task<UpdateResponse> UpdateAgentAsync(UpdateAgentViewModel vm)
        {
            return await _accountService.UpdateAgentAsync(vm);
        }

        public async Task<UpdateResponse> ActivedUserAsync(string id)
        {
            return await _accountService.ActivedUserAsync(id);
        }
        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
            return await _accountService.ConfirmAccountAsync(userId, token);
        }
        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel vm)
        {
            ResetPasswordRequest request = _mapper.Map<ResetPasswordRequest>(vm);
            return await _accountService.ResetPasswordAsync(request);
        }
        public async Task SignOutAsync()
        {
            await _accountService.SignOutAsync();
        }

        public async Task DeleteUserAsync(string id)
        {
            //Eliminando las propiedades del agente.
            List<Product> propertyList = await _propertyRepository.GetAllAsync();
            foreach (Product item in propertyList)
            {
                if (item.IdAgent == id)
                {
                    await _propertyRepository.DeleteAsync(item);
                }
            }
            await _accountService.DeleteUserAsync(id);
        }
    }
}
