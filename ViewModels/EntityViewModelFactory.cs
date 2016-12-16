using Microsoft.Practices.Unity;
using ModelLayer;
using Repositories;
using Utils;

namespace ViewModels
{
    public class EntityViewModelFactory : IEntityViewModelFactory
    {
        private readonly UnityContainer _container;

        public EntityViewModelFactory()
        {
            _container = new UnityContainer();
        }
        
        public IEntityViewModel GetEntityViewModel(string type, IEntity entity, int companyId)
        {
            _container.RegisterType<IRepository, Repository>("Repository");
            _container.RegisterType<IWriter, XMLWriter>("Writer");
            _container.RegisterType<IEntityViewModel, EmployeeViewModel>("Employee", new InjectionConstructor(entity, _container.Resolve<IRepository>("Repository"), _container.Resolve<IWriter>("Writer"),companyId));
            _container.RegisterType<IEntityViewModel, TodoViewModel>("Todo", new InjectionConstructor(entity, _container.Resolve<IRepository>("Repository"), _container.Resolve<IWriter>("Writer"),companyId));
            _container.RegisterType<IEntityViewModel, HistoryViewModel>("HistoryPost", new InjectionConstructor(entity, _container.Resolve<IRepository>("Repository"), _container.Resolve<IWriter>("Writer"),companyId));

            return _container.Resolve<IEntityViewModel>(type);
        }
    }
}
