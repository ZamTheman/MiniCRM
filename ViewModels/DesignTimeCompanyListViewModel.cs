using System.Collections.ObjectModel;
using ModelLayer;

namespace ViewModels
{
    public class DesignTimeCompanyListViewModel : ICompanyListViewModel
    {
        public ObservableCollection<Company> AllCompanies { get; set; }
    }
}
