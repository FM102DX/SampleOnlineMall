namespace SampleOnlineMall.FrontEnd.Blazor.Data
{
    public class ComponentHub
    {
        //класс, через который взаимодействуют компоненты
        public string SearchText { get; set; }
        
        public void Search (string SearchText)
        {
            DoingSearch(SearchText);
        }

        public event DoingSearchHandler DoingSearch;

        public delegate void DoingSearchHandler(string SearchText);

        public delegate void PaginatorSelectionChangedHandler(int selectedPage);
        
        public event PaginatorSelectionChangedHandler PaginatorSelectionChanged;

    }
}
