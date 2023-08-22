using Microsoft.AspNetCore.Components;
using SampleOnlineMall.FrontEnd.Blazor.Data;

namespace SampleOnlineMall.FrontEnd.Blazor.Components.Paginator
{
    public partial class Paginator : ComponentBase
    {
        [Inject]
        Serilog.ILogger Logger { get; set; }

        [Inject]
        NavigationManager Navi { get; set; }

        [Inject]
        ComponentHub CompHub { get; set; }

        public int SelectedPage { get; set; }

        public int ItemsPerPage { get; set; }

        public int TotalCount { get; set; }

        public int PagesCount { get; set; }

        public string Status { get; set; }

        public string SelectedClass(int i)
        {
            string selectedClass = "";

            if (i == SelectedPage)
            {
                selectedClass = "paginator-control-item-selected";
            }
            else
            {
                selectedClass = "paginator-control-item-regular";
            }
            return selectedClass;
        }
        public PaginatorPositionTypeEnum PositionType { get; set; }

        protected override void OnInitialized()
        {
            CompHub.PaginatorStateSet += CompHub_SetPaginatorState;
        }

        private void CompHub_SetPaginatorState(int selectedPage, int totalCount, int itemsPerPage)
        {
            SelectedPage = selectedPage;
            TotalCount = totalCount;
            ItemsPerPage = itemsPerPage;

            PagesCount = totalCount / itemsPerPage;
            if (PagesCount * itemsPerPage < totalCount) PagesCount++;

            if (PagesCount == 0) PagesCount = 1;
            if (SelectedPage == 0) SelectedPage = 1;

            Status = $"[Paginator] selectedPage={SelectedPage},totalCount={TotalCount}, itemsPerPage={ItemsPerPage} PagesCount={PagesCount}";

            Logger.Information(Status);

            StateHasChanged();
        }
    }
}
