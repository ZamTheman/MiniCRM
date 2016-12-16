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
    public class EmployeeViewModel : ViewModelBase, IEntityViewModel
    {
        private bool _saveButtonVisible;
        public bool SaveButtonVisible
        {
            get { return _saveButtonVisible; }
            set
            {
                Set(() => SaveButtonVisible, ref _saveButtonVisible, value);
                
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name)
                    return;
                Set(() => Name, ref _name, value);
                SaveEntityCommand.RaiseCanExecuteChanged();
            }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set
            {
                if (value == _phone)
                    return;
                Set(() => Phone, ref _phone, value);
                SaveEntityCommand.RaiseCanExecuteChanged();
            }
        }

        private string _mobile;
        public string Mobile
        {
            get { return _mobile; }
            set
            {
                if (value == _mobile)
                    return;
                Set(() => Mobile, ref _mobile, value);
                SaveEntityCommand.RaiseCanExecuteChanged();
            }
        }

        private string _eMail;
        public string EMail
        {
            get { return _eMail; }
            set
            {
                if (value == _eMail)
                    return;
                Set(() => EMail, ref _eMail, value);
                SaveEntityCommand.RaiseCanExecuteChanged();
            }
        }

        private string _position;
        public string Position
        {
            get { return _position; }
            set
            {
                if (value == _position)
                    return;
                Set(() => Position, ref _position, value);
                SaveEntityCommand.RaiseCanExecuteChanged();
            }
        }

        private Employee _activEmployee;
        public Employee ActiveEmployee
        {
            get { return _activEmployee; }
            set
            {
                if (value == _activEmployee)
                    return;
                Set(() => ActiveEmployee, ref _activEmployee, value);
                UpdateAllFields();
            }
        }
        
        public IEntity ActiveEntity { get; set; }
        public IWriter Writer { get; set; }
        public IRepository Repository { get; set; }
        public int CompanyId { get; set; }
        public RelayCommand SaveEntityCommand { get; set; }

        private void UpdateAllFields()
        {
            Name = ActiveEmployee.Name;
            Phone = ActiveEmployee.Phone;
            Mobile = ActiveEmployee.Mobile;
            EMail = ActiveEmployee.Email;
            Position = ActiveEmployee.Position;
        }

        [InjectionConstructor]
        public EmployeeViewModel(IEntity entity, IRepository repository, IWriter writer, int companyId)
        {
            Repository = repository;
            Writer = writer;
            CompanyId = companyId;
            SaveEntityCommand = new RelayCommand(SaveEntity, CanSaveEntity);
            ActiveEmployee = entity as Employee;
        }

        public bool CanSaveEntity()
        {
            if (ActiveEmployee == null)
            {
                if (!string.IsNullOrEmpty(Name) || !string.IsNullOrEmpty(Phone) ||
                    !string.IsNullOrEmpty(Mobile) || !string.IsNullOrEmpty(EMail) || !string.IsNullOrEmpty(Position))
                {
                    SaveButtonVisible = true;
                    return true;
                }
                SaveButtonVisible = false;
                return false;
            }

            if (Name != ActiveEmployee.Name || Phone != ActiveEmployee.Phone ||
                Mobile != ActiveEmployee.Mobile || EMail != ActiveEmployee.Email || Position != ActiveEmployee.Position)
            {
                SaveButtonVisible = true;
                return true;
            }
            SaveButtonVisible = false;
            return false;
        }

        private async void SaveEntity()
        {
            if (ActiveEmployee != null)
            {
                ActiveEmployee.Name = Name;
                ActiveEmployee.Phone = Phone;
                ActiveEmployee.Mobile = Mobile;
                ActiveEmployee.Email = EMail;
                ActiveEmployee.Position = Position;
                await Repository.SaveEntity(Writer, ActiveEmployee, CompanyId);
            }

            else
            {
                var emp = new Employee()
                {
                    Name = this.Name,
                    Phone = this.Phone,
                    Mobile = this.Mobile,
                    Email = this.EMail,
                    Position = this.Position,
                };
                int id = await Repository.SaveEntity(Writer, emp, CompanyId);

                ActiveEmployee = new Employee()
                {
                    Id = id,
                    Name = this.Name,
                    Phone = this.Phone,
                    Mobile = this.Mobile,
                    Email = this.EMail,
                    Position = this.Position
                };
            }
            Messenger.Default.Send(new EntityAddedMessenger() {Entity = ActiveEmployee});
        }

    }
}
