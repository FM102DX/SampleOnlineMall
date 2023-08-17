using SampleOnlineMall.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleOnlineMall.DataAccess.Models
{
    public class RepositoryResponce<T>: IRepositoryResponce<T> where T : IBaseEntity
    {
        public int TotlaCount { get; set; }
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
        public IEnumerable<T> Items{ get; set; }
    }
}
