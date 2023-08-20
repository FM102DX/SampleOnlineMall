using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleOnlineMall.Core.Models
{
    public class PictureInfo
    {
        public string BigPictureFullPath { get; set; }
        public string MediumPictureFullPath { get; set; }
        public string SmallPictureFullPath { get; set; }

        public PictureInfo Clone()
        {
            PictureInfo pictureInfo = new PictureInfo();
            pictureInfo.BigPictureFullPath = BigPictureFullPath;
            pictureInfo.MediumPictureFullPath = MediumPictureFullPath;
            pictureInfo.SmallPictureFullPath= SmallPictureFullPath;
            return pictureInfo;
        }
    }
}
