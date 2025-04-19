using System.Collections;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace EmployeeManagementSystem.Model
{
    // prism validation
    public class InputHelper : INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _errors = [];

        public int Id { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    ValidateName();
                }
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    ValidateEmail();
                }
            }
        }

        private DateTime? _birthDay;
        public DateTime? BirthDay
        {
            get => _birthDay;
            set
            {
                if (_birthDay != value)
                {
                    _birthDay = value;
                    ValidateDateTime();
                }
            }
        }

        private string _birthPlace;
        public string BirthPlace
        {
            get => _birthPlace;
            set
            {
                if (_birthPlace != value)
                {
                    _birthPlace = value;
                    ValidateRequired(nameof(BirthPlace), value);
                }
            }
        }

        private Role _selectedRole;
        public Role SelectedRole
        {
            get => _selectedRole;
            set
            {
                if (_selectedRole != value)
                {
                    _selectedRole = value;
                    ValidateRole();
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
            SelectedRole = employee.Role;
        }

        public InputHelper()
        {
        }

        public Employee GetEmployee()
        {
            return new Employee
            {
                Id = Id,
                Name = Name,
                Email = Email,
                BirthDay = BirthDay ?? DateTime.MinValue,
                BirthPlace = BirthPlace,
                Role = SelectedRole
            };
        }

        private bool ValidateRequired(string propName, string probValue)
        {
            ClearErrors(propName);
            if (string.IsNullOrWhiteSpace(probValue))
            {
                AddError(propName, $"{propName} is required.");
                return true;
            }
            return false;
        }

        private void ValidateName()
        {
            ClearErrors(nameof(Name));
            if (!ValidateRequired(nameof(Name), _name) && _name.Split(" ").Count() < 2)
            {
                AddError(nameof(Name), "Name is not valid.");
            }
        }

        private void ValidateEmail()
        {
            ClearErrors(nameof(Email));
            if (!ValidateRequired(nameof(Email), Email) && !Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                AddError(nameof(Email), "Email is not valid.");
            }
        }

        private void ValidateRole()
        {
            ClearErrors(nameof(SelectedRole));
            if (SelectedRole is null)
            {
                AddError(nameof(SelectedRole), "Please select Role.");
            }
        }

        private void ValidateDateTime()
        {
            ClearErrors(nameof(BirthDay));
            if (!BirthDay.HasValue || BirthDay.Value.Year < (DateTime.Now.Year - 100))
            {
                AddError(nameof(BirthDay), "Please set a valid Birt date.");
            }
        }

        public void ValidateEverything()
        {
            // Name
            ValidateRequired(nameof(Name), _name);

            // Email
            ValidateEmail();

            // ValidateRole
            ValidateRole();

            // ValidateBirtDay
            ValidateDateTime();

            // BirthPlace
            ValidateRequired(nameof(BirthPlace), _birthPlace);
        }

        public bool HasErrors => _errors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !_errors.ContainsKey(propertyName))
            {
                return null;
            }

            return _errors[propertyName];
        }

        private void AddError(string propertyName, string error)
        {
            if (!_errors.ContainsKey(propertyName))
            {
                _errors[propertyName] = [];
            }

            if (!_errors[propertyName].Contains(error))
            {
                _errors[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        private void ClearErrors(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }

        protected void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
