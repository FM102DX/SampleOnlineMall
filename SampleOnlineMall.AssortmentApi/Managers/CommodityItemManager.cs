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

namespace SampleOnlineMall.Core.Managers
{
    public class CommodityItemManager
    {
        private IAsyncRepository<CommodityItem> _repo;
        private Serilog.ILogger _logger;
        private SampleOnlineMallAssortmentApiApp _app;
        private Mapper _mapper;
        public CommodityItemManager(IAsyncRepository<CommodityItem> repo, Serilog.ILogger logger, SampleOnlineMallAssortmentApiApp app, Mapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _app = app;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CommodityItem>> GetAll()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<int> Count()
        {
            return await _repo.GetCountAsync();
        }

        public async Task<CommonOperationResult> DeleteItemById(Guid id)
        {
            try
            {
                await _repo.DeleteAsync(id);
                var imgPath = _app.GetCommodityItemImageDirectoryFromGuid(id);
                var exists = Directory.Exists(imgPath);
                _logger.Information($"trying to delete {imgPath}, it exists: {exists}");
                if (exists)
                {
                    Directory.Delete(imgPath,true);
                }
                return CommonOperationResult.SayOk();
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}");
                return CommonOperationResult.SayFail($"{ex.Message}");
            }
        }

        public async Task<CommonOperationResult> DeleteAll()
        {
            try
            {
                var itemList = await _repo.GetAllAsync();
                var itemGuidArray = itemList.ToList().Select(x => x.Id).ToArray();
                foreach(var id in itemGuidArray)
                {
                    await DeleteItemById(id);
                }
                return CommonOperationResult.SayOk();
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}");
                return CommonOperationResult.SayFail($"{ex.Message}");

            }
        }

        public async Task<CommonOperationResult> InsertFromWebApi (CommodityItemApiFeed item)
        {
            _logger.Information($"This is ItemManager. Received commodity item name={item.Name}");

            try
            {

                //saving object
                var convertedItem = _mapper.CommodityItemFromCommodityItemApiFeed(item);
                var saveRez = await _repo.AddAsync(convertedItem);

                if(!saveRez.Success) return CommonOperationResult.SayFail(saveRez.Message);

                // saving pictures
                byte[] imageByteArr;
                var picDirectory = _app.GetCommodityItemImageDirectoryFromGuid(item.Id);
                
                var firstPicPath = Path.Combine(picDirectory, "1.jpg");
                var secondPicPath = Path.Combine(picDirectory, "2.jpg");
                var thirdPicPath = Path.Combine(picDirectory, "3.jpg");

                var firstPicPathM = Path.Combine(picDirectory, "1m.jpg");
                var secondPicPathM = Path.Combine(picDirectory, "2m.jpg");
                var thirdPicPathM = Path.Combine(picDirectory, "3m.jpg");

                var firstPicPathS = Path.Combine(picDirectory, "1s.jpg");
                var secondPicPathS = Path.Combine(picDirectory, "2s.jpg");
                var thirdPicPathS= Path.Combine(picDirectory, "3s.jpg");

                //pic1
                imageByteArr = Convert.FromBase64String(item.FirstPic);
                _logger.Information($"item.FirstPic {item.FirstPic.Length}");
                File.WriteAllBytes(firstPicPath, imageByteArr);

                //pic2
                imageByteArr = Convert.FromBase64String(item.SecondPic);
                _logger.Information($"item.SecondPic {item.SecondPic.Length}");
                File.WriteAllBytes(secondPicPath, imageByteArr);

                //pic3
                imageByteArr = Convert.FromBase64String(item.ThirdPic);
                _logger.Information($"item.ThirdPic {item.ThirdPic.Length}");
                File.WriteAllBytes(thirdPicPath, imageByteArr);

                item.FirstPic = null;
                item.SecondPic = null;
                item.ThirdPic = null;

                ResizeImageToWidthAndSave(firstPicPath, firstPicPathM, 700);
                ResizeImageToWidthAndSave(secondPicPath, secondPicPathM, 700);
                ResizeImageToWidthAndSave(thirdPicPath, thirdPicPathM, 700);

                ResizeImageToWidthAndSave(firstPicPath, firstPicPathS, 300);
                ResizeImageToWidthAndSave(secondPicPath, secondPicPathS, 300);
                ResizeImageToWidthAndSave(thirdPicPath, thirdPicPathS, 300);




                return CommonOperationResult.SayOk();

            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}");
                return CommonOperationResult.SayFail($"Ex={ex.Message} InnerEx={ex.InnerException}");

            }
        }
        public static void ResizeImageToWidthAndSave(string source, string target, int targetWidth)
        {
            var img = Shim.Load(source);
            var format = img.Metadata.DecodedImageFormat;
            var resizedImage = ResizeImageToWidth(img, targetWidth);
            var memoryStream = new MemoryStream();
            resizedImage.Save(memoryStream, format);
            File.WriteAllBytes(target, memoryStream.ToArray());
        }


        public static Shim ResizeImageToWidth(Shim source, int targetWidth)
        {
            var ratio = source.Width / targetWidth;
            var targetHeight = source.Height / ratio;
            SixLabors.ImageSharp.Size targetSize = new SixLabors.ImageSharp.Size(targetWidth, targetHeight);
            source.Mutate(x => x.Resize(targetSize));
            return source;
        }

    }
}
