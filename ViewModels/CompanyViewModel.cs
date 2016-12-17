using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Networking.ServiceDiscovery.Dnssd;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using ModelLayer;
using Utils.Messages;
using Repositories;
using Utils;

namespace ViewModels
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
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _addDeleteButtonsVisible;
        public bool AddDeleteButtonsVisible
        {
            get { return _addDeleteButtonsVisible;}
            set
            {
                if (!ViewEmployees && !ViewHistories && !ViewTodos)
                    Set(() => AddDeleteButtonsVisible, ref _addDeleteButtonsVisible, false);

                else
                Set(() => AddDeleteButtonsVisible, ref _addDeleteButtonsVisible, true);
            }
        }

        private bool _viewEmployees;
        public bool ViewEmployees
        {
            get { return _viewEmployees; }
            set
            {
                Set(() => ViewEmployees, ref _viewEmployees, value);
                AddDeleteButtonsVisible = true;
            }
        }

        private bool _viewTodos;
        public bool ViewTodos
        {
            get { return _viewTodos; }
            set
            {
                Set(() => ViewTodos, ref _viewTodos, value);
                AddDeleteButtonsVisible = true;
            }
        }

        private bool _viewHistories;
        public bool ViewHistories
        {
            get { return _viewHistories; }
            set
            {
                Set(() => ViewHistories, ref _viewHistories, value);
                AddDeleteButtonsVisible = true;
            }
        }

        private bool _canSaveCompanyBool;
        public bool CanSaveCompanyBool
        {
            get { return _canSaveCompanyBool; }
            set
            {
                Set(() => CanSaveCompanyBool, ref _canSaveCompanyBool, value);
            }
        }

        private ICompany _selectedCompany;
        public ICompany SelectedCompany
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
            set
            {
                Set(() => CompanyName, ref _companyName, value);
                SaveCompanyCommand.RaiseCanExecuteChanged();
            }
        }

        private string _companyPhone;
        public string CompanyPhone
        {
            get { return _companyPhone; }
            set
            {
                Set(() => CompanyPhone, ref _companyPhone, value);
                SaveCompanyCommand.RaiseCanExecuteChanged();
            }
        }

        private string _companyCity;
        public string CompanyCity
        {
            get { return _companyCity; }
            set
            {
                Set(() => CompanyCity, ref _companyCity, value);
                SaveCompanyCommand.RaiseCanExecuteChanged();
            }
        }

        private string _companyStreet;
        public string CompanyStreet
        {
            get { return _companyStreet; }
            set
            {
                Set(() => CompanyStreet, ref _companyStreet, value);
                SaveCompanyCommand.RaiseCanExecuteChanged();
            }
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
        //
        // Controls the visibility of the save button
        //
        private bool _saveButtonVisibility;
        public bool SaveButtonVisible
        {
            get { return _saveButtonVisibility; }
            set { Set(() => SaveButtonVisible, ref _saveButtonVisibility, value); }
        }

        private IRepository _repository;
        private IWriter _writer;

        #endregion

        // Commands
        public RelayCommand SaveCompanyCommand { get; private set; }
        public RelayCommand EmplyeeListActiveCommand { get; private set; }
        public RelayCommand TodoListActiveCommand { get; private set; }
        public RelayCommand HistoryListActiveCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }
        public RelayCommand AddCommand { get; private set; }
        public RelayCommand<IEntity> EntitySelectedCommand { get; private set; }
        
        // Constructor
        public CompanyViewModel(IRepository repository, IReader reader, IWriter writer)
        {
            this._repository = repository;
            this._writer = writer;
            LoadCommands();
            Registrations();
        }

        private void Registrations()
        {
            Messenger.Default.Register<SelectedCompanyMessenger>(this, (company) =>
            {
                this.SelectedCompany = company.SelectedCompany;
                UpdateAllFields();
            });

            Messenger.Default.Register<EntityAddedMessenger>(this, (entity) =>
            {
                this.SelectedEntity = entity.Entity;
                ListWasUpdated();
            });
        }

        private void LoadCommands()
        {
            SaveCompanyCommand = new RelayCommand(SaveCompany, CanSaveCompany);
            EmplyeeListActiveCommand = new RelayCommand(ToggleEmployeeListVisibility);
            TodoListActiveCommand = new RelayCommand(ToggleTodoListVisibility);
            HistoryListActiveCommand = new RelayCommand(ToggleHistoryListVisibility);
            EntitySelectedCommand = new RelayCommand<IEntity>(EntitySelected);
            DeleteCommand = new RelayCommand(DeleteSelectedEntity, () => _selectedEntity != null);
            AddCommand = new RelayCommand(AddNewEntity);
        }

        private void AddNewEntity()
        {
            IEntity emtpyEntity = null;
            if (ViewTodos)
                emtpyEntity = new Todo();
            if (ViewEmployees)
                emtpyEntity = new Employee();
            if (ViewHistories)
                emtpyEntity = new HistoryPost();

            Messenger.Default.Send(new SelectedEntityMessenger() { SelectedEntityMessageEntity = emtpyEntity });
        }

        public CompanyViewModel()
        {
            
        }

        private void EntitySelected(IEntity entity)
        {
            Messenger.Default.Send(new SelectedEntityMessenger() { SelectedEntityMessageEntity = entity });
        }

        private void ListWasUpdated()
        {
            Messenger.Default.Send(new ListupdatedMessenger() {CompanyId = SelectedCompany.Id });
        }

        private async Task DeleteEntityFromFile()
        {
            await _repository.DeleteEntity(_writer, SelectedEntity, SelectedCompany);
        }

        private async void DeleteSelectedEntity()
        {
            await DeleteEntityFromFile();   
            string[] type = SelectedEntity.GetType().ToString().Split('.');
            switch (type[type.Length - 1])
            {
                case "Employee":
                    var employeeToRemove = CompanyEmployees.FirstOrDefault(c => c.Id == SelectedEntity.Id);
                    if (employeeToRemove != null)
                    {
                        SelectedCompany.Employees.RemoveAll(e => e.Id == SelectedEntity.Id);
                        CompanyEmployees.Remove(employeeToRemove);
                    }
                        
                    break;
                case "Todo":
                    var todoToRemove = CompanyTodos.FirstOrDefault(c => c.Id == SelectedEntity.Id);
                    if (todoToRemove != null)
                    {
                        SelectedCompany.Todos.RemoveAll(t => t.Id == SelectedEntity.Id);
                        CompanyTodos.Remove(todoToRemove);
                    }
                        
                    break;
                case "HistoryPost":
                    var historyToRemove = CompanyHistories.FirstOrDefault(c => c.Id == SelectedEntity.Id);
                    if (historyToRemove != null)
                    {
                        SelectedCompany.Histories.RemoveAll(h => h.Id == SelectedEntity.Id);
                        CompanyHistories.Remove(historyToRemove);
                    }
                    break;
            }
            
            SelectedEntity = null;
            EntitySelected(null);
        }

        private bool CanSaveCompany()
        {
            if (SelectedCompany == null)
            {
                if (!string.IsNullOrEmpty(CompanyName) || !string.IsNullOrEmpty(CompanyCity) ||
                    !string.IsNullOrEmpty(CompanyCity) || !string.IsNullOrEmpty(CompanyStreet))
                {
                    SaveButtonVisible = true;
                    return true;
                }
                SaveButtonVisible = false;
                return false;
            }

            if (CompanyName != SelectedCompany.Name || CompanyPhone != SelectedCompany.Phone ||
                CompanyCity != SelectedCompany.City || CompanyStreet != SelectedCompany.Street)
            {
                SaveButtonVisible = true;
                return true;
            }
            SaveButtonVisible = false;
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

        private async void SaveCompany()
        {
            if (SelectedCompany != null)
            {
                SelectedCompany.Name = CompanyName;
                SelectedCompany.City = CompanyCity;
                SelectedCompany.Phone = CompanyPhone;
                SelectedCompany.Street = CompanyStreet;
                SelectedCompany.Employees.Clear();
                SelectedCompany.Employees.AddRange(CompanyEmployees);
                SelectedCompany.Todos.Clear();
                SelectedCompany.Todos.AddRange(CompanyTodos);
                SelectedCompany.Histories.Clear();
                SelectedCompany.Histories.AddRange(CompanyHistories);
                await _repository.Save(_writer, SelectedCompany);
            }

            else
            {
                int tempId = await _repository.Save(_writer, new Company()
                {
                    Name = CompanyName,
                    City = CompanyCity,
                    Phone = CompanyPhone,
                    Street = CompanyStreet,
                    Employees = CompanyEmployees,
                    Todos = CompanyTodos,
                    Histories = CompanyHistories
                });

                SelectedCompany = new Company
                {
                    Id = tempId,
                    Name = CompanyName,
                    City = CompanyCity,
                    Phone = CompanyPhone,
                    Street = CompanyStreet
                };
                SelectedCompany.Employees.Clear();
                SelectedCompany.Employees.AddRange(CompanyEmployees);
                SelectedCompany.Todos.Clear();
                SelectedCompany.Todos.AddRange(CompanyTodos);
                SelectedCompany.Histories.Clear();
                SelectedCompany.Histories.AddRange(CompanyHistories);
            }

            ListWasUpdated();
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
