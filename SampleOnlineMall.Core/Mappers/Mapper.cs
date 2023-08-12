using SampleOnlineMall.Core.Models;
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
            var newItem = new CommodityItem();
            newItem.Id = item.Id;
            newItem.Name = item.Name;
            newItem.Description = item.Description;
            return newItem;
        }

        public CommodityItemFrontend CommodityItemFrontendFromCommodityItem(CommodityItem item)
        {
            var newItem = new CommodityItemFrontend();
            newItem.Id = item.Id;
            newItem.Name = item.Name;
            newItem.Description = item.Description;
            newItem.Pictures = new List<PictureInfo>();
            return newItem;

        }
        public CommodityItemFrontendDisplayed CommodityItemFrontendDisplayedFromTransport (CommodityItemFrontend item)
        {
            var newItem = new CommodityItemFrontendDisplayed();
            newItem.Id = item.Id;
            newItem.Name = item.Name;
            newItem.Description = item.Description;
            newItem.Pictures = item.Pictures.ToList();
            return newItem;
        }


        
    }
}
