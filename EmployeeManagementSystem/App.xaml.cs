using EmployeeManagementSystem.LogManager;
using EmployeeManagementSystem.Service;
using EmployeeManagementSystem.View;
using EmployeeManagementSystem.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Configuration;
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
            SetUpLog();
            Logger.Information("Has been started");
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private static void SetUpLog()
        {
            Serilog.Debugging.SelfLog.Enable(Console.WriteLine);
            Log.Logger = new LoggerConfiguration()
                                    .Enrich.FromLogContext()
                                    .MinimumLevel.Verbose()
                                    .WriteTo.File($"Logs/{DateTime.Now:yyyyMMdd_HHmmss}.txt")
                                    .CreateLogger();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddTransient<InputWindow>();
            services.AddTransient(typeof(IDialogWindowService<>), typeof(DialogWindowService<>));
            services.AddSingleton<EmployeeViewModel>();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Dispose of services if needed
            if (ServiceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }

            Logger.Information("Has been closed..");
        }
    }

}
