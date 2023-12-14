﻿using EmporiumApp.Core.Application.Enums;
using EmporiumApp.Infraestructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Infraestructure.Identity.Seeds
{
    public static class DefaultAdminUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultUser = new();
            //defaultUser.CardId = "40211056599";
            defaultUser.UserName = "Joeldoe";
            defaultUser.Email = "DefaultAdminUser@gmail.com";
            defaultUser.FirstName = "Joel";
            defaultUser.LastName = "Doe";
            defaultUser.IsVerified = true;
            defaultUser.EmailConfirmed = true;
            defaultUser.PhoneNumberConfirmed = true;
            defaultUser.TypeUser = (int)Roles.Admin;

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Agent.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Client.ToString());
                }
            }
        }
    }
}
