using System.Collections.Generic;
using GalaSoft.MvvmLight;

namespace ModelLayer
{
    public class Company : ObservableObject, ICompany
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Phone { get; set; }

        public IList<Employee> Employees { get; set; }
        public IList<HistoryPost> Histories { get; set; }
        public IList<Todo> Todos { get; set; }

        public Company()
        {
            Employees = new List<Employee>();
            Histories = new List<HistoryPost>();
            Todos = new List<Todo>();
        }
    }
}
