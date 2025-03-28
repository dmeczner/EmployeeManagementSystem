using System.ComponentModel;

namespace EmployeeManagementSystem.Model
{
    public class Employee : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private Role _role;
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

        public Role Role
        {
            get => _role;
            set
            {
                if (_role != value)
                {
                    _role = value;
                    OnPropertyChanged(nameof(Role));
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
        public void ChangeEmployee(Employee employee)
        {
            if (Id == employee.Id)
            {
                Name = employee.Name;
                Role = employee.Role;
                Email = employee.Email;
                BirthDay = employee.BirthDay;
                BirthPlace = employee.BirthPlace;
            }
        }

        public void ChangeEmployee(InputHelper helper)
        {
            if (Id == helper.Id)
            {
                Name = helper.Name;
                Role = helper.SelectedRole;
                Email = helper.Email;
                BirthDay = helper.BirthDay ?? DateTime.MinValue;
                BirthPlace = helper.BirthPlace;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
