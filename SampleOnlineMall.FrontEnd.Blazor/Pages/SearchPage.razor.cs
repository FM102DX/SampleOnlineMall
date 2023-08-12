using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using static SampleOnlineMall.FrontEnd.Blazor.Components.ShopItemCollection.ShopItemCollection;

namespace SampleOnlineMall.FrontEnd.Blazor.Pages
{
    public partial class SearchPage : ComponentBase
    {

        [Inject]
        public Serilog.ILogger Logger { get; set; }

        [Parameter]
        public string SearchText { get; set; }

        public ShopItemCollectionUsageCaseEnum UsageCase { get; set; } = ShopItemCollectionUsageCaseEnum.MainBarSearch;

        protected override async Task OnInitializedAsync()
        {

        }
    }
}
