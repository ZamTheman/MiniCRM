﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using ModelLayer;
using Repositories;
using Utils;
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
        
        private readonly IEntityViewModelFactory _entityViewModelFactory;
        
        public StartPageViewModel(IEntityViewModelFactory entityViewModelFactory, ICompanyViewModel companyViewModel, ICompanyListViewModel companyListViewModel)
        {
            _entityViewModelFactory = entityViewModelFactory;
            FirstColumnViewModel = companyListViewModel;
            SecondColumnViewModel = companyViewModel;
            ThirdColumnViewModel = null;
            RegisterMessages();
            //Initialization = UglyTests(); // To test stuff not accessible from unittest
        }

        // Ugly test area

        //public Task Initialization { get; private set; }

        //private async Task UglyTests()
        //{
        //    var writer = new XMLWriter();
        //    await writer.DeleteSingleEntityByIdAsync(1, new Employee() { Id = 1 });
        //    string hey = "";
        //}

        // Ugly test area end

        private void RegisterMessages()
        {
            Messenger.Default.Register<SelectedEntityMessenger>(this, (entity) =>
            {
                ThirdColumnViewModel = null;
                if (entity.SelectedEntityMessageEntity == null)
                    return;
                var type = entity.SelectedEntityMessageEntity.GetType().ToString();
                string[] tempArray = type.Split('.');
                ThirdColumnViewModel = _entityViewModelFactory.GetEntityViewModel(tempArray[tempArray.Length - 1], entity.SelectedEntityMessageEntity, SelectedCompany.Id);
            });

            Messenger.Default.Register<SelectedCompanyMessenger>(this, (company) =>
            {
                this.SelectedCompany = company.SelectedCompany;
            });
        }

        public ICompany SelectedCompany { get; set; }
    }
}
