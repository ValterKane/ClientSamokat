using ClientSamokat.Model;
using ClientSamokat.View;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace ClientSamokat.ViewModel
{
    public class CourierVM : ObservableObject
    {
        private ObservableCollection<Courier> couriers;
        private readonly AppShell _shell;

        public ObservableCollection<Courier> Couriers
        {
            get => couriers ??= new ObservableCollection<Courier>();
            set => SetProperty(ref couriers, value);
        }

        public CourierVM(AppShell shell)
        {
            _shell = shell;
            LoadData();    
        }

        public void LoadData()
        {
            try
            {
                Task.Run(() =>
                {
                    while (true)
                    {
                        var Data = _shell.DataContext.couriers.ToList();

                        if (Couriers.Count > Data.Count)
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                Couriers.Clear();
                            });
                        }

                        if (Couriers.Count == 0)
                        {
                            foreach (Courier bi in Data)
                            {
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    Couriers.Add(bi);
                                });
                            }
                        }
                        else
                        {

                            for (int i = 0; i < Data.Count; i++)
                            {

                                if (i > Couriers.Count)
                                {
                                    Application.Current.Dispatcher.Invoke(() =>
                                    {
                                        Couriers.Add(Data[i]);
                                    });

                                }
                                else
                                {
                                    if (!Couriers[i].Equals(Data[i]))
                                    {
                                        Application.Current.Dispatcher.Invoke(() =>
                                        {
                                            Couriers[i] = Data[i];
                                        });
                                    }

                                }
                            }
                        }
                        Thread.Sleep(_shell.RefreshTimer);
                    }
                });

            }
            catch (Exception ex)
            {
                PopUpErrorMessage _popUpWindow = new(ex.Message, "Ошибка!");
                _popUpWindow.ShowDialog();

            }

        }

    }
}
