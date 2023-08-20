using Microsoft.AspNetCore.Components;
using SampleOnlineMall.FrontEnd.Blazor.Data;

namespace SampleOnlineMall.FrontEnd.Blazor.Components.ImageSelector
{
    public partial class Paginator : ComponentBase
    {
        [Parameter]
        public int CurrentPage { get; set; } = 1;

        [Parameter]
        public int TotalPages { get; set; } = 1;

        [Inject]
        public Serilog.ILogger Logger { get; set; }

        [Inject]
        ComponentHub CompHub { get; set; }

        public void NumberClickHandler()
        {
            
        }
    }
}
