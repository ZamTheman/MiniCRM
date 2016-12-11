using System.Collections.Generic;

namespace ModelLayer
{
    public interface ICompany
    {
        int Id { get; set; }
        string Name { get; set; }
        string City { get; set; }
        string Street { get; set; }
        string Phone { get; set; }

        IList<Employee> Employees { get;  }
        IList<HistoryPost> Histories { get;  }
        IList<Todo> Todos { get;  }
    }
}
