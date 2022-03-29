using System.Threading;
using System.Windows;

namespace Udemy.Spock.ParallelProgramming.MultiThreading
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Mutex _instanceMutex;

        protected override void OnStartup(StartupEventArgs e)
        {
            _instanceMutex = new Mutex(true, @"Global\BooksLib", out var createdNew);
            if (!createdNew)
            {
                Current.Shutdown();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _instanceMutex?.ReleaseMutex();
        }
    }
}
