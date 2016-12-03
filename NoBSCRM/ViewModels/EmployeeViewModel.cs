using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.Unity;
using NoBSCRM.Messages;
using NoBSCRM.Models;

namespace NoBSCRM.ViewModels
{
    public class EmployeeViewModel : ViewModelBase, IEntityViewModel
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name)
                    return;
                Set(() => Name, ref _name, value);
            }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set
            {
                if (value == _phone)
                    return;
                Set(() => Phone, ref _phone, value);
            }
        }

        private string _mobile;
        public string Mobile
        {
            get { return _mobile; }
            set
            {
                if (value == _mobile)
                    return;
                Set(() => Mobile, ref _mobile, value);
            }
        }

        private string _eMail;
        public string Email
        {
            get { return _eMail; }
            set
            {
                if (value == _eMail)
                    return;
                Set(() => Email, ref _eMail, value);
            }
        }

        private string _position;
        public string Position
        {
            get { return _position; }
            set
            {
                if (value == _position)
                    return;
                Set(() => Position, ref _position, value);
            }
        }

        private Employee _activEmployee;
        public IEntity ActiveEntity
        {
            get {return _activEmployee;}
            set
            {
                if (value == _activEmployee)
                    return;
                Set(() => ActiveEntity as Employee, ref _activEmployee, value as Employee);
            }
        }

        [InjectionConstructor]
        public EmployeeViewModel(IEntity entity)
        {
            ActiveEntity = entity;
        }
    }
}
