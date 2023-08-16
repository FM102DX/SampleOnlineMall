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
            newItem.Price = item.Price;
            newItem.SupplierId = item.SupplierId;
            return newItem;
        }

        public CommodityItemFrontend CommodityItemFrontendFromCommodityItem(CommodityItem item)
        {
            var newItem = new CommodityItemFrontend();
            newItem.Id = item.Id;
            newItem.Name = item.Name;
            newItem.Price = item.Price;
            newItem.Description = item.Description;
            newItem.Pictures = new List<PictureInfo>();
            newItem.SupplierId = item.SupplierId;
            return newItem;

        }
        public CommodityItemFrontendDisplayed CommodityItemFrontendDisplayedFromTransport (CommodityItemFrontend item)
        {
            var newItem = new CommodityItemFrontendDisplayed();
            newItem.Id = item.Id;
            newItem.Name = item.Name;
            newItem.Price= item.Price;
            newItem.Description = item.Description;
            newItem.Pictures = item.Pictures.ToList();
            newItem.SupplierId = item.SupplierId;
            newItem.Supplier = item.Supplier;
            return newItem;
        }


        
    }
}
