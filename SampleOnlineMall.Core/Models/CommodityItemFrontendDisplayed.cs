using System;
using SampleOnlineMall.DataAccess.Models;
using SampleOnlineMall.DataAccess.Abstract;
using SampleOnlineMall.Core.Models;
using System.Collections.Generic;

namespace SampleOnlineMall.Core
{
    public class CommodityItemFrontendDisplayed : CommodityItemBase
    {
        //mall commodityItem item that we show on site
        public CommodityItemFrontendDisplayed() : base()
        {
        }
        public PictureInfo FacePicture
        {
            get
            {
                if (Pictures.Count > 0)
                {
                    return Pictures[0];
                }
                return new PictureInfo();
            }
        }

        public List<PictureInfo> Pictures { get; set; }

    }
}
