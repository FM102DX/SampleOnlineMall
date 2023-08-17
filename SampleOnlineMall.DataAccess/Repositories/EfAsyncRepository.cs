using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using SampleOnlineMall.DataAccess.Abstract;
using SampleOnlineMall.DataAccess.Models;
using SampleOnlineMall.Service;
using System.Linq.Expressions;

namespace SampleOnlineMall.DataAccess
{
    public class EfAsyncRepository<T> : IAsyncRepositoryT<T> where T: BaseEntity
    {
        
        private DbContext _context;
        Serilog.ILogger _logger;

        public EfAsyncRepository(DbContext context, Serilog.ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                var rez = Task.FromResult((IEnumerable<T>)_context.Set<T>().AsNoTracking());
                return rez;
            }
            catch (Exception ex)
            {
                List<T> lst = new List<T>();
                IEnumerable<T> en = (IEnumerable<T>)lst;
                return Task.FromResult(en);
            }
        }

        public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            try
            {
                var rez = Task.FromResult((IEnumerable<T>)_context.Set<T>().Where(filter).AsNoTracking());
                return rez;
            }
            catch (Exception ex)
            {
                List<T> lst = new List<T>();
                IEnumerable<T> en = (IEnumerable<T>)lst;
                return Task.FromResult(en);
            }
        }

        public Task<IEnumerable<T>> GetPageAsync(int pageNo, int elementsPerPage)
        {
            try
            {
                var skipCount=(pageNo-1)*elementsPerPage;
                if (skipCount<0)
                {
                    skipCount = 0;
                }
                var rez = Task.FromResult((IEnumerable<T>)_context.Set<T>().Skip(skipCount).Take(elementsPerPage).AsNoTracking());
                return rez;
            }
            catch (Exception ex)
            {
                List<T> lst = new List<T>();
                IEnumerable<T> en = (IEnumerable<T>)lst;
                return Task.FromResult(en);
            }
        }

        public Task<IEnumerable<T>> GetPageAsync(Expression<Func<T, bool>> filter, int pageNo, int elementsPerPage)
        {
            try
            {
                var skipCount = (pageNo - 1) * elementsPerPage;
                if (skipCount < 0)
                {
                    skipCount = 0;
                }
                var rez = Task.FromResult((IEnumerable<T>)_context.Set<T>().Where(filter).Skip(skipCount).Take(elementsPerPage).AsNoTracking());
                return rez;
            }
            catch (Exception ex)
            {
                List<T> lst = new List<T>();
                IEnumerable<T> en = (IEnumerable<T>)lst;
                return Task.FromResult(en);
            }
        }

        public Task<T> GetByIdOrNullAsync(Guid id)
        {
            return _context.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            T targetObejct = GetByIdOrNullAsync(id).Result;
            return Task.FromResult(targetObejct == null);
        }

        public Task<CommonOperationResult> AddAsync(T t)
        {
            try
            {
                _logger.Information($"This is repository. Gonna insert object {t}");
                _context.Set<T>().Add(t);
                _context.SaveChanges();
                _logger.Information($"Successful");
                return Task.FromResult(CommonOperationResult.SayOk());
            }
            catch (Exception ex)
            {
                var errMsg = $"Error while inserting {ex}, innerexception is {ex.InnerException}";
                _logger.Debug(errMsg);
                return Task.FromResult(CommonOperationResult.SayFail(errMsg));
            }
        }

        public Task<CommonOperationResult> UpdateAsync(T t)
        {
            _context.Set<T>().Update(t);
            var rez = _context.SaveChanges();
            return Task.FromResult(CommonOperationResult.SayOk(rez.ToString()));
        }

        public Task<CommonOperationResult> DeleteAsync(Guid id)
        {
            T t = this.GetByIdOrNullAsync(id).Result;
            if (t == null)
            {
                return Task.FromResult(CommonOperationResult.SayFail($"Id not found: {id}"));
            }
            _context.Set<T>().Remove(t);
            var rez = _context.SaveChanges();
            return Task.FromResult(CommonOperationResult.SayOk(rez.ToString()));
        }

        public Task<bool> Exists(Guid id)
        {
            var t = this.GetByIdOrNullAsync(id).Result;
            return Task.FromResult(t != null);
        }

        public Task<CommonOperationResult> InitAsync(bool deleteDb=false)
        {
            if (deleteDb) _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            return Task.FromResult(CommonOperationResult.SayOk());
        }

        public Task<List<T>> GetItemsListAsync()
        {
            List<T> rez = new List<T>();

            IEnumerable<T> list = GetAllAsync().Result;

            foreach (T t in list)
            {
                rez.Add(t);
            }
            return Task.FromResult(rez);
        }

        public Task<int> GetCountAsync()
        {
            return Task.FromResult(_context.Set<T>().Count());
        }

        public Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> filter)
        {
            var rez = (IEnumerable<T>)(_context.Set<T>().Where(filter));
            return Task.FromResult(rez);
        }

        public Task<IEnumerable<T>> SearchAsync(string text)
        {
            throw new NotImplementedException();
        }


    }
}