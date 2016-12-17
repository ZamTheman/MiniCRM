using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.Unity;
using ModelLayer;
using Repositories;
using Utils;
using Utils.Messages;

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
                SaveEntityCommand.RaiseCanExecuteChanged();
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
                SaveEntityCommand.RaiseCanExecuteChanged();
            }
        }

        private Todo _activeTodo;
        public Todo ActiveTodo
        {
            get { return _activeTodo; }
            set
            {
                if (value == _activeTodo)
                    return;
                Set(() => ActiveTodo, ref _activeTodo, value);
                UpdateAllFields();
            }
        }

        public IWriter Writer { get; set; }
        public IRepository Repository { get; set; }
        public int CompanyId { get; set; }
        public RelayCommand SaveEntityCommand { get; set; }

        private bool _saveButtonVisible;
        public bool SaveButtonVisible
        {
            get { return _saveButtonVisible; }
            set { Set(() => SaveButtonVisible, ref _saveButtonVisible, value); }
        }

        private void UpdateAllFields()
        {
            if (ActiveTodo != null)
                Date = Convert.ToDateTime(ActiveTodo.Date);
            else
                Date = DateTime.Now;

            Description = ActiveTodo.Description;
        }

        [InjectionConstructor]
        public TodoViewModel(IEntity entity, IRepository repository, IWriter writer, int companyId)
        {
            CompanyId = companyId;
            Writer = writer;
            Repository = repository;
            SaveEntityCommand = new RelayCommand(SaveEntity, CanSaveEntity);
            ActiveTodo = entity as Todo;
        }

        private bool CanSaveEntity()
        {
            if (ActiveTodo == null)
            {
                if (!string.IsNullOrEmpty(Description))
                {
                    SaveButtonVisible = true;
                    return true;
                }
            }

            if (Date != ActiveTodo.Date || Description != ActiveTodo.Description)
            {
                SaveButtonVisible = true;
                return true;
            }
            SaveButtonVisible = false;
            return false;
        }

        private async void SaveEntity()
        {
            if (ActiveTodo != null)
            {
                ActiveTodo.Date = Date;
                ActiveTodo.Description = Description;
                await Repository.SaveEntity(Writer, ActiveTodo, CompanyId);
            }

            else
            {
                var ent = new Todo()
                {
                    Date = this.Date,
                    Description = this.Description
                };
                int id = await Repository.SaveEntity(Writer, ent, CompanyId);

                ActiveTodo = new Todo()
                {
                    Id = id,
                    Date = this.Date,
                    Description = this.Description
                };
            }
            Messenger.Default.Send(new EntityAddedMessenger() {Entity = ActiveTodo, companyId = CompanyId});
        }

    }
}
