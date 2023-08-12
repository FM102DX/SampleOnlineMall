using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using static SampleOnlineMall.FrontEnd.Blazor.Components.ShopItemCollection.ShopItemCollection;

namespace SampleOnlineMall.FrontEnd.Blazor.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        public Serilog.ILogger Logger { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Logger.Information("index page opened");
        }
    }
}
