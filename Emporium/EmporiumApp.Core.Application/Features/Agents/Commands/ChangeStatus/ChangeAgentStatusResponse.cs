using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Features.Agents.Commands.ChangeStatus
{
    public class ChangeAgentStatusResponse
    {
        public string Id { get; set; }
        public bool Status { get; set; }
    }
}
