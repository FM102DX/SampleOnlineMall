using SampleOnlineMall.FrontEnd.Blazor.Components.Paginator;

namespace SampleOnlineMall.FrontEnd.Blazor.Data
{
    public class ComponentHub
    {

        Serilog.ILogger _logger;

        //class to organize component interactio
        public string SearchText { get; set; }

        public ComponentHub(Serilog.ILogger logger)
        {
            _logger = logger;
        }
        
        //search
        public void Search (string SearchText)
        {
            DoingSearch(SearchText);
        }

        public event DoingSearchHandler DoingSearch;

        public delegate void DoingSearchHandler(string SearchText);



        // PaginatorSelectionChanged
        public delegate void PaginatorSelectionChangedHandler(int selectedPage);
        
        public event PaginatorSelectionChangedHandler PaginatorSelectionChanged;
        
        public void ChangePaginatorSelection(int i)
        {
            PaginatorSelectionChanged(i);
        }


        // PaginatorStateSet
        public delegate void SetPaginatorStateHandler(int selectedPage, int count, int itemsPerPage);

        public event SetPaginatorStateHandler PaginatorStateSet;

        public void SetPaginatonState(int selectedPage, int count, int itemsPerPage)
        {
            PaginatorStateSet (selectedPage,count, itemsPerPage);
        }
    }
}
