using ClientSamokat.Additions;
using ClientSamokat.HttpContext;
using ClientSamokat.LongPolling;
using ClientSamokat.Model;
using ClientSamokat.View;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Windows.Input;

namespace ClientSamokat.ViewModel
{
    public class BicycleAddVM : ObservableObject
    {
        private readonly AppShell _shell;
        private readonly IServiceProvider _serviceProvider;
        private readonly IQueryBuilder queryBuilder;
        private readonly ICommand addNewBicycle;

        private string _bicycleName;
        private string _bicycleProd;
        private DateTime _lastTIDate = new(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        private DateTime _checkInDate = new(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        private int _occ_id;

        public int Occ_Id
        {
            get => _occ_id;
            set => SetProperty(ref _occ_id, value);
        }

        public DateTime ChechInDate
        {
            get => _checkInDate;
            set => SetProperty(ref _checkInDate, value);
        }

        public DateTime LastTIDate
        {
            get => _lastTIDate;
            set => SetProperty(ref _lastTIDate, value);
        }

        public string BicycleProd
        {
            get => _bicycleProd;
            set => SetProperty(ref _bicycleProd, value);
        }

        public string BicycleName
        {
            get { return _bicycleName; }
            set { SetProperty(ref _bicycleName, value); }
        }


        public BicycleAddVM(IServiceProvider service)
        {
            _shell = service.GetRequiredService<AppShell>();
            Occ_Id = _shell._Occ.OccId;
            _serviceProvider = service;
            queryBuilder = service.GetRequiredService<IQueryBuilder>();
        }

        public ICommand AddNew
        {
            get => addNewBicycle ?? new MyCommand(async obj =>
            {

                Bicycle newBi = new()
                {
                    BicycleName = BicycleName,
                    BicycleId = 0,
                    BicycleProducer = BicycleProd,
                    CheckInDate = ChechInDate,
                    LastTiDate = LastTIDate,
                    Occ_Id = Occ_Id,
                    Stat = false,
                };

                await SendNewBicycle(newBi);

            });


        }

        private async Task SendNewBicycle(Bicycle newBi)
        {

            PostService _tempReq = new();

            if (await _tempReq.Post(opt =>
            {
                opt.DefaultRequestHeaders.Accept.Clear();
                opt.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                opt.BaseAddress = new Uri(queryBuilder.GetQueryString(null, "Add_New_Bi_Conn"));
            }, newBi))
            {
                PopUpErrorMessage _popUpWindow = new("Новый велотранспорт успешно добавлен!", "Успех!");
                _popUpWindow.ShowDialog();
            }
            else
            {
                PopUpErrorMessage _popUpWindow = new("Ошибка при добавлении!", "Ошибка!");
                _popUpWindow.ShowDialog();
            }
        }
    }
}
