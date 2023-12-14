using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Domain.Entities
{
    public class ProductImprovement
    {
        public int Id { get; set; }
        public int PropId { get; set; }
        public int ImprovementId { get; set; }
    }
}
