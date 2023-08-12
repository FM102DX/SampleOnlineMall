using System;
using SampleOnlineMall.DataAccess.Models;
using SampleOnlineMall.DataAccess.Abstract;

namespace SampleOnlineMall.Core
{
    public class WebLoggerMessage : BaseEntity, IBaseEntity
    {
        public WebLoggerMessage() : base()
        {

        }
        public string Sender { get; set; }
        public string? Message { get; set; }
    }
}
