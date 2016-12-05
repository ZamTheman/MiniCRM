using NoBSCRM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoBSCRM.ViewModels
{
    public class DesignTimeCompanyListViewModel : ICompanyListViewModel
    {
        public ObservableCollection<Company> AllCompanies { get; set; }
    }
}
