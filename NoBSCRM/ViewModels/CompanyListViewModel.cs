using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public RelayCommand<Company> SendCompanyCommand { get; set; }

        public Task Initialization { get; private set; }

        private Company _selectedCompany;
        private IRepository _repository;
        private IReader _reader;
        private IWriter _writer;

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

        


        public CompanyListViewModel(IRepository repository, IReader reader, IWriter writer)
        {
            _writer = writer;
            _reader = reader;
            _repository = repository;
            SendCompanyCommand = new RelayCommand<Company>(SendSelectedCompany);
            Initialization = LoadDataAsync();
        }

        private void SendSelectedCompany(Company company)
        {
            if (company != null)
            {
                Messenger.Default.Send<SelectedCompanyMessenger>(new SelectedCompanyMessenger() { SelectedCompany = company });
            }
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
    }
}
