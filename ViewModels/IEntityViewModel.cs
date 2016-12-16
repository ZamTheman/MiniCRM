using ModelLayer;
using Repositories;
using Utils;

namespace ViewModels
{
    public interface IEntityViewModel : IViewModel
    {
        IEntity ActiveEntity { get; set; }
        IWriter Writer { get; set; }
        IRepository Repository { get; set; }

        int CompanyId { get; set; }
    }
}
