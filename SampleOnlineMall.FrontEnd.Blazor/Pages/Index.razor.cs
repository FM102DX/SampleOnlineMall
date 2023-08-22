using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using static SampleOnlineMall.FrontEnd.Blazor.Components.ShopItemCollection.ShopItemCollection;

namespace SampleOnlineMall.FrontEnd.Blazor.Pages
{
    public partial class Index : ComponentBase
    {
        [Parameter]
        public int? Page { get; set; }

        public int PageToPass 
        { 
            get 
            {
                int page = 0;
                if(Page!=null)
                {
                    page = (int)Page;
                }
                return page;
            } 
        }

        [Inject]
        public Serilog.ILogger Logger { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Logger.Information($"index page opened with page={Page}");
        }
        protected override void OnInitialized()
        {
            Logger.Information($"VOID index page opened with page={Page}");
        }
    }
}
