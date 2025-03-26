using EmployeeManagementSystem.ViewModel;
using System.Windows;

namespace EmployeeManagementSystem.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EmployeeViewModel _viewModel;

        public MainWindow()
        {
            DataContext = _viewModel = new();
            InitializeComponent();
        }
    }
}