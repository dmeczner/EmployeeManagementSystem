using System.ComponentModel;

namespace EmployeeManagementSystem.Model
{
    public class InputHelper : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private string _email;
        private DateTime _birthDay;
        private string _birthPlace;

        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        public DateTime BirthDay
        {
            get => _birthDay;
            set
            {
                if (_birthDay != value)
                {
                    _birthDay = value;
                    OnPropertyChanged(nameof(BirthDay));
                }
            }
        }

        public string BirthPlace
        {
            get => _birthPlace;
            set
            {
                if (_birthPlace != value)
                {
                    _birthPlace = value;
                    OnPropertyChanged(nameof(BirthPlace));
                }
            }
        }

        public InputHelper(Employee employee) 
        {
            Id = employee.Id;
            Name = employee.Name;
            Email = employee.Email;
            BirthDay = employee.BirthDay;
            BirthPlace = employee.BirthPlace;
        }

        public InputHelper()
        {
        }

        public Employee GetEmployee(Role role)
        {
            return new Employee { Id = Id, BirthDay = (DateTime)BirthDay, BirthPlace = BirthPlace, Email = Email, Role = role };
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
