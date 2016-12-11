using System;
using GalaSoft.MvvmLight;

namespace ModelLayer
{
    public class Todo : ObservableObject, IEntity
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
    }
}
