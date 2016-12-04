using System;
using System.Globalization;
using System.Xml.Serialization;
using GalaSoft.MvvmLight;

namespace NoBSCRM.Models
{
    public class HistoryPost : ObservableObject, IEntity
    {
        public DateTime Date { get; set; }
        public string Post { get; set; }
    }
}
