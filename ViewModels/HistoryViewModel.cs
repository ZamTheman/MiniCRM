using System;
using GalaSoft.MvvmLight;
using Microsoft.Practices.Unity;
using ModelLayer;
using Repositories;
using Utils;

namespace ViewModels
{
    public class HistoryViewModel : ViewModelBase, IEntityViewModel
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

        public IWriter Writer { get; set; }
        public IRepository Repository { get; set; }
        public int CompanyId { get; set; }

        private void UpdateAllFields()
        {
            Date = Convert.ToDateTime(_activePost.Date);
            Post = _activePost.Post;
        }

        [InjectionConstructor]
        public HistoryViewModel(IEntity entity, IRepository repository, IWriter writer, int companyId)
        {
            CompanyId = companyId;
            Writer = writer;
            Repository = repository;
            ActiveEntity = entity;
        }
    }
}
