using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.ViewModels.ProductType
{
    public class ProductTypeSaveViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "File required")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required(ErrorMessage = "File required")]
        [DataType(DataType.Text)]
        public string Description { get; set; }
    }
}
