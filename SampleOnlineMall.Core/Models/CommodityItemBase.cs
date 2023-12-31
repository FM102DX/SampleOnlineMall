﻿using System;
using SampleOnlineMall.DataAccess.Models;
using SampleOnlineMall.DataAccess.Abstract;

namespace SampleOnlineMall.Core
{
    public class CommodityItemBase : BaseEntity, IBaseEntity
    {
        //mall commodityItem item
        public CommodityItemBase() : base() {  }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public override string ToString()
        {
            return $"{Id} {Name} ";
        }
        public Guid SupplierId { get; set; }
    }
}
