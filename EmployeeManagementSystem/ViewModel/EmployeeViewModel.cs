using EmployeeManagementSystem.Common;
using EmployeeManagementSystem.DataSource;
using EmployeeManagementSystem.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace EmployeeManagementSystem.ViewModel
{
    public class EmployeeViewModel : BaseViewModel
    {
        public ObservableCollection<Employee> Employees { get; set; } = [];
        private ICollectionView _employeesView;
        public ICollectionView EmployeesView
        {
            get
            {
                if (_employeesView == null)
                {
                    _employeesView = CollectionViewSource.GetDefaultView(Employees);
                    _employeesView.Filter = FilterEmployees;
                }
                return _employeesView;
            }
        }

        private Employee selectedEmployee;
        public Employee SelectedEmployee
        {
            get => selectedEmployee;
            set
            {
                if (selectedEmployee != value)
                {
                    selectedEmployee = value;
                    OnPropertyChanged(nameof(SelectedEmployee));
                }
            }
        }

        private string searchQuery;
        public string SearchQuery
        {
            get => searchQuery;
            set
            {
                if (searchQuery != value)
                {
                    searchQuery = value;
                    OnPropertyChanged(nameof(SearchQuery));
                    SearchEmployees();
                }
            }
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SearchCommand { get; }

        public EmployeeViewModel()
        {
            
            foreach (var employee in Mock.Employees) 
            {
                Employees.Add(employee);
            }

            AddCommand = new RelayCommand(AddEmployee);
            EditCommand = new RelayCommand(EditEmployee, CanEditOrDelete);
            DeleteCommand = new RelayCommand(DeleteEmployee, CanEditOrDelete);
            SearchCommand = new RelayCommand(SearchEmployees);
        }

        private void AddEmployee()
        {
            // Add employee logic
        }

        private void EditEmployee()
        {
            // Edit employee logic
        }

        private void DeleteEmployee()
        {
            // Delete employee logic
        }

        private bool CanEditOrDelete()
        {
            return SelectedEmployee != null;
        }

        private void SearchEmployees()
        {
            EmployeesView.Refresh();
        }

        private bool FilterEmployees(object obj)
        {
            if (obj is Employee employee)
            {
                return string.IsNullOrEmpty(SearchQuery) || employee.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }
    }
}
