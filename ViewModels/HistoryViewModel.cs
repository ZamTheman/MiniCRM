using System;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.Unity;
using ModelLayer;
using Repositories;
using Utils;
using Utils.Messages;

namespace ViewModels
{
    public class HistoryViewModel : EntityViewModelBase
    {
        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (value == _date)
                    return;
                Set(() => Date, ref _date, value);
                SaveEntityCommand.RaiseCanExecuteChanged();
            }
        }

        private string _post;
        public string Post
        {
            get { return _post; }
            set
            {
                if (value == _post)
                    return;
                Set(() => Post, ref _post, value);
                SaveEntityCommand.RaiseCanExecuteChanged();
            }
        }

        private HistoryPost _activePost;
        public IEntity ActiveEntity
        {
            get { return _activePost; }
            set
            {
                if (value == _activePost)
                    return;
                Set(() => ActiveEntity as HistoryPost, ref _activePost, value as HistoryPost);
                UpdateAllFields();
            }
        }

        private HistoryPost _activeHistoryPost;
        public HistoryPost ActiveHistoryPost
        {
            get { return _activeHistoryPost; }
            set
            {
                if (value == _activeHistoryPost)
                    return;
                Set(() => ActiveHistoryPost, ref _activeHistoryPost, value);
                UpdateAllFields();
            }
        }

        private void UpdateAllFields()
        {
            if (ActiveHistoryPost != null)
                Date = Convert.ToDateTime(ActiveHistoryPost.Date);
            else
                Date = DateTime.Now;

            Post = ActiveHistoryPost.Post;
        }

        [InjectionConstructor]
        public HistoryViewModel(IEntity entity, IRepository repository, IWriter writer, int companyId) : base(repository, writer, companyId)
        {
            SaveEntityCommand = new RelayCommand(SaveEntity, CanSaveEntity);
            ActiveHistoryPost = entity as HistoryPost;
        }

        private bool CanSaveEntity()
        {
            if (ActiveHistoryPost == null)
            {
                if (!string.IsNullOrEmpty(Post))
                {
                    SaveButtonVisible = true;
                    return true;
                }
            }

            if (Date != ActiveHistoryPost.Date || Post != ActiveHistoryPost.Post)
            {
                SaveButtonVisible = true;
                return true;
            }
            SaveButtonVisible = false;
            return false;
        }

        private async void SaveEntity()
        {
            if (ActiveHistoryPost != null)
            {
                ActiveHistoryPost.Date = Date;
                ActiveHistoryPost.Post = Post;
                await Repository.SaveEntity(Writer, ActiveHistoryPost, CompanyId);
            }

            else
            {
                ActiveHistoryPost = new HistoryPost()
                {
                    Date = this.Date,
                    Post = this.Post
                };
                int id = await Repository.SaveEntity(Writer, ActiveHistoryPost, CompanyId);

                ActiveHistoryPost.Id = id;
            }
            Messenger.Default.Send(new EntityAddedMessenger() { Entity = ActiveHistoryPost, companyId = CompanyId });
        }
    }
}
