using ClientSamokat.HttpContext;
using ClientSamokat.LongPolling;
using ClientSamokat.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net.Http.Headers;
using System.Windows.Media.Imaging;

namespace ClientSamokat.ViewModel
{
    public class MapControlVM : ObservableObject
    {
        private readonly GetService requstServer;
        private readonly IQueryBuilder _queryBuilder;
        private BitmapImage _image;
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public BitmapImage? MapImage
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }
        public BicyclesLock Obj { get; private set; }

       

        public MapControlVM(BicyclesLock obj, IQueryBuilder queryBuilder)
        {
            requstServer = new();
            Obj = obj;
            _queryBuilder = queryBuilder;
            loadImage();
        }

        public void loadImage()
        {
            byte[] byteArray = Task.Factory.StartNew(() =>
            {
                return requstServer.GetByteContent(opt =>
                {
                    opt?.DefaultRequestHeaders.Accept.Clear();
                    opt?.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    opt.BaseAddress = new Uri(
                        _queryBuilder.GetQueryString(new KeyValuePair<string, string>[]
                        {
                        new("apikey", "bf51d061-1d59-40df-9f03-846f3a5e6399"),
                        new("ll", $"{Obj.Latitude.ToString(CultureInfo.InvariantCulture)},{Obj.Longitude.ToString(CultureInfo.InvariantCulture)}"),
                        new("pt", $"{Obj.Latitude.ToString(CultureInfo.InvariantCulture)},{Obj.Longitude.ToString(CultureInfo.InvariantCulture)},pm2pnl"),
                        new("size", "650,450"),
                        new("z","17")
                        }, "StaticMapAPI"));

                }).Result;
            }).Result;
                
                
               
            if(byteArray != null )
            {
               using(var mem = new MemoryStream(byteArray))
                {
                    MapImage = new BitmapImage();
                    mem.Position = 0;
                    MapImage.BeginInit();
                    MapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    MapImage.CacheOption = BitmapCacheOption.OnLoad;
                    MapImage.UriSource = null;
                    MapImage.StreamSource = mem;
                    MapImage.EndInit();
                }
                MapImage.Freeze();
            }
            

        }
    }
}
