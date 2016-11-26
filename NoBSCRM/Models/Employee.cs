using GalaSoft.MvvmLight;

namespace NoBSCRM.Models
{
    public class Employee : ObservableObject
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Mobil { get; set; }
        public string EMail { get; set; }
        public string Position { get; set; }
    }
}
