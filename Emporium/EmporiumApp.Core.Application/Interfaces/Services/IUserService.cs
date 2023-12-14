﻿using EmporiumApp.Core.Application.Dtos.Account;
using EmporiumApp.Core.Application.ViewModels.Filters;
using EmporiumApp.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<UserViewModel>> GetAllVmAsync();
        Task<List<UserViewModel>> GetAllAgentsWithFilters(FiltersViewModel filters);
        Task<List<AuthenticationResponse>> GetAllUsersAsync();
        List<AuthenticationResponse> GetAllUsers();
        Task<UserSaveViewModel> GetUserByIdAsync(string id);
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task<AuthenticationResponse> RegisterAsync(UserSaveViewModel vm, string origin);
        Task<RegisterManagerResponse> RegisterAdminAsync(ManagerSaveViewModel vm);
        Task<RegisterManagerResponse> RegisterDevAsync(ManagerSaveViewModel vm);
        Task<UpdateResponse> UpdateUserAsync(UserSaveViewModel vm, string id);
        Task<UpdateResponse> UpdateAgentAsync(UpdateAgentViewModel vm);
        Task<UpdateResponse> ActivedUserAsync(string id);
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel vm);
        Task SignOutAsync();
        Task DeleteUserAsync(string id);
    }
}
