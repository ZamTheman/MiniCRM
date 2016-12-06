using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using NoBSCRM.Messages;
using NoBSCRM.Models;
using NoBSCRM.Repositories;
using NoBSCRM.Utils;

namespace NoBSCRM.ViewModels
{
    public class CompanyViewModel : ViewModelBase, ICompanyViewModel
    {
        #region Properties

        private IEntity _selectedEntity;
        public IEntity SelectedEntity
        {
            get { return _selectedEntity;}
            set
            {
                if (value == _selectedEntity)
                    return;
                Set(() => SelectedEntity, ref _selectedEntity, value);
                EntitySelected(_selectedEntity);
            }
        }

        private bool _viewEmployees;
        public bool ViewEmployees
        {
            get { return _viewEmployees; }
            set
            {
                Set(() => ViewEmployees, ref _viewEmployees, value);
            }
        }

        private bool _viewTodos;
        public bool ViewTodos
        {
            get { return _viewTodos; }
            set
            {
                Set(() => ViewTodos, ref _viewTodos, value);
            }
        }

        private bool _viewHistories;
        public bool ViewHistories
        {
            get { return _viewHistories; }
            set
            {
                Set(() => ViewHistories, ref _viewHistories, value);
            }
        }

        private Company _selectedCompany;
        public Company SelectedCompany
        {
            get { return _selectedCompany;}
            set
            {
                Set(() => SelectedCompany, ref _selectedCompany, value);
                UpdateAllFields();
            }
        }

        private string _companyName;
        public string CompanyName
        {
            get { return _companyName;}
            set { Set(() => CompanyName, ref _companyName, value); }
        }

        private string _companyPhone;
        public string CompanyPhone
        {
            get { return _companyPhone; }
            set { Set(() => CompanyPhone, ref _companyPhone, value); }
        }

        private string _companyCity;
        public string CompanyCity
        {
            get { return _companyCity; }
            set { Set(() => CompanyCity, ref _companyCity, value); }
        }

        private string _companyStreet;
        public string CompanyStreet
        {
            get { return _companyStreet; }
            set { Set(() => CompanyStreet, ref _companyStreet, value); }
        }

        private ObservableCollection<Todo> _companyTodos;
        public ObservableCollection<Todo> CompanyTodos
        {
            get { return _companyTodos; }
            set { Set(() => CompanyTodos, ref _companyTodos, value); }
        }

        private ObservableCollection<HistoryPost> _companyHistories;
        public ObservableCollection<HistoryPost> CompanyHistories
        {
            get { return _companyHistories; }
            set { Set(() => CompanyHistories, ref _companyHistories, value); }
        }

        private ObservableCollection<Employee> _companyEmployees;
        public ObservableCollection<Employee> CompanyEmployees
        {
            get { return _companyEmployees; }
            set { Set(() => CompanyEmployees, ref _companyEmployees, value); }
        }

        private IRepository _repository;
        private IReader _reader;
        private IWriter _writer;

        #endregion

        // Commands
        public RelayCommand SaveCustomerCommand { get; private set; }
        public RelayCommand EmplyeeListActiveCommand { get; private set; }
        public RelayCommand TodoListActiveCommand { get; private set; }
        public RelayCommand HistoryListActiveCommand { get; private set; }
        public RelayCommand<IEntity> EntitySelectedCommand { get; private set; }

        // Constructor
        public CompanyViewModel(IRepository repository, IReader reader, IWriter writer)
        {
            this._repository = repository;
            this._writer = writer;
            this._reader = reader;
            SaveCustomerCommand = new RelayCommand(SaveCustomer, CanSaveCustomer);
            EmplyeeListActiveCommand = new RelayCommand(ToggleEmployeeListVisibility);
            TodoListActiveCommand = new RelayCommand(ToggleTodoListVisibility);
            HistoryListActiveCommand = new RelayCommand(ToggleHistoryListVisibility);
            EntitySelectedCommand = new RelayCommand<IEntity>(EntitySelected);
            Messenger.Default.Register<SelectedCompanyMessenger>(this, (company) =>
            {
                this.SelectedCompany = company.SelectedCompany;
                UpdateAllFields();
            });
        }

        public CompanyViewModel()
        {
            
        }

        private void EntitySelected(IEntity entity)
        {
            if (entity != null)
            {
                Messenger.Default.Send<SelectedEntityMessenger>(new SelectedEntityMessenger() { SelectedEntity = entity });
            }
        }

        private bool CanSaveCustomer()
        {
            if (CompanyName != SelectedCompany.Name || CompanyCity != SelectedCompany.Phone ||
                CompanyCity != SelectedCompany.City || CompanyStreet != SelectedCompany.Street ||
                !CompanyTodos.Equals(SelectedCompany.Todos) || !CompanyHistories.Equals(SelectedCompany.Histories) ||
                !CompanyEmployees.Equals(SelectedCompany.Employees))
                return true;

            return false;
        }

        private void ToggleEmployeeListVisibility()
        {
            ViewTodos = false;
            ViewHistories = false;
            SelectedEntity = null;
        }

        private void ToggleTodoListVisibility()
        {
            ViewEmployees = false;
            ViewHistories = false;
            SelectedEntity = null;
        }

        private void ToggleHistoryListVisibility()
        {
            ViewEmployees = false;
            ViewTodos = false;
            SelectedEntity = null;
        }

        private void SaveCustomer()
        {
            throw new NotImplementedException();
        }

        private void UpdateAllFields()
        {
            CompanyName = SelectedCompany != null ? SelectedCompany.Name : "";
            CompanyPhone = SelectedCompany != null ? SelectedCompany.Phone : "";
            CompanyCity = SelectedCompany != null ? SelectedCompany.City : "";
            CompanyStreet = SelectedCompany != null ? SelectedCompany.Street : "";
            CompanyTodos = new ObservableCollection<Todo>();
            CompanyHistories = new ObservableCollection<HistoryPost>();
            CompanyEmployees = new ObservableCollection<Employee>();
            if (SelectedCompany != null)
            {
                CompanyTodos.AddRange(SelectedCompany.Todos);
                CompanyHistories.AddRange(SelectedCompany.Histories);
                CompanyEmployees.AddRange(SelectedCompany.Employees);
            }
        }
    }
}
