using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using ModelLayer;
using Repositories;
using Utils;
using Utils.Messages;

namespace ViewModels
{
    public class CompanyListViewModel : ViewModelBase, ICompanyListViewModel
    {
        private ObservableCollection<ICompany> _allCompanies;
        public ObservableCollection<ICompany> AllCompanies
        {
            get { return _allCompanies; }
            set
            {
                _allCompanies = value;
                RaisePropertyChanged();
                FilterList();
            }
        }

        private ObservableCollection<ICompany> _filteredCompanies;
        public ObservableCollection<ICompany> FilteredCompanies
        {
            get { return _filteredCompanies; }
            set
            {
                _filteredCompanies = value;
                RaisePropertyChanged();
            }
        }

        private string _filterString;
        public string FilterString
        {
            get { return _filterString; }
            set
            {
                Set(() => FilterString, ref _filterString, value);
                FilterCommand.Execute(value);
            }
        }


        public RelayCommand<Company> SendCompanyCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand AddCommand { get; }
        public RelayCommand FilterCommand { get; set; }

        public Task Initialization { get; private set; }

        
        private IRepository _repository;
        private IReader _reader;
        private IWriter _writer;

        private ICompany _selectedCompany;
        public ICompany SelectedCompany
        {
            get { return _selectedCompany; }
            set
            {
                if (value == _selectedCompany)
                    return;
                Set(() => SelectedCompany, ref _selectedCompany, value);
                SendCompanyCommand.Execute(value);
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        public CompanyListViewModel(IRepository repository, IReader reader, IWriter writer)
        {
            _writer = writer;
            _reader = reader;
            _repository = repository;
            SendCompanyCommand = new RelayCommand<Company>(SendSelectedCompany);
            DeleteCommand = new RelayCommand (DeleteSelectedCompany, () => _selectedCompany != null);
            AddCommand = new RelayCommand(() => SelectedCompany = null);
            FilterCommand = new RelayCommand(FilterList);
            Initialization = LoadDataAsync(0);
            FilterList();
            FilterString = "";
            Messenger.Default.Register<ListupdatedMessenger>(this , (action) =>
            {
                Initialization = LoadDataAsync(action.CompanyId);
            });
        }

        private void FilterList()
        {
            if(FilteredCompanies == null)
                FilteredCompanies = new ObservableCollection<ICompany>();

            if (string.IsNullOrEmpty(FilterString))
            {
                FilteredCompanies.Clear();
                FilteredCompanies.AddRange(AllCompanies);
            }
 
            else
            {
                FilteredCompanies.Clear();
                var query = AllCompanies.Where(c => c.Name.ToLower().Contains(FilterString.ToLower()) || c.City.ToLower().Contains(FilterString.ToLower()));
                FilteredCompanies.AddRange(query);
            }
        }

        private void DeleteSelectedCompany()
        {
            _repository.DeleteCompany(_writer, SelectedCompany);
            var companyToRemove = AllCompanies.FirstOrDefault(c => c.Id == SelectedCompany.Id);
            if (companyToRemove != null)
                AllCompanies.Remove(companyToRemove);
            SelectedCompany = null;
            SendCompanyCommand.Execute(null);
            FilterList();
        }

        private void SendSelectedCompany(ICompany company)
        {
            Messenger.Default.Send<SelectedCompanyMessenger>(new SelectedCompanyMessenger() { SelectedCompany = company });
        }

        // Todo
        // Clean the generation of dummy data. Move to Mock repository?
        //

        private async Task LoadDataAsync(int companyId)
        {
            var companies = new List<ICompany>();
            try
            {
                companies = (List<ICompany>) await _repository.GetAll(_reader);
            }
            catch
            {
                return;
            }
            
            AllCompanies = new ObservableCollection<ICompany>();
            AllCompanies.AddRange(companies);
            SelectedCompany = AllCompanies.FirstOrDefault(c => c.Id == companyId);
            FilterList();
        }
        
        private async Task GenerateNewDataIfEmpty()
        {
            await _repository.WriteDummyData(_writer);
            await LoadDataAsync(0);
        }
    }
}
