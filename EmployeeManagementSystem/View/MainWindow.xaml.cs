using EmployeeManagementSystem.Service;
using EmployeeManagementSystem.ViewModel;
using System.Windows;

namespace EmployeeManagementSystem.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(EmployeeViewModel viewModel, IDialogWindowService<InputWindow> inputDialogService)
        {
            InitializeComponent();
            viewModel.InputShowDialogAction = inputDialogService.ShowDialog;
            DataContext = viewModel;            
        }
    }
}