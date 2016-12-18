using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Repositories;
using Utils;

namespace ViewModels
{
    public abstract class EntityViewModelBase : ViewModelBase, IEntityViewModel
    {
        private bool _saveButtonVisible;
        public bool SaveButtonVisible
        {
            get { return _saveButtonVisible; }
            set
            {
                Set(() => SaveButtonVisible, ref _saveButtonVisible, value);

            }
        }

        private bool _addRemoveButtonsVisible;
        public bool AddRemoveButtonsVisible
        {
            get { return _addRemoveButtonsVisible; }
            set
            {
                Set(() => AddRemoveButtonsVisible, ref _addRemoveButtonsVisible, value);
            }
        }

        public IWriter Writer { get; set; }
        public IRepository Repository { get; set; }
        public int CompanyId { get; set; }
        public RelayCommand SaveEntityCommand { get; set; }

        protected EntityViewModelBase(IRepository repository, IWriter writer, int companyId)
        {
            Repository = repository;
            Writer = writer;
            CompanyId = companyId;
            AddRemoveButtonsVisible = true;
        }
    }
}
