﻿using EmporiumApp.Core.Application.ViewModels.Improvement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.ViewModels.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Ubication { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductType { get; set; }
        public int SaleTypeId { get; set; }
        public string SaleType { get; set; }
        public double Price { get; set; }
        public double ParcelSize { get; set; }
        public int RoomQty { get; set; }
        public int RestRoomQty { get; set; }
        public string Description { get; set; }

        public string AgentName { get; set; }
        public string IdAgent { get; set; }

        public string ProductImgUrl1 { get; set; }
        public string ProductImgUrl2 { get; set; }
        public string ProductImgUrl3 { get; set; }
        public string ProductImgUrl4 { get; set; }

        public bool IsFavourite { get; set; } = false;

        public DateTime Created { get; set; }
        public List<ImprovementViewModel> Improvements { get; set; }
    }
}
