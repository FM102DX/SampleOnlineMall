using System;
using SampleOnlineMall.DataAccess.Models;
using SampleOnlineMall.DataAccess.Abstract;

namespace SampleOnlineMall.Core
{
    public class CommodityItem : BaseEntity, IBaseEntity
    {
        //mall commodityItem item
        public CommodityItem() : base()
        {
        }
        public string Name { get; set; }
        public string? Description { get; set; }
        public override string ToString()
        {
            return $"{Id} {Name} ";
        }
    }
}
