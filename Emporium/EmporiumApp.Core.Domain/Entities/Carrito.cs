using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Domain.Entities
{
    public class Carrito
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public int ProductId { get; set; }
    }
}
