using SampleOnlineMall.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleOnlineMall.DataAccess.Models
{
    public class RepositoryRequestFuncSearch<T> where T : IBaseEntity
    {
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
        public Func<T,bool> SearchFunc { get; set; }
        public bool UsePagination { get; set; }
        public bool UseSearch { get; set; }

        public static RepositoryRequestFuncSearch<T> FromTextSearchRequest (RepositoryRequestTextSearch sourceReqest)
        {
            var targetReqest = new RepositoryRequestFuncSearch<T>();
            targetReqest.ItemsPerPage = sourceReqest.ItemsPerPage;
            targetReqest.UseSearch = sourceReqest.UseSearch;
            targetReqest.UsePagination = sourceReqest.UsePagination;
            targetReqest.Page = sourceReqest.Page;
            return targetReqest;
        }
    }
}
