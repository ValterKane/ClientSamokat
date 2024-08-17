using ClientSamokat.Additions;
using ClientSamokat.HttpContext;
using ClientSamokat.LongPolling;
using ClientSamokat.Model;
using ClientSamokat.View;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ClientSamokat.ViewModel
{
    public class AutorizationVM : ObservableObject
    {
        public int Id { get; set; } = 3;
        public string Password { get; set; }

        private readonly IQueryBuilder queryBuilder;
        private readonly GetService getService;
        private ICommand autorizeCmd;
        private readonly AppShell _shell;
        private readonly IServiceProvider Services;

        public ICommand AutorizeCmd
        {
            get
            {
                return autorizeCmd = new MyCommand(async obj =>
                {
                    if (obj is not null)
                    {
                        await GetAdminsOcc();
                        await GetOnline();
                        _shell.SetMainView(new GeneralView(Services.GetRequiredService<GeneralVM>()));
                        //Password = obj?.ToString();
                        //await Autorize();
                        //await GetAdminsOcc();
                        //if (_shell.AA_Flag == true)
                        //{
                        //    _shell.SetMainView(new GeneralView(Services.GetRequiredService<GeneralVM>()));

                        //}
                    }
                });
            }
        }

        public AutorizationVM(IQueryBuilder Builder, IServiceProvider provider)
        {
            queryBuilder = Builder;
            Services = provider;
            _shell = Services.GetRequiredService<AppShell>();
            getService = new();
        }

        private async Task Autorize()
        {
           
            bool flag = await getService.Get<bool>(opt =>
            {
                opt.DefaultRequestHeaders.Accept.Clear();
                opt.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                opt.BaseAddress = new Uri(queryBuilder.GetQueryString(new KeyValuePair<string, string>[]
                {
                         new("id", Id.ToString()),
                         new("hash", Password),
                }, "Aut_MS_Conn"));

            });

            _shell.AA_Flag = flag;
            _shell.AdminId = Id;    
        }

        private async Task GetAdminsOcc()
        {

            Occ data = await getService.Get<Occ>(opt =>
            {
                opt.DefaultRequestHeaders.Accept.Clear();
                opt.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                opt.BaseAddress = new Uri(queryBuilder.GetRESTString(Id.ToString(), "Aut_MS_Occ"));
            });

            Services.GetRequiredService<DataContext>().occs = data;
            _shell._Occ = data;

            _shell.StartDataPoller();
        }

        private async Task GetOnline()
        {

            PutService _tempPut = new();

            await _tempPut.Put(opt =>
            {
                opt.DefaultRequestHeaders.Accept.Clear();
                opt.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                opt.BaseAddress = new Uri(queryBuilder.GetRESTString(Id.ToString(), "Aut_Set_On"));
            });
        }
    }
}
