using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
    public class StartPageViewModel : ViewModelBase, IStartPageViewModel
    {
        public ICompanyListViewModel _companyListViewModel;
        public ICompanyViewModel _companyViewModel;
        public ICompanyViewModel CompanyViewModel { get { return _companyViewModel; } set { _companyViewModel = value; } }
        
        private bool _isLoading = false;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                Set(() => IsLoading, ref _isLoading, value);
            }
        }

        private Company _selectedCompany;
        public Company SelectedCompany
        {
            get { return _selectedCompany; }
            set
            {
                if (value == _selectedCompany)
                    return;
                Set(() => SelectedCompany, ref _selectedCompany, value);
                SendCompanyCommand.Execute(value);
            }
        }

        private IRepository _repository;
        private IReader _reader;
        private IWriter _writer;

        public ObservableCollection<Company> AllCompanies { get; set; }

        public Task Initialization { get; private set; }
        
        public RelayCommand<Company> SendCompanyCommand { get; set; } 

        public StartPageViewModel(IRepository repository, IReader reader, IWriter writer, ICompanyListViewModel companyListViewModel, ICompanyViewModel companyViewModel)
        {
            this._repository = repository;
            this._reader = reader;
            this._writer = writer;
            this._companyListViewModel = companyListViewModel;
            this.CompanyViewModel = companyViewModel;
            SendCompanyCommand = new RelayCommand<Company>(SendSelectedCompany);
            Initialization = LoadDataAsync();
        }
        
        private async Task LoadDataAsync()
        {
            var companies = new List<Company>();
            try
            {
                companies = await _repository.GetAll(_reader) as List<Company>;
            }

            catch
            {
                await _repository.WriteDummyData(_writer);
                companies = await _repository.GetAll(_reader) as List<Company>;
            }

            AllCompanies = new ObservableCollection<Company>();
            AllCompanies.AddRange(companies);
        }

        private void SendSelectedCompany(Company company)
        {
            if (company != null)
            {
                Messenger.Default.Send<MessageCommunicator>(new MessageCommunicator() {selectedCompany = company});
            }
        }
    }
}
