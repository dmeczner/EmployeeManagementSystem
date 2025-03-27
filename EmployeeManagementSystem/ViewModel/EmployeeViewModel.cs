using EmployeeManagementSystem.Common;
using EmployeeManagementSystem.DataSource;
using EmployeeManagementSystem.Model;
using EmployeeManagementSystem.Service;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace EmployeeManagementSystem.ViewModel
{
    public class EmployeeViewModel<T> : BaseViewModel
    {
        public Action CloseAction { get; set; }
        public ObservableCollection<Role> Roles { get; set; } = new ObservableCollection<Role>();
        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();

        private ICollectionView _rolesView;
        public ICollectionView RolesView
        {
            get
            {
                if (_rolesView == null)
                {
                    _rolesView = CollectionViewSource.GetDefaultView(Roles);
                    _rolesView.SortDescriptions.Add(new SortDescription(nameof(Role.Name), ListSortDirection.Ascending));
                }
                return _rolesView;
            }
        }

        private ICollectionView _employeesView;
        public ICollectionView EmployeesView
        {
            get
            {
                if (_employeesView == null)
                {
                    _employeesView = CollectionViewSource.GetDefaultView(Employees);
                    _employeesView.SortDescriptions.Add(new SortDescription(nameof(Employee.Name), ListSortDirection.Ascending));
                    _employeesView.Filter = FilterEmployees;
                }
                return _employeesView;
            }
        }

        private readonly IDialogWindowService<T> _dialogService;

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set => SetField(ref _selectedEmployee, value);
        }

        private InputHelper _currentInputHelper;
        public InputHelper CurrentInputHelper
        {
            get => _currentInputHelper;
            set => SetField(ref _currentInputHelper, value);
        }

        //private Role _selectedRole;
        //public Role SelectedRole
        //{
        //    get => _selectedRole;
        //    set => SetField(ref _selectedRole, value);
        //}

        private bool _isEdit;

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                if (_searchQuery != value)
                {
                    SetField(ref _searchQuery, value);
                    SearchEmployees();
                }
            }
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand ImportCommand { get; }
        public ICommand ExportCommand { get; }

        public EmployeeViewModel(IDialogWindowService<T> dialogService)
        {
            _dialogService = dialogService;

            foreach (var employee in Mock.Employees)
            {
                Employees.Add(employee);
            }

            foreach (var role in Mock.Roles)
            {
                Roles.Add(role);
            }

            AddCommand = new RelayCommand(AddEmployee);
            EditCommand = new RelayCommand(EditEmployee, CanEditOrDelete);
            DeleteCommand = new RelayCommand(DeleteEmployee, CanEditOrDelete);
            SaveCommand = new RelayCommand(Save, CanSave);
            CancelCommand = new RelayCommand(Cancel);
            ImportCommand = new RelayCommand(ImportFromJson);
            ExportCommand = new RelayCommand(ExportToJson);
        }

        private void AddEmployee()
        {
            _isEdit = false;
            CurrentInputHelper = new InputHelper();
            _dialogService.ShowDialog();
        }

        private void EditEmployee()
        {
            _isEdit = true;
            CurrentInputHelper = new InputHelper(SelectedEmployee);
            CurrentInputHelper.SelectedRole = Roles.Single(x => x.Id == SelectedEmployee.Role.Id);
            _dialogService.ShowDialog();
        }

        private void DeleteEmployee()
        {
            if (Mock.DeleteEmployee(SelectedEmployee.Id))
            {
                Employees.Remove(SelectedEmployee);
            }
        }

        private void Save()
        {
            if (_isEdit)
            {
                SelectedEmployee.ChangeEmployee(CurrentInputHelper);
                Mock.UpdateEmployee(SelectedEmployee);
            }
            else
            {
                Employees.Add(Mock.AddEmployee(CurrentInputHelper));
            }
            CloseAction?.Invoke();
        }

        private void Cancel()
        {
            CloseAction?.Invoke();
        }

        private bool CanEditOrDelete()
        {
            return SelectedEmployee != null;
        }

        private bool CanSave()
        {
            return !CurrentInputHelper.HasErrors;
        }

        private void ImportFromJson()
        {
            // Implement import logic here
        }

        private void ExportToJson()
        {
            Mock.SaveData();
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
