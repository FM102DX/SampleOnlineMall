using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SampleOnlineMall.Service;

namespace SampleOnlineMall.DataAccess.Abstract
{
    public interface ISearchOptions<T> where T : IBaseEntity
    {
        public bool UseSearch { get; set; }
        public string SearchText { get; set; }
        public Expression<Func<T, bool>> Filter { get; set; }
    }
}
