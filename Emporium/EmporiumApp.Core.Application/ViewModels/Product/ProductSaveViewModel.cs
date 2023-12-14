using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.ViewModels.Product
{
    public class ProductSaveViewModel
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        [Required(ErrorMessage = "Debe especificar la ubicacion de la propiedad")]
        public string? Ubication { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe especificar el tipo de propiedad")]
        public int ProductTypeId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe especificar el tipo de venta")]
        public int SaleTypeId { get; set; }

        //public int ImprovementId { get; set; }
        public List<int> ListImprovement { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe especificar el precio de venta")]
        public double Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe especificar el tamaño de la propiedad en metros")]
        public double ParcelSize { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe especificar la cantidad de habitaciones")]
        public int RoomQty { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe especificar la cantidad de baños")]
        public int RestRoomQty { get; set; }

        [Required(ErrorMessage = "Debe especificar la descripcion de la propiedad")]
        public string? Description { get; set; }

        public string? AgentName { get; set; }
        public string? IdAgent { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? ProductImg1 { get; set; }
        public string? ProductImgUrl1 { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? ProductImg2 { get; set; }
        public string? ProductImgUrl2 { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? ProductImg3 { get; set; }
        public string? ProductImgUrl3 { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? ProductImg4 { get; set; }
        public string? ProductImgUrl4 { get; set; }
        public DateTime Created { get; set; }
        public string? CreatedBy { get; set; }
    }
}
