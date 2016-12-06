using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using NoBSCRM.Messages;
using NoBSCRM.Models;
using NoBSCRM.Repositories;
using NoBSCRM.Utils;

namespace NoBSCRM.ViewModels
{
    public class CompanyListViewModel : ViewModelBase, ICompanyListViewModel
    {
        public ObservableCollection<Company> AllCompanies { get; set; }

        public RelayCommand<Company> SendCompanyCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }

        public Task Initialization { get; private set; }

        
        private IRepository _repository;
        private IReader _reader;
        private IWriter _writer;

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
            Initialization = LoadDataAsync();
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

        private void SendSelectedCompany(Company company)
        {
            Messenger.Default.Send<SelectedCompanyMessenger>(new SelectedCompanyMessenger() { SelectedCompany = company });
        }

        // Todo
        // Clean the generation of dummy data. Move to Mock repository?
        //

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
    }
}
