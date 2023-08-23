using SampleOnlineMall.DataAccess.Models;

namespace WebLogger.Blazor.Core.Models
{
    public class WebLoggerSettings:BaseEntity
    {
        public int RefreshPeriodMs { get; set; }

        public int ItemsPerPage { get; set; }

        public TimeSpan LogsKeepingPeriod { get; set; }

    }
}
