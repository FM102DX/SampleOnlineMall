using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using SampleOnlineMall.FrontEnd.Blazor.Components.ImageSelector;
using System.Collections;
using SampleOnlineMall.FrontEnd.Blazor.Data;
using SampleOnlineMall.Core;
using SampleOnlineMall.Core.Models;

namespace T109.ActiveDive.FrontEnd.Blazor.Pages
          
{
    public partial class Item : ComponentBase
    {

        [Inject]
        public StoreManager Manager { get; set; }

        [Inject]
        public Serilog.ILogger Logger { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Parameter]
        public Guid ItemId { get; set; }

        public List<SelectableImage> ImgList { get; set; }= new List<SelectableImage>();

        public CommodityItemFrontend ActualItem { get; set; } = new CommodityItemFrontend();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ActualItem = await Manager.GetByIdOrNullAsync(ItemId);

                List<PictureInfo> pictureinfo = ActualItem.Pictures.ToList();

                for (int i = 0; i < pictureinfo.Count; i++)
                {
                    ImgList.Add(new SelectableImage() { Id = 0, FullSizePath = pictureinfo[i].MediumPictureFullPath, MidSizePath = pictureinfo[i].MediumPictureFullPath, ThumbPath = pictureinfo[i].SmallPictureFullPath });
                }
                
                //SetCurrentImage();

                //SmallImgSelector.MySelectionChanged += SmallImgSelector_MySelectionChanged;
            }
            catch (Exception ex)
            {
                Logger.Error("GetItemError" + ex.Message);
            }
        }

        public string CurrentImageFullPath { get; set; }

    }
}
