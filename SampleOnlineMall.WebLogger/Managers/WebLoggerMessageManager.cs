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

namespace SampleOnlineMall.Core.Managers
{
    public class WebLoggerMessageManager
    {
        private IAsyncRepository<WebLoggerMessage> _repo;
        private Serilog.ILogger _logger;
        private SampleOnlineMallWebLoggerApp _app;
        public WebLoggerMessageManager(IAsyncRepository<WebLoggerMessage> repo, Serilog.ILogger logger, SampleOnlineMallWebLoggerApp app)
        {
            _repo = repo;
            _logger = logger;
            _app = app;
        }

        public async Task<IEnumerable<WebLoggerMessage>> GetAll()
        {
            return await _repo.GetAllAsync();
        }
        public async Task<IEnumerable<WebLoggerMessage>> GetAllBySender()
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

        public async Task<CommonOperationResult> InsertFromWebApi (WebLoggerMessage item)
        {
            _logger.Information($"This is ItemManager. Received commodity item id={item.Id}");
            try
            {
                //saving object
                var saveRez = await _repo.AddAsync(item);
                if (!saveRez.Success)
                {
                    return CommonOperationResult.SayFail(saveRez.Message);
                }
                // saving pictures
                return CommonOperationResult.SayOk();
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}");
                return CommonOperationResult.SayFail($"Ex={ex.Message} InnerEx={ex.InnerException}");
            }
        }
    }
}
