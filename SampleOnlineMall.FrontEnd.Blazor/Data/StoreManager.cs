using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SampleOnlineMall.Core;
using SampleOnlineMall.DataAccess.Abstract;
using SampleOnlineMall.DataAccess.DataAccess;
using System;


namespace SampleOnlineMall.FrontEnd.Blazor.Data
{
    public class StoreManager
    {
        private IAsyncRepository<CommodityItemFrontend> _repo { get; set; }

        private SampleOnlineMallFrontEndBlazorApp _app { get; set; }

        public string StoreBaseUrl { get; set; }

        public StoreManager(NavigationManager Navi, IAsyncRepository<CommodityItemFrontend> repo, SampleOnlineMallFrontEndBlazorApp app)
        {
            StoreBaseUrl = Navi.BaseUri;
            _repo = repo;
            _app = app;
        }
        public string GetItemPageFullAddress(Guid itemId)
        {
            return $"{StoreBaseUrl}Item/" + itemId.ToString();
        }
        public async Task<List<CommodityItemFrontend>> GetAllAsync()
        {
            return (await _repo.GetAllAsync()).ToList();
        }
        
        public async Task<List<CommodityItemFrontend>> Search(string searchText)
        {
            return (await _repo.SearchAsync(searchText)).ToList();
        }

        public async Task<CommodityItemFrontend> GetByIdOrNullAsync(Guid id)
        {
            return await _repo.GetByIdOrNullAsync(id);
        }


        



    }
}
