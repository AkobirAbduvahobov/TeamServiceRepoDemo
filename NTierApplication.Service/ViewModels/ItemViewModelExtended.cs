﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierApplication.Service.ViewModels
{
    public class ItemViewModelExtended 
    {
        public long? ItemId { get; set; }
        public string ItemName { get; set; }
        public int ItemType { get; set; }
        public DateTime ItemDate { get; set; }
    }
}
