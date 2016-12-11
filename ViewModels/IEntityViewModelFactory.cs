using ModelLayer;

namespace ViewModels
{
    public interface IEntityViewModelFactory
    {
        IEntityViewModel GetEntityViewModel(string type, IEntity entity);
    }
}
