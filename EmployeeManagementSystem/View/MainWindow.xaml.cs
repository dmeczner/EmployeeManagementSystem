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
        public MainWindow(EmployeeViewModel viewModel)
        {
            InitializeComponent();
            //viewModel.InputShowAction = inputDialogService.ShowDialog;
            //viewModel.InputCloseAction = inputDialogService.Close;
            //viewModel.LoadingShowAction = loadingDialogService.ShowDialog;
            //viewModel.LoadingShowAction = loadingDialogService.Close;
            DataContext = viewModel;            
        }
    }
}