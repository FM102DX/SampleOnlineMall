using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleOnlineMall.DataAccess.Models
{
    public class ClientToApiPaginatedRequest
    {
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
