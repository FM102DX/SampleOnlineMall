using SampleOnlineMall.Core.Appilcation;
using SampleOnlineMall.DataAccess.Abstract;
using SampleOnlineMall.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using Shim= SixLabors.ImageSharp.Image;
using SixLabors.ImageSharp.Formats;
using SampleOnlineMall.Core.Mappers;
using SampleOnlineMall.Core.Models;

namespace SampleOnlineMall.Core.Managers
{
    //used to show items in frontend
    public class CommodityItemFrontendManager
    {
        private IAsyncRepository<CommodityItem> _repo;
        private Serilog.ILogger _logger;
        private SampleOnlineMallAssortmentApiApp _app;
        private WebLoggerManager _webLogMgr;
        private Mapper _mapper;
        public CommodityItemFrontendManager(IAsyncRepository<CommodityItem> repo, Serilog.ILogger logger, SampleOnlineMallAssortmentApiApp app, Mapper mapper, WebLoggerManager webLogMgr)
        {
            _repo = repo;
            _logger = logger;
            _app = app;
            _mapper = mapper;
            _webLogMgr = webLogMgr;
        }

        public async Task<IEnumerable<CommodityItemFrontend>> GetAll()
        {
            var items = (await _repo.GetAllAsync()).Select(x => _mapper.CommodityItemFrontendFromCommodityItem(x)).ToList() ;
            foreach (var item in items)
            {
                var x = GetPictureInfoListForItem(item);
                item.Pictures = x;
            }
            _webLogMgr.Log($"{items[0].Pictures.Count()}");
            return items;
        }

        private List<PictureInfo> GetPictureInfoListForItem(CommodityItemFrontend item)
        {
            var picLst = new List<PictureInfo>();
            
            for (int i = 1;i<=3;i++)
            {
                var pic = new PictureInfo();
                pic.BigPictureFullPath = $"{_app.BaseUrl}/CommodityItemImages/{item.Id}/{i}.jpg";
                pic.MediumPictureFullPath = $"{_app.BaseUrl}/CommodityItemImages/{item.Id}/{i}m.jpg";
                pic.SmallPictureFullPath = $"{_app.BaseUrl}/CommodityItemImages/{item.Id}/{i}s.jpg";
                picLst.Add(pic);
            }
            return picLst;
        }

        public async Task<int> Count()
        {
            return await _repo.GetCountAsync();
        }
        public async Task<CommodityItemFrontend> GetByIdOrNull(Guid id)
        {
            var item = _mapper.CommodityItemFrontendFromCommodityItem(await _repo.GetByIdOrNullAsync(id));
            item.Pictures = GetPictureInfoListForItem(item);
            return item;
        }
    }
}
