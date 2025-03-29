using EmployeeManagementSystem.Model;
using EmployeeManagementSystem.Service;
using EmployeeManagementSystem.ViewModel;
using Moq;

namespace EmployeeManagementSystem.Tests
{
    [TestFixture]
    public class EmployeeViewModelTests
    {
        private EmployeeViewModel _viewModel;
        private Mock<Action> _mockInputShowDialogAction;
        private Mock<Action> _mockInputCloseAction;

        [SetUp]
        public void Setup()
        {
            _mockInputShowDialogAction = new Mock<Action>();
            _mockInputCloseAction = new Mock<Action>();

            _viewModel = new EmployeeViewModel
            {
                InputShowDialogAction = _mockInputShowDialogAction.Object,
                InputCloseAction = _mockInputCloseAction.Object
            };
        }

        [Test]
        public void AddEmployee_ShouldAddNewEmployee()
        {
            // Arrange
            _viewModel.CurrentInputHelper = new InputHelper();

            // Act
            _viewModel.AddCommand.Execute(null);

            // Assert
            _mockInputShowDialogAction.Verify(ds => ds(), Times.Once);
        }

        [Test]
        public void EditEmployee_ShouldEditSelectedEmployee()
        {
            // Arrange
            var employee = new Employee { Id = 1, Name = "John Doe", Role = new Role { Id = 1, Name = "Developer" } };
            _viewModel.SelectedEmployee = employee;

            // Act
            _viewModel.EditCommand.Execute(null);

            // Assert
            _mockInputShowDialogAction.Verify(ds => ds(), Times.Once);
            Assert.That(employee.Name, Is.EqualTo(_viewModel.CurrentInputHelper.Name));
        }

        [Test]
        public void DeleteEmployee_ShouldRemoveSelectedEmployee()
        {
            // Arrange
            var employee = new Employee { Id = 1, Name = "John Doe" };
            _viewModel.Employees.Add(employee);
            _viewModel.SelectedEmployee = employee;

            // Act
            _viewModel.DeleteCommand.Execute(null);

            // Assert
            Assert.That(_viewModel.Employees, Does.Not.Contain(employee));
        }

        [Test]
        public void Cancel_ShouldInvokeCloseAction()
        {
            // Arrange
            bool isClosed = false;
            _mockInputCloseAction.Setup(ds => ds()).Callback(() => isClosed = true);

            // Act
            _viewModel.CancelCommand.Execute(null);

            // Assert
            Assert.That(isClosed, Is.True);
        }

        [Test]
        public void SearchEmployees_ShouldFilterEmployees()
        {
            // Arrange
            var employee1 = new Employee { Name = "John Doe" };
            var employee2 = new Employee { Name = "Jane Smith" };
            _viewModel.Employees.Add(employee1);
            _viewModel.Employees.Add(employee2);
            _viewModel.SearchQuery = "Jane";

            // Act
            _viewModel.SearchEmployees();

            // Assert
            Assert.That(_viewModel.EmployeesView.Cast<Employee>().Contains(employee2), Is.True);
            Assert.That(_viewModel.EmployeesView.Cast<Employee>().Contains(employee1), Is.False);
        }
    }
}