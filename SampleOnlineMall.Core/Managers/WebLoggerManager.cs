using SampleOnlineMall.Core.Models;
using SampleOnlineMall.DataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleOnlineMall.Core.Managers
{
    public  class WebLoggerManager
    {
        //this is class to use web logger

        private WebApiAsyncRepository<WebLoggerMessage> _repo;
        private string _sender;
        private Serilog.ILogger _logger
        {
            get
            {
                return _loggerOptions.Logger;
            }
        }
        private WebApiAsyncRepositoryOptions _loggerOptions;

        public WebLoggerManager(string sender, WebApiAsyncRepositoryOptions loggerOptions)
        {
            _loggerOptions = loggerOptions;
            _sender = sender;
            _repo = new WebApiAsyncRepository<WebLoggerMessage>(_loggerOptions);
        }

        public async void Log (string text, WebLoggerLogLevel logLevel= WebLoggerLogLevel.Information)
        {
            var msg = new WebLoggerMessage();
            msg.Message = text;
            msg.Sender = _sender;
            msg.LogLevel = logLevel;
            await _repo.AddAsync(msg);
        }
        public async void Information(string text)
        {
            Log(text, WebLoggerLogLevel.Information);
        }
        public async void Error(string text)
        {
            Log(text, WebLoggerLogLevel.Error);
        }
        public async void Debug(string text)
        {
            Log(text, WebLoggerLogLevel.Debug);
        }
    }
}
