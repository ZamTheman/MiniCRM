using System;
using GalaSoft.MvvmLight;

namespace ModelLayer
{
    public class HistoryPost : ObservableObject, IEntity
    {
        public DateTime Date { get; set; }
        public string Post { get; set; }
        public int Id { get; set; }
    }
}
