using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Dtos.Account
{
    public class RegisterManagerRequest
    {
        public string CardId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        [JsonIgnore]
        public bool IsVerified { get; set; } = true;
    }
}
