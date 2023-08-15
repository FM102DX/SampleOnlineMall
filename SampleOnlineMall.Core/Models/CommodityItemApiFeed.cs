using System;
using SampleOnlineMall.DataAccess.Models;
using SampleOnlineMall.DataAccess.Abstract;

namespace SampleOnlineMall.Core
{
    public class CommodityItemApiFeed : CommodityItemBase
    {
        //mall commodityItem item
        public CommodityItemApiFeed() : base()
        {

        }
        public string? FirstPic { get; set; }
        public string? SecondPic { get; set; }
        public string? ThirdPic { get; set; }
    }
}
