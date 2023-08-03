using Microsoft.AspNetCore.Mvc;
using SampleOnlineMall.Core;
using SampleOnlineMall.Core.Managers;
using SampleOnlineMall.Service;
using System;
using System.Collections.Generic;



namespace SampleOnlineMall
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssortmentController : Controller
    {
        public Serilog.ILogger _logger { get; set; }
        public CommodityItemManager _commodityItemManager { get; set; }

        public AssortmentController(CommodityItemManager commodityItemManager, Serilog.ILogger logger)
        {
            _logger = logger;
            _commodityItemManager= commodityItemManager;
        }

        [HttpDelete]
        [Route("deleteallcommodityitems/")]
        public async Task<IActionResult> DeleteAllCommodityItems()
        {
            var rezult = await _commodityItemManager.DeleteAll();
            if (rezult.Success)
            {
                return StatusCode(201, CommonOperationResult.SayOk());
            }
            else
            {
                return StatusCode(501, CommonOperationResult.SayFail(rezult.Message));
            }
        }


        [HttpPost]
        [Route("insertcommodityitem/")]
        public async Task<IActionResult> InsertCommodityItem([FromBody] CommodityItem commodityItem)
        {
            var rezult = await _commodityItemManager.InsertFromWebApi(commodityItem);
            if (rezult.Success)
            {
                return StatusCode(201, CommonOperationResult.SayOk());
            }
            else
            {
                return StatusCode(501, CommonOperationResult.SayFail(rezult.Message));
            }

        }
    }
}
