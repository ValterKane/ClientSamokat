using ClientSamokat.Components;
using ClientSamokat.HttpContext;
using ClientSamokat.LongPolling;
using ClientSamokat.Model;
using ClientSamokat.View;
using ClientSamokat.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace ClientSamokat
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices(serv =>
                {
                    serv.AddSingleton<Application ,App>();
                    serv.AddSingleton<DataContext>().AddSingleton<DataPoller>();
                    serv.AddSingleton<AppShell>();

                    serv.AddTransient<Window, AutorizeView>().AddTransient<BicycleAddVM>();

                    serv.AddSingleton<AutorizationVM>().AddSingleton<GeneralVM>().AddSingleton<CourierVM>();


                    serv.AddTransient<IQueryBuilder, QueryBuilder>();
                    serv.AddTransient<IHashProvider, Sha256Converter>();
                    
                })
                .Build();

            AppShell? appShell = host.Services.GetService<AppShell>();

            appShell?.Run();
        }

    }
}
