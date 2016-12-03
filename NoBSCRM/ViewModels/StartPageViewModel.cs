using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
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
        private bool _isLoading = false;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                Set(() => IsLoading, ref _isLoading, value);
            }
        }
        
        private IViewModel _firstColumnViewModel;
        public IViewModel FirstColumnViewModel { get { return _firstColumnViewModel;} set
        {
                if (value == _firstColumnViewModel)
                    return;
                Set(() => FirstColumnViewModel, ref _firstColumnViewModel, value);
        } }

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

        public StartPageViewModel(IEntityViewModelFactory entityViewModelFactory, ICompanyViewModel companyViewModel, ICompanyListViewModel companyListViewModel)
        {
            FirstColumnViewModel = companyListViewModel;
            SecondColumnViewModel = companyViewModel;
            ThirdColumnViewModel = null;
            Messenger.Default.Register<SelectedEntityMessenger>(this, (entity) =>
            {
                var type = entity.SelectedEntity.GetType().ToString();
                string[] tempArray = type.Split('.');
                ThirdColumnViewModel = entityViewModelFactory.GetEntityViewModel(tempArray[tempArray.Length-1], entity.SelectedEntity);
            });
        }
    }
}
