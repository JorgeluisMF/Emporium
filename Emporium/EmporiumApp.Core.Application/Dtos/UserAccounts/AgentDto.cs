using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Dtos.UserAccounts
{
    public class AgentDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PropQty { get; set; }
        public string EMail { get; set; }
        public string PhoneNumber { get; set; }
    }
}
