using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleOnlineMall.FrontEnd.Blazor.Data;
using SampleOnlineMall.Core;
using SampleOnlineMall.DataAccess.Models;
using Newtonsoft.Json;
using SampleOnlineMall.FrontEnd.Blazor.Components.Paginator;

namespace SampleOnlineMall.FrontEnd.Blazor.Components.ShopItemCollection
{
    public partial class ShopItemCollection: ComponentBase
    {
        [Inject]
        public StoreManager Manager { get; set; }

        [Inject]
        public Serilog.ILogger Logger { get; set; }

        [Inject]
        ComponentHub CompHub { get; set; }



        [Parameter]
        public ShopItemCollectionUsageCaseEnum UsageCase { get; set; }

        [Parameter]
        public int Page { get; set; }

        [Parameter]
        public int ItemsPerPage { get; set; } = 4;

        [Parameter]
        public string SearchText { get; set; }

        public List<CommodityItemFrontendDisplayed> ItemsDisplayed { get; set; } = new List<CommodityItemFrontendDisplayed>();

        public string FullName { get; set; }

        public int ItemsTotalCount { get; set; }
        public int PagesCount { get; set; }

        public PaginatorPositionTypeEnum CurrentPageType { get; set; }

        protected override void OnInitialized()
        {
            CompHub.DoingSearch += CompHub_DoingSearch;
            CompHub.PaginatorSelectionChanged += CompHub_PaginatorSelectionChanged;
        }

        protected override async Task OnParametersSetAsync()
        {
            Logger.Information($"OnParametersSetAsync");
            await DoPageLoadWithPagination(Page);
            CompHub.SetPaginatonState(Page, ItemsTotalCount, ItemsPerPage);
        }

        private async void CompHub_PaginatorSelectionChanged(int selectedPage)
        {
            //await DoPageLoadWithPagination(selectedPage);
        }

        protected override async Task OnInitializedAsync()
        {
            Logger.Information($"OnInitializedAsync");
            //await DoPageLoadWithPagination(Page);
            //CompHub.SetPaginatonState(Page, ItemsTotalCount, ItemsPerPage);
        }

        private async void CompHub_DoingSearch(string SearchText)
        {
            Logger.Information("Searching from comphub with text: " + SearchText);

            ItemsDisplayed = (await Manager.Search(SearchText)).ToList();

            //ItemsTotalCount = ItemsDisplayed.Count;

            StateHasChanged();
        }

        protected async Task DoPageLoadWithPagination(int pageNo = 1)
        {
            if (UsageCase == ShopItemCollectionUsageCaseEnum.MainPageAppearamce)
            {
                RepositoryRequestTextSearch req = new RepositoryRequestTextSearch()
                {
                    UsePagination = true,
                    ItemsPerPage = ItemsPerPage,
                    UseSearch = false,
                    Page = pageNo
                };

                var responce = await Manager.GetAllAsync(req);

                Logger.Information($"Go");

                Logger.Information($"{JsonConvert.SerializeObject(responce)}");

                ItemsTotalCount = responce.TotalCount;

                ItemsDisplayed = responce.Items.ToList();

                StateHasChanged();
            }
            else if (UsageCase == ShopItemCollectionUsageCaseEnum.MainBarSearch)
            {

                RepositoryRequestTextSearch req = new RepositoryRequestTextSearch()
                {
                    UsePagination = true,
                    UseSearch = true,
                    ItemsPerPage = 3,
                    SearchText = SearchText,
                    Page = pageNo
                };

                var responce = await Manager.GetAllAsync(req);

                ItemsDisplayed = responce.Items.ToList();

                StateHasChanged();
            }
        }

        protected async Task DoPageLoadWithNoPagination()
        {
            if (UsageCase == ShopItemCollectionUsageCaseEnum.MainPageAppearamce)
            {
                
                RepositoryRequestTextSearch req = new RepositoryRequestTextSearch()
                {
                    UsePagination = false,
                    UseSearch = false
                };

                var responce = await Manager.GetAllAsync(req);

                ItemsDisplayed = responce.Items.ToList();

                ItemsDisplayed.ForEach(x=> Logger.Information(x.AsJson()));

            }
            else if (UsageCase == ShopItemCollectionUsageCaseEnum.MainBarSearch)
            {
                Logger.Information($"Doing search in ShopItemCollection with text = {SearchText}");
                if (Manager == null) { Logger.Information("Repository is NULL"); } else Logger.Information("Repository is NOT NULL");
                ItemsDisplayed = (await Manager.Search(SearchText)).ToList();
            }

            Logger.Information($"This Shopitemcollection object, IncomingItemsCount= {ItemsDisplayed.Count}");

            ItemsTotalCount = ItemsDisplayed.Count;
        }

        public enum ShopItemCollectionUsageCaseEnum
        {
            MainPageAppearamce=1,
            MainBarSearch = 2
        }

        public void DoLoggerAction()
        {
            Logger.Information("LoggerActionDone");
        }
    }
}