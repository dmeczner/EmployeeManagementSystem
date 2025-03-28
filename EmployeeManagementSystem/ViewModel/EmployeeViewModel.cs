using EmployeeManagementSystem.Common;
using EmployeeManagementSystem.DataSource;
using EmployeeManagementSystem.Model;
using EmployeeManagementSystem.Service;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace EmployeeManagementSystem.ViewModel
{
    public class EmployeeViewModel : BaseViewModel
    {
        private DialogRegister _dialogRegister;
        public ObservableCollection<Role> Roles { get; set; } = [];
        public ObservableCollection<Employee> Employees { get; set; } = [];

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
        public ICommand LoadingCancelCommand { get; }


        private LoadingScreenUIInfo _loadingInfo;

        public LoadingScreenUIInfo LoadingInfo
        {
            get => _loadingInfo;
            set
            {
                if (_loadingInfo != value)
                {
                    SetField(ref _loadingInfo, value);
                }
            }
        }

        public EmployeeViewModel(DialogRegister dialog)
        {
            _dialogRegister = dialog;
            LoadingInfo = new LoadingScreenUIInfo();
            AddCommand = new RelayCommand(AddEmployee);
            EditCommand = new RelayCommand(EditEmployee, CanEditOrDelete);
            DeleteCommand = new RelayCommand(DeleteEmployee, CanEditOrDelete);
            SaveCommand = new RelayCommand(InputSave, CanSave);
            CancelCommand = new RelayCommand(InputCancel);
            ImportCommand = new RelayCommand(async () => await ImportFromJson());
            ExportCommand = new RelayCommand(async () => await ExportToJson());

            LoadingCancelCommand = new RelayCommand(LoadCancel);

            _dialogRegister.LoadingShowAction();

            double quantityOfEmployees = Mock.Employees.Count;
            double onePerCent = 100 / quantityOfEmployees;

            for (int i = 0; i < quantityOfEmployees; i++)
            {
                var currentEmployee = Mock.Employees[i];
                LoadingInfo.ProcessText = $"Employee Loading:{i + 1}/{quantityOfEmployees}";
                LoadingInfo.ProcessState = onePerCent * (i + 1);
                LoadingInfo.ProcessCurrent = currentEmployee.Name;
                Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    Thread.Sleep(1000);
                    Employees.Add(currentEmployee);
                }, DispatcherPriority.Background, LoadingInfo.CancelTokenSource.Token);

            }
            //foreach (var employee in Mock.Employees)
            //{
            //    Thread.Sleep(1000);
            //    Employees.Add(employee);
            //}

            foreach (var role in Mock.Roles)
            {
                Thread.Sleep(1000);
                Roles.Add(role);
            }
        }

        private void AddEmployee()
        {
            _isEdit = false;
            CurrentInputHelper = new InputHelper();
            _dialogRegister.InputShowAction();
        }

        private void EditEmployee()
        {
            _isEdit = true;
            CurrentInputHelper = new InputHelper(SelectedEmployee)
            {
                SelectedRole = Roles.Single(x => x.Id == SelectedEmployee.Role.Id)
            };
            _dialogRegister.InputShowAction();
        }

        private void DeleteEmployee()
        {
            if (Mock.DeleteEmployee(SelectedEmployee.Id))
            {
                Employees.Remove(SelectedEmployee);
            }
        }

        private void InputSave()
        {
            CurrentInputHelper.ValidateEverything();
            if (CanSave())
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
                _dialogRegister.InputCloseAction?.Invoke();

                EmployeesView.Refresh();
            }
        }

        private void InputCancel()
        {
            _dialogRegister.InputCloseAction?.Invoke();
        }

        private void LoadCancel()
        {
            LoadingInfo.CancelTokenSource.Cancel();
        }

        private bool CanEditOrDelete()
        {
            return SelectedEmployee != null;
        }

        private bool CanSave()
        {
            return !CurrentInputHelper.HasErrors;
        }

        private async Task ImportFromJson()
        {
            var result = await Mock.LoadData();
            if (result != null)
            {
                foreach (var item in result)
                {
                    Employees.Add(item);
                }
            }
        }

        private async Task ExportToJson()
        {
            await Mock.SaveData();
        }

        public void SearchEmployees()
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
