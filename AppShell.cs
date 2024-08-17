using ClientSamokat.LongPolling;
using ClientSamokat.Model;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace ClientSamokat
{
    public class AppShell
    {
        public bool? AA_Flag { get; set; } = false;

        public Occ _Occ { get; set; }

        public int AdminId { get; set; }    

        private Application? _appContext;

        private static DataPoller _poller;

        private readonly IServiceProvider? ServiceProvider;

        public DataContext DataContext { get; private set; }

        public TimeSpan RefreshTimer { get; set; } = new TimeSpan(0, 0, 2);


        public AppShell(IServiceProvider? Services, DataContext _context)
        {
            ServiceProvider = Services;
            _poller = ServiceProvider.GetRequiredService<DataPoller>();
            DataContext = _context;
            
        }

        public int Run()
        {
            _appContext = ServiceProvider?.GetService < Application>() ?? throw new ArgumentNullException("App нет в DI");
            _appContext.ShutdownMode = ShutdownMode.OnMainWindowClose;
            return _appContext.Run();
        }

        public void SetMainView(Window window)
        {
            _appContext.MainWindow.Visibility = Visibility.Collapsed;
            _appContext.MainWindow = window;
            _appContext.MainWindow.ShowDialog();
        }

        public void StartDataPoller()
        {
            _poller.OpenConnection(_Occ.OccId, RefreshTimer);
        }
    }
}
