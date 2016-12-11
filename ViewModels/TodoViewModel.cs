using System;
using GalaSoft.MvvmLight;
using Microsoft.Practices.Unity;
using ModelLayer;

namespace ViewModels
{
    public class TodoViewModel : ViewModelBase, IEntityViewModel
    {
        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (value == _date)
                    return;
                Set(() => Date, ref _date, value);
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                if (value == _description)
                    return;
                Set(() => Description, ref _description, value);
            }
        }
        
        private Todo _activeTodo;
        public IEntity ActiveEntity
        {
            get { return _activeTodo; }
            set
            {
                if (value == _activeTodo)
                    return;
                Set(() => ActiveEntity as Todo, ref _activeTodo, value as Todo);
                UpdateAllFields();
            }
        }

        private void UpdateAllFields()
        {
            Date = Convert.ToDateTime(_activeTodo.Date);
            Description = _activeTodo.Description;
        }

        [InjectionConstructor]
        public TodoViewModel(IEntity entity)
        {
            ActiveEntity = entity;
        }
    }
}
