using Windows.UI.Xaml.Data;
using GalaSoft.MvvmLight;  
using GalaSoft.MvvmLight.Views;  
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using NoBSCRM.Models;
using NoBSCRM.Repositories;
using NoBSCRM.Utils;
using UnityServiceLocator = Microsoft.Practices.Unity.UnityServiceLocator;

namespace NoBSCRM.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary> 
    
    [Bindable]
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            var unityContainer = new UnityContainer();
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(unityContainer));
            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models
            }
            else
            {
                // Create run time view services and models
            }

            //Register your services used here
            unityContainer.RegisterType<INavigationService, NavigationService>();
            unityContainer.RegisterType<IStartPageViewModel, StartPageViewModel>();
            unityContainer.RegisterType<ICompanyListViewModel, CompanyListViewModel>();
            unityContainer.RegisterType<ICompanyViewModel, CompanyViewModel>();
            unityContainer.RegisterType<IRepository, Repository>();
            unityContainer.RegisterType<IReader, XMLReader>();
            unityContainer.RegisterType<IWriter, XMLWriter>();
            unityContainer.RegisterType<IEntityViewModelFactory, EntityViewModelFactory>();
        }

        // <summary>
        // Gets the StartPage view model.
        // </summary>
        // <value>
        // The StartPage view model.
        // </value>
        public StartPageViewModel StartPageInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StartPageViewModel>();
            }
        }

        // <summary>
        // The cleanup.
        // </summary>
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }

}
