using System;
using System.Globalization;
using System.Xml.Serialization;
using GalaSoft.MvvmLight;

namespace NoBSCRM.Models
{
    public class HistoryPost : ObservableObject
    {
        [XmlIgnore]
        private DateTime date;

        [XmlElement(DataType = "string", ElementName = "Date")]
        public string Date
        {
            get { return this.date.ToString("yyyy-MM-dd"); }
            set { this.date = DateTime.ParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture); }
        }

        public string Post { get; set; }
    }
}
