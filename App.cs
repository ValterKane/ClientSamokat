using System.Net.Http.Headers;
using System.Windows;
using ClientSamokat.HttpContext;
using ClientSamokat.LongPolling;
using ClientSamokat.View;

namespace ClientSamokat
{
    public class App : Application
    {

        public App(Window MainView)
        {          
            base.MainWindow = MainView;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.MainWindow.Show();
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }


    }
}
