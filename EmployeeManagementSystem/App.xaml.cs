using EmployeeManagementSystem.Service;
using EmployeeManagementSystem.View;
using EmployeeManagementSystem.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace EmployeeManagementSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider? ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddTransient<InputWindow>();
            services.AddTransient<LoadingScreen>();
            services.AddTransient(typeof(IDialogWindowService<>), typeof(DialogWindowService<>));
            services.AddSingleton<EmployeeViewModel>();
            services.AddSingleton<DialogRegister>();
        }

        private void OnExit(object sender, ExitEventArgs e)
        {
            // Dispose of services if needed
            if (ServiceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }

}
