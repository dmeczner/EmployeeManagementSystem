using EmployeeManagementSystem.Common;
using EmployeeManagementSystem.Model;
using EmployeeManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EmployeeManagementSystem.View
{
    public delegate void EmployeeFunctionDelegate();

    /// <summary>
    /// Interaction logic for LoadingScreen.xaml
    /// </summary>
    public partial class LoadingScreen : Window
    {
        public EmployeeFunctionDelegate EmployeeFunction {  get; set; }

        public LoadingScreen(EmployeeViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
