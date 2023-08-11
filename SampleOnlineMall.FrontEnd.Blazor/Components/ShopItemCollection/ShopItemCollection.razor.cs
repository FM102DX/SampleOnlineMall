using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleOnlineMall.FrontEnd.Blazor.Data;
using SampleOnlineMall.Core;

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
        public string SearchText { get; set; }

        public List<CommodityItemFrontend> ItemsNo { get; set; } = new List<CommodityItemFrontend>();

        public string FullName { get; set; }

        public int Count { get; set; }

        protected override void OnInitialized()
        {
            CompHub.DoingSearch += CompHub_DoingSearch;
        }
        protected override async Task OnInitializedAsync()
        {
             await DoPageLoad();
        }

        private async void CompHub_DoingSearch(string SearchText)
        {
            Logger.Information("Searching from comphub with text: " + SearchText);

            ItemsNo = (await Manager.Search(SearchText)).ToList();

            Count = ItemsNo.Count;

            StateHasChanged();
        }

        protected async Task DoPageLoad()
        {
            if (UsageCase == ShopItemCollectionUsageCaseEnum.MainPageAppearamce)
            {
                ItemsNo = (await Manager.GetAllAsync()).ToList();
            }
            else if (UsageCase == ShopItemCollectionUsageCaseEnum.MainBarSearch)
            {
                Logger.Information($"Doing search in ShopItemCollection with text = {SearchText}");
                if (Manager == null) { Logger.Information("Repository is NULL"); } else Logger.Information("Repository is NOT NULL");
                ItemsNo = (await Manager.Search(SearchText)).ToList();
            }

            Logger.Information($"This Shopitemcollection object, IncomingItemsCount= {ItemsNo.Count}");

            Count = ItemsNo.Count;
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