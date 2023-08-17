using SampleOnlineMall.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleOnlineMall.DataAccess.Models
{
    public class RepositoryRequest<T>: IRepositoryRequest<T> where T : IBaseEntity
    {
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
        public ISearchOptions<T> SearchOptions { get; set; }
        public bool UsePagination { get; set; }
    }
}
