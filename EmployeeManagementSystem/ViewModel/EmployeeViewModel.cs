using EmployeeManagementSystem.Common;
using EmployeeManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeManagementSystem.ViewModel
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Employee> employees;
        private Employee selectedEmployee;
        private string searchQuery;

        public ObservableCollection<Employee> Employees
        {
            get => employees;
            set
            {
                if (employees != value)
                {
                    employees = value;
                    OnPropertyChanged(nameof(Employees));
                }
            }
        }

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
            Employees = new ObservableCollection<Employee>();
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
            // Search employees logic
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
