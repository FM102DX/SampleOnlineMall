using System;
using SampleOnlineMall.DataAccess.Models;
using SampleOnlineMall.DataAccess.Abstract;
using SampleOnlineMall.Core.Models;
using System.Collections.Generic;

namespace SampleOnlineMall.Core
{
    public class CommodityItemFrontend : BaseEntity, IBaseEntity
    {
        //mall commodityItem item
        public CommodityItemFrontend() : base()
        {
        }
        public string Name { get; set; }
        public string? Description { get; set; }

        public IEnumerable<PictureInfo> Pictures { get; set; }

    }
}
