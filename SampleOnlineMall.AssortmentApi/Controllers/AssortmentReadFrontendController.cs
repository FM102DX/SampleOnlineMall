using Microsoft.AspNetCore.Mvc;
using SampleOnlineMall.Core;
using SampleOnlineMall.Core.Managers;
using SampleOnlineMall.Service;
using System;
using System.Collections.Generic;



namespace SampleOnlineMall
{
    [ApiController]
    [Route("")]
    public class AssortmentReadFrontend : Controller
    {
        public Serilog.ILogger _logger { get; set; }
        public CommodityItemFrontendManager _itemManager { get; set; }
        public WebLoggerManager _webLoggerManager { get; set; }
        public AssortmentReadFrontend(CommodityItemFrontendManager itemManager, WebLoggerManager webLoggerManager, Serilog.ILogger logger)
        {
            _logger = logger;
            _itemManager= itemManager;
            _webLoggerManager= webLoggerManager;
        }

        [HttpGet]
        [Route("getall/")]
        public async Task<IEnumerable<CommodityItemFrontend>> GetAllItems()
        {
            return await _itemManager.GetAll();
        }

        [HttpGet]
        [Route("search/{searchText}")]
        public async Task<IEnumerable<CommodityItemFrontend>> SearchFrontednAssort(string searchText)
        {
            return await _itemManager.Search(searchText.ToLower());
        }

        [HttpGet]
        [Route("GetByIdOrNull/{id}")]
        public async Task<CommodityItemFrontend> GetByIdOrNull(Guid id)
        {
            return await _itemManager.GetByIdOrNull(id);
        }
    }
}
