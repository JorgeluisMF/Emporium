﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Dtos.Account
{
    public class RegisterManagerResponse
    {
        public bool HasError { get; set; }
        public string Error { get; set; }
    }
}
