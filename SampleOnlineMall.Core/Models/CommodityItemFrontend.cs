using System;
using SampleOnlineMall.DataAccess.Models;
using SampleOnlineMall.DataAccess.Abstract;
using SampleOnlineMall.Core.Models;
using System.Collections.Generic;

namespace SampleOnlineMall.Core
{
    public class CommodityItemFrontend : CommodityItemBase
    {
        //mall commodityItem item
        public CommodityItemFrontend() : base()
        {}

        public IEnumerable<PictureInfo> Pictures { get; set; }

    }
}
