﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Dtos.Account
{
    public class UpdateRequest
    {
        public string Id { get; set; }
        public string CardId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsVerified { get; set; } = false;
        public string ProfilePicture { get; set; }
        public int TypeUser { get; set; }
    }
}
