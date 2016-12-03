using Microsoft.Practices.Unity;
using NoBSCRM.Models;

namespace NoBSCRM.ViewModels
{
    public class EntityViewModelFactory : IEntityViewModelFactory
    {
        private readonly UnityContainer _container;

        public EntityViewModelFactory()
        {
            _container = new UnityContainer();
        }
        
        public IEntityViewModel GetEntityViewModel(string type, IEntity entity)
        {
            _container.RegisterType<IEntityViewModel, EmployeeViewModel>("Employee", new InjectionConstructor(entity as Employee));
            _container.RegisterType<IEntityViewModel, TodoViewModel>("Todo");
            _container.RegisterType<IEntityViewModel, HistoryViewModel>("HistoryPost");

            return _container.Resolve<IEntityViewModel>(type);
        }
    }

    
}
