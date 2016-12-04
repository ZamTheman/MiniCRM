﻿using System;
using System.Globalization;
using System.Xml.Serialization;
using GalaSoft.MvvmLight;

namespace NoBSCRM.Models
{
    public class Todo : ObservableObject, IEntity
    {
        //[XmlIgnore]
        //private DateTime date { get; set; }

        //[XmlElement(DataType = "string", ElementName = "Date")]
        //public string Date
        //{
        //    get { return this.date.ToString("yyyy-MM-dd"); }
        //    set { this.date = DateTime.ParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture); }
        //}

        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
