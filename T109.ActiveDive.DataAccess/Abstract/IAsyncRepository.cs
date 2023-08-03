using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SampleOnlineMall.Service;

namespace SampleOnlineMall.DataAccess.Abstract
{
    public interface IAsyncRepository<T> where T : IBaseEntity
    {
        public Task<IEnumerable<T>> GetAllAsync();

        public Task<T> GetByIdOrNullAsync(Guid id);

        public Task<int> GetCountAsync();
        public  Task<bool> Exists(Guid id);

        public Task<CommonOperationResult> AddAsync(T t);

        public Task<CommonOperationResult> UpdateAsync(T t);

        public Task<CommonOperationResult> DeleteAsync(Guid id);

        public Task<CommonOperationResult> InitAsync(bool deleteDb = false);

    }
}
