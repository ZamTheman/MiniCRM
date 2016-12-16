using GalaSoft.MvvmLight;

namespace ModelLayer
{
    public class Employee : ObservableObject, IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
    }
}
