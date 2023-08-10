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

        public async void Log (string text)
        {
            var msg = new WebLoggerMessage() { Message = text, Sender = _sender };
            await _repo.AddAsync(msg);
        }
    }
}
