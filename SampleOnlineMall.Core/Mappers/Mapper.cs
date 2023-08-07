using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleOnlineMall.Core.Mappers
{
    public class Mapper
    {
        public CommodityItem CommodityItemFromCommodityItemApiFeed(CommodityItemApiFeed item)
        {
            CommodityItem newItem = new CommodityItem();
            newItem.Id= item.Id;
            newItem.Name = item.Name;
            newItem.Description= item.Description;
            return newItem;
        }
    }
}
