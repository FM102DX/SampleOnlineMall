using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SampleOnlineMall.Service;

namespace SampleOnlineMall.DataAccess.Abstract
{
    public interface IAsyncRepositoryT<T> where T : IBaseEntity
    {
        
        //GetAll
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter);

        public Task<IEnumerable<T>> GetPageAsync(int pageNo, int elementsPerPage);
        public Task<IEnumerable<T>> GetPageAsync(Expression<Func<T, bool>> filter, int pageNo, int elementsPerPage);



        //Search
        public Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> filter);
        public Task<IEnumerable<T>> SearchAsync(string text);


        //others
        public Task<T> GetByIdOrNullAsync(Guid id);

        public Task<int> GetCountAsync();
        public  Task<bool> Exists(Guid id);

        public Task<CommonOperationResult> AddAsync(T t);

        public Task<CommonOperationResult> UpdateAsync(T t);

        public Task<CommonOperationResult> DeleteAsync(Guid id);

        public Task<CommonOperationResult> InitAsync(bool deleteDb = false);

    }
}
