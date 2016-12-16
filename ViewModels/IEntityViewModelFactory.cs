using ModelLayer;
using Repositories;
using Utils;

namespace ViewModels
{
    public interface IEntityViewModelFactory
    {
        IEntityViewModel GetEntityViewModel(string type, IEntity entity, int companyId);
    }
}
