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
            }
        }
        public RelayCommand<Company> SendCompanyCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand AddCommand { get; }

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
            Initialization = LoadDataAsync(0);
            Messenger.Default.Register<ListupdatedMessenger>(this , (action) =>
            {
                Initialization = LoadDataAsync(action.CompanyId);
            });
        }
        
        private void DeleteSelectedCompany()
        {
            _repository.DeleteCompany(_writer, SelectedCompany);
            var companyToRemove = AllCompanies.FirstOrDefault(c => c.Id == SelectedCompany.Id);
            if (companyToRemove != null)
                AllCompanies.Remove(companyToRemove);
            SelectedCompany = null;
            SendCompanyCommand.Execute(null);
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
                try
                {
                    //await GenerateNewDataIfEmpty();
                }

                catch (Exception ex)
                {
                    return;
                }
            }
            
            AllCompanies = new ObservableCollection<ICompany>();
            AllCompanies.AddRange(companies);
            SelectedCompany = AllCompanies.FirstOrDefault(c => c.Id == companyId);
        }
        
        private async Task GenerateNewDataIfEmpty()
        {
            await _repository.WriteDummyData(_writer);
            await LoadDataAsync(0);
        }
    }
}
