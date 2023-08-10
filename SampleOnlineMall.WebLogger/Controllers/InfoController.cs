using Microsoft.AspNetCore.Mvc;
using SampleOnlineMall.Core;
using SampleOnlineMall.Core.Managers;
using SampleOnlineMall.Service;
using System;
using System.Collections.Generic;


namespace SampleOnlineMall.WebLogger
{
    [ApiController]
    [Route("")]
    public class InfoController : Controller
    {
        public Serilog.ILogger _logger { get; set; }
        public WebLoggerMessageManager _itemManager { get; set; }

        public InfoController(WebLoggerMessageManager itemManager, Serilog.ILogger logger)
        {
            _logger = logger;
            _itemManager= itemManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new WebLoggerMessagesDisplayViewModel
            {
                ItemList = (await _itemManager.GetAll()).ToList(),
                CssPath = @"/css/style.css",
                SwaggerFullPath = @$"{HttpContext.Request.Path}/swagger/index.html"
            };
            return View("LoggerBase", viewModel);
        }
    }
}
