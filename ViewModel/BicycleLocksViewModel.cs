using ClientSamokat.Additions;
using ClientSamokat.HttpContext;
using ClientSamokat.LongPolling;
using ClientSamokat.Model;
using ClientSamokat.View;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClientSamokat.ViewModel
{
    public class BicycleLocksViewModel : ObservableObject
    {
        private readonly IQueryBuilder queryBuilder;

        private int lockid;
        private int occ_id;

        private ICommand _addNew;

        public ICommand AddNew
        {
            get => _addNew ??= new MyCommand(async obj =>
            {
                await PostData();
            });
        }

        public int LockId
        {
            get => lockid;
            set => SetProperty(ref lockid, value);  
        }

        public BicycleLocksViewModel(int occ_id, IQueryBuilder _QB)
        {
            this.occ_id = occ_id;   
            this.queryBuilder = _QB;
        }

        private async Task PostData()
        {

            BicyclesLock _temp = new BicyclesLock()
            {
                LastDateInspect = DateTime.UtcNow,
                Latitude = 37.882611,
                LockId = this.LockId,
                Longitude = 51.313824,
                Stat = false,
                Occ_Id = occ_id,
            };

            PostService postService = new PostService();

            if (await postService.Post<BicyclesLock>(opt =>
            {
                opt.DefaultRequestHeaders.Accept.Clear();
                opt.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                opt.BaseAddress = new Uri(queryBuilder.GetQueryString(null, "Add_New_BiL_Conn"));

            }, _temp))
            {
                PopUpErrorMessage _popUpWindow = new("Новый велозамок успешно добавлен!", "Успех!");
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
