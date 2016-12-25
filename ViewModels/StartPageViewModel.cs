using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using ModelLayer;
using Utils.Messages;

namespace ViewModels
{
    public class StartPageViewModel : ViewModelBase, IStartPageViewModel
    {
        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                Set(() => IsLoading, ref _isLoading, value);
            }
        }
        
        private IViewModel _firstColumnViewModel;
        public IViewModel FirstColumnViewModel
        {
            get { return _firstColumnViewModel; }
            set
            {
                if (value == _firstColumnViewModel)
                    return;
                Set(() => FirstColumnViewModel, ref _firstColumnViewModel, value);
            }
        }

        private IViewModel _secondColumnViewModel;
        public IViewModel SecondColumnViewModel
        {
            get { return _secondColumnViewModel; }
            set
            {
                if (value == _firstColumnViewModel)
                    return;
                Set(() => SecondColumnViewModel, ref _secondColumnViewModel, value);
            }
        }

        private IViewModel _thirdColumnViewModel;
        public IViewModel ThirdColumnViewModel
        {
            get { return _thirdColumnViewModel; }
            set
            {
                if (value == _thirdColumnViewModel)
                    return;
                Set(() => ThirdColumnViewModel, ref _thirdColumnViewModel, value);
            }
        }
        
        private readonly IEntityViewModelFactory _entityViewModelFactory;

        public ICompany SelectedCompany { get; set; }
        public RelayCommand GoBackCommand { get; }
        public RelayCommand GoForwardCommand { get; }

        private IViewModel _companyListViewModel;
        private IViewModel _companyViewModel;
        private IViewModel _entityViewModel;

        private bool _firstColumnVisibility;
        public bool FirstColumnVisibility
        {
            get { return _firstColumnVisibility; }
            set
            {
                Set(() => FirstColumnVisibility, ref _firstColumnVisibility, value);
                GoBackCommand.RaiseCanExecuteChanged();
                GoForwardCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _secondColumnVisibility;
        public bool SecondColumnVisibility
        {
            get { return _secondColumnVisibility; }
            set
            {
                Set(() => SecondColumnVisibility, ref _secondColumnVisibility, value);
                GoBackCommand.RaiseCanExecuteChanged();
                GoForwardCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _thirdColumnVisibility;
        public bool ThirdColumnVisibility
        {
            get { return _thirdColumnVisibility; }
            set
            {
                Set(() => ThirdColumnVisibility, ref _thirdColumnVisibility, value);
                GoBackCommand.RaiseCanExecuteChanged();
                GoForwardCommand.RaiseCanExecuteChanged();
            }
        }

        public StartPageViewModel(IEntityViewModelFactory entityViewModelFactory, ICompanyViewModel companyViewModel, ICompanyListViewModel companyListViewModel)
        {
            _companyListViewModel = companyListViewModel;
            _companyViewModel = companyViewModel;
            _entityViewModelFactory = entityViewModelFactory;
            _firstColumnVisibility = true;
            _secondColumnVisibility = true;
            _thirdColumnVisibility = true;
            FirstColumnViewModel = _companyListViewModel;
            SecondColumnViewModel = _companyViewModel;
            ThirdColumnViewModel = null;
            RegisterMessages();
            GoBackCommand = new RelayCommand(GoBackInViewModels, CanGoBackInViewModels);
            GoForwardCommand = new RelayCommand(GoForwardInViewModels, CanGoForwardInViewModels);
        }

        private void GoForwardInViewModels()
        {
            switch (CheckSize())
            {
                case "Small":
                    if (_firstColumnVisibility)
                    {
                        FirstColumnVisibility = false;
                        SecondColumnVisibility = true;
                        ThirdColumnVisibility = false;
                    }
                    else
                    {
                        SecondColumnVisibility = false;
                        ThirdColumnVisibility = true;
                    }
                    break;
                case "Medium":
                    FirstColumnVisibility = false;
                    SecondColumnVisibility = true;
                    ThirdColumnVisibility = true;
                    break;
            }
            GoBackCommand.RaiseCanExecuteChanged();
            GoForwardCommand.RaiseCanExecuteChanged();
        }
        
        private void GoBackInViewModels()
        {
            switch (CheckSize())
            {
                case "Small":
                    if (_thirdColumnVisibility)
                    {
                        FirstColumnVisibility = false;
                        SecondColumnVisibility = true;
                        ThirdColumnVisibility = false;
                    }
                    else
                    {
                        FirstColumnVisibility = true;
                        SecondColumnVisibility = false;
                    }
                    break;
                case "Medium":
                    FirstColumnVisibility = true;
                    SecondColumnVisibility = true;
                    ThirdColumnVisibility = false;
                    break;
            }
            GoBackCommand.RaiseCanExecuteChanged();
            GoForwardCommand.RaiseCanExecuteChanged();
        }

        private string CheckSize()
        {
            if ((FirstColumnVisibility && SecondColumnVisibility) || (SecondColumnVisibility && ThirdColumnVisibility))
                return "Medium";
            if ((FirstColumnVisibility && !SecondColumnVisibility && !ThirdColumnVisibility) ||
                    (!FirstColumnVisibility && SecondColumnVisibility && !ThirdColumnVisibility) ||
                    (!FirstColumnVisibility && !SecondColumnVisibility && ThirdColumnVisibility))
                return "Small";

            return "Full";
        }

        private bool CanGoForwardInViewModels()
        {
            if (_thirdColumnVisibility)
                return false;
            return true;
        }

        private bool CanGoBackInViewModels()
        {
            if (_firstColumnVisibility)
                return false;
            return true;
        }

        private void RegisterMessages()
        {
            Messenger.Default.Register<SelectedEntityMessenger>(this, (company) =>
            {
                ThirdColumnViewModel = null;
                if (company.SelectedEntityMessageEntity == null)
                    return;
                var type = company.SelectedEntityMessageEntity.GetType().ToString();
                string[] tempArray = type.Split('.');
                _entityViewModel = _entityViewModelFactory.GetEntityViewModel(tempArray[tempArray.Length - 1], company.SelectedEntityMessageEntity, SelectedCompany.Id);
                ThirdColumnViewModel = _entityViewModel;
            });

            Messenger.Default.Register<SelectedCompanyMessenger>(this, (company) =>
            {
                this.SelectedCompany = company.SelectedCompany;
            });
        }
        

        
    }
}
