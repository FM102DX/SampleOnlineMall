using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Serialization;
using SampleOnlineMall.DataAccess.Abstract;
using SampleOnlineMall.DataAccess.Models;
using SampleOnlineMall.Service;
using System.Linq.Expressions;

namespace SampleOnlineMall.DataAccess.DataAccess
{
    public class WebApiAsyncRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        //GenericRepository на webApi

        private static readonly object _locker = new object();

        private HttpClient httpClient;
        private Serilog.ILogger _logger
        {
            get { return _options.Logger; }
        }
        private WebApiAsyncRepositoryOptions _options;

        public WebApiAsyncRepository(WebApiAsyncRepositoryOptions options)
        {
            _options = options;
            httpClient = new System.Net.Http.HttpClient(new HttpClientHandler());
            httpClient.BaseAddress = new Uri(_options.BaseAddress);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<int> GetCountAsync()
        {
            int rez = -1;
            try
            {
                var response = await httpClient.GetAsync($"{_options.CountHostPath}");
                var json = response.Content.ReadAsStringAsync().Result;
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:

                        rez = JsonConvert.DeserializeObject<int>(json);
                        break;
                    default:
                        throw new Exception();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
            return rez;
        }

        public async Task<IEnumerable<T>> Search(string searchText)
        {
            IEnumerable<T> items =  new List<T>();
            try
            {
                _logger.Information($"This is WebApiAsyncRepository.search searchText={searchText}");
                
                _logger.Information($"Sending reqyest to {httpClient.BaseAddress}");

                var response = await httpClient.GetAsync($"{_options.SearchHostPath}/{searchText}");
                
                var json = await response.Content.ReadAsStringAsync();

                _logger.Information($"Received json {json}");

                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        items = JsonConvert.DeserializeObject<IEnumerable<T>>(json);
                        break;
                    default:
                        throw new Exception();
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"ERROR: in WebApiAsyncRepository.search {ex.Message}");
            }
            return items;
        }


        public async Task<IEnumerable<T>> GetAllAsync()
        {
            IEnumerable<T> items = new List<T>();
            try
            {
                var response = await httpClient.GetAsync($"{_options.GetAllHostPath}");
                var json = await response.Content.ReadAsStringAsync();
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        items = JsonConvert.DeserializeObject<IEnumerable<T>>(json);
                        break;
                    default:
                        throw new Exception();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
            return items;
        }
        public async Task<T> GetByIdOrNullAsync(Guid id)
        {
            T item = null;
            try
            {
                var response = await httpClient.GetAsync($"{_options.GetByIdOrNullHostPath}{id}");
                var json = await response.Content.ReadAsStringAsync();
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        item = JsonConvert.DeserializeObject<T>(json);
                        break;
                    default:
                        throw new Exception();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
            return item;
        }

        public async Task<bool> Exists(Guid id)
        {
            var rez = await GetByIdOrNullAsync(id);
            return rez != null;
        }

        public async Task<CommonOperationResult> AddAsync(T t)
        {
            CommonOperationResult rez;
            try
            {
                string json;
                StringContent jsonContent;

                json = JsonConvert.SerializeObject(t, Formatting.Indented,
                        new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

                jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync($"{_options.InsertHostPath}", jsonContent);

                switch (response.StatusCode)
                {

                    case System.Net.HttpStatusCode.OK:
                        rez = CommonOperationResult.SayOk(response.Content.ReadAsStringAsync().Result);
                        break;

                    default:
                        throw new Exception();

                }
            }
            catch (Exception ex)
            {
                rez = CommonOperationResult.SayFail($"WebApiRepository caught an exception: {ex.Message}");
            }
            return rez;
        }

        public async Task<CommonOperationResult> UpdateAsync(T t)
        {
            CommonOperationResult rez;
            try
            {
                string json;

                StringContent jsonContent;

                json = JsonConvert.SerializeObject(t, Formatting.Indented,
                        new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

                jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync($"{_options.UpdateHostPath}", jsonContent);

                switch (response.StatusCode)
                {

                    case System.Net.HttpStatusCode.OK:
                        rez = CommonOperationResult.SayOk(response.Content.ReadAsStringAsync().Result);
                        break;
                    default:
                        throw new Exception();
                }
            }
            catch (Exception ex)
            {
                rez = CommonOperationResult.SayFail($"{ex.Message}");
            }
            return rez;
        }

        public async Task<CommonOperationResult> DeleteAsync(Guid id)
        {

            CommonOperationResult rez;
            try
            {
                var response = await httpClient.DeleteAsync($"{_options.DeleteHostPath}/{id}");

                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        rez = CommonOperationResult.SayOk(response.Content.ReadAsStringAsync().Result);
                        break;
                    default:
                        throw new Exception();
                }
            }
            catch (Exception ex)
            {
                rez = CommonOperationResult.SayFail($"{ex.Message}");
            }
            return rez;

        }

        public async Task<CommonOperationResult> InitAsync(bool deleteDb)
        {
            return await Task.FromResult(CommonOperationResult.SayOk());
        }

        public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}