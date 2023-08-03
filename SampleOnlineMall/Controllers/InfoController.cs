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
    public class InfoController : Controller
    {
        public Serilog.ILogger _logger { get; set; }
        public CommodityItemManager _commodityItemManager { get; set; }

        public InfoController(CommodityItemManager commodityItemManager, Serilog.ILogger logger)
        {
            _logger = logger;
            _commodityItemManager= commodityItemManager;
        }

        [HttpGet]
        public async Task<string> Info()
        {
            int cnt = await _commodityItemManager.Count();
            var positions = string.Join("<br />", _commodityItemManager.GetAll().Result.ToList().Select(x => $"{x.Name}"));
            return $"This is AssortmentController. Now {cnt} positions in assortment {positions}";
        }
    }
}
