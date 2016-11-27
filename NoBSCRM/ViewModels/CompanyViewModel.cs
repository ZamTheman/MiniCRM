using System;
using System.Collections.ObjectModel;
using System.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.Unity;
using NoBSCRM.Messages;
using NoBSCRM.Models;
using NoBSCRM.Repositories;
using NoBSCRM.Utils;

namespace NoBSCRM.ViewModels
{
    public class CompanyViewModel : ViewModelBase, ICompanyViewModel
    {
        #region Properties

        private Todo _selectedTodo;
        public Todo SelectedTodo
        {
            get { return _selectedTodo; }
            set
            {
                if (_selectedTodo == value)
                    return;
                Set(() => SelectedTodo, ref _selectedTodo, value);
            }
        }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                if (_selectedEmployee == value)
                    return;
                Set(() => SelectedEmployee, ref _selectedEmployee, value);
            }
        }

        private HistoryPost _selectedHistory;
        public HistoryPost SelectedHistory
        {
            get { return _selectedHistory; }
            set
            {
                if (_selectedHistory == value)
                    return;
                Set(() => SelectedHistory, ref _selectedHistory, value);
            }
        }

        private bool _viewEmployees;
        public bool ViewEmployees
        {
            get { return _viewEmployees; }
            set
            {
                Set(() => ViewEmployees, ref _viewEmployees, value);
                Set(() => ViewTodos, ref _viewTodos, false);
                Set(() => ViewHistories, ref _viewHistories, false);
            }
        }

        private bool _viewTodos;
        public bool ViewTodos
        {
            get { return _viewTodos; }
            set
            {
                Set(() => ViewEmployees, ref _viewEmployees, false);
                Set(() => ViewTodos, ref _viewTodos, value);
                Set(() => ViewHistories, ref _viewHistories, false);
            }
        }

        private bool _viewHistories;
        public bool ViewHistories
        {
            get { return _viewHistories; }
            set
            {
                Set(() => ViewEmployees, ref _viewEmployees, false);
                Set(() => ViewTodos, ref _viewTodos, false);
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
        
        // Constructor
        public CompanyViewModel(IRepository repository, IReader reader, IWriter writer)
        {
            this._repository = repository;
            this._writer = writer;
            this._reader = reader;
            SaveCustomerCommand = new RelayCommand(SaveCustomer, CanSaveCustomer);
            Messenger.Default.Register<MessageCommunicator>(this, (company) =>
            {
                this.SelectedCompany = company.selectedCompany;
            });
        }

        public CompanyViewModel()
        {
            
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

        private void SaveCustomer()
        {
            throw new NotImplementedException();
        }

        private void UpdateAllFields()
        {
            CompanyName = SelectedCompany.Name;
            CompanyPhone = SelectedCompany.Phone;
            CompanyCity = SelectedCompany.City;
            CompanyStreet = SelectedCompany.Street;
            CompanyTodos = new ObservableCollection<Todo>();
            CompanyTodos.AddRange(SelectedCompany.Todos);
            CompanyHistories = new ObservableCollection<HistoryPost>();
            CompanyHistories.AddRange(SelectedCompany.Histories);
            CompanyEmployees = new ObservableCollection<Employee>();
            CompanyEmployees.AddRange(SelectedCompany.Employees);
        }
    }
}
