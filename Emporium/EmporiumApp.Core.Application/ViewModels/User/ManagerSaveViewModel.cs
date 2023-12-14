﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.ViewModels.User
{
    public class ManagerSaveViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string CardId { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Field is required")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords does not match")]
        [Required(ErrorMessage = "Confirm password")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.Text)]
        public string Email { get; set; }

        public int TypeUser { get; set; }
        public bool? HasError { get; set; }
        public string? Error { get; set; }
        public bool IsVerified { get; set; } = true;
    }
}
