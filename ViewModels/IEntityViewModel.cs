using ModelLayer;

namespace ViewModels
{
    public interface IEntityViewModel : IViewModel
    {
        IEntity ActiveEntity { get; set; }
    }
}
