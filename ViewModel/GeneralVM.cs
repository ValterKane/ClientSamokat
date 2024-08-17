using ClientSamokat.Additions;
using ClientSamokat.HttpContext;
using ClientSamokat.LongPolling;
using ClientSamokat.Model;
using ClientSamokat.View;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Input;

namespace ClientSamokat.ViewModel
{
    public class GeneralVM : ObservableObject
    {
        private readonly AppShell _shell;
        private readonly IServiceProvider _serviceProvider;

        private DateTime _dateFrom = new(DateTime.Now.Year, 01, 01);

        private DateTime _dateTo = new(DateTime.Now.Year, 12, 31);

        private ObservableCollection<Bicycle> _bb_1 = new();

        private ObservableCollection<Journal> _journal_1 = new();

        private ObservableCollection<BicyclesLock> _bl_1 = new();

        private readonly CancellationTokenSource _cancellationTokenSource = new();

        private Occ _currentOcc;
        private ICommand _openMap;
        private ICommand _openCourierList;
        private readonly ICommand _addNewBicycle;
        private ICommand _deleteBicycle;
        private ICommand _deleteBicyclesLocks;
        private readonly ICommand _addNewBicycleLocks;

        public DateTime DateFrom
        {
            get => _dateFrom;
            set => SetProperty(ref _dateFrom, value);
        }

        public DateTime DateTo
        {
            get => _dateTo;
            set => SetProperty(ref _dateTo, value);
        }

        public Occ _CurrentOcc
        {
            get => _currentOcc;
            set => SetProperty(ref _currentOcc, value);
        }

        #region CommandRegion
        public ICommand AddNewBicycle
        {
            get => _addNewBicycle ?? new MyCommand(obj =>
            {

                BicycleView bicycleView = new(_serviceProvider.GetRequiredService<BicycleAddVM>());
                bicycleView.Show();
            });
        }
        public ICommand OpenCurier
        {
            get => new MyCommand(obj =>
            {
                if (obj != null)
                {
                    if (obj is not Courier)
                    {
                        throw new Exception("Ошибка в передаче параметра. Эта команда ожидает экземпляр Courier модели");
                    }
                    else
                    {
                        Courier Object = (Courier)obj;
                        PopUpCourierView popUpCV = new(new PopUpCurierViewModel(Object));
                        popUpCV.Show();
                    }
                }
            });
        }
        public ICommand OpenMap
        {
            get
            {
                return _openMap = new MyCommand(obj =>
                {
                    if (obj is not null)
                    {
                        if (obj is not BicyclesLock)
                        {
                            throw new Exception("Ошибка в передаче параметра. Эта команда ожидает экземпляр BicyclesLock модели");
                        }
                        else
                        {
                            BicyclesLock Object = (BicyclesLock)obj;
                            MapControl map = new(new MapControlVM(Object, _serviceProvider.GetRequiredService<IQueryBuilder>()));
                            map.Show();
                        }
                    }
                });
            }
        }
        public ICommand OpenCourierList
        {
            get => _openCourierList ??= new MyCommand(obj =>
            {
                CourierView CV = new(_serviceProvider.GetRequiredService<CourierVM>());
                CV.Show();
            });


        }
        public ICommand AddNewBicycleLocks
        {
            get => _addNewBicycleLocks ?? new MyCommand(obj =>
            {
                BicycleLocksView BLV = new(new BicycleLocksViewModel(_shell._Occ.OccId, _serviceProvider.GetRequiredService<IQueryBuilder>()));
                BLV.Show();
            });
        }

        public ICommand DeleteBicycle
        {
            get => _deleteBicycle ??= new MyCommand(async obj =>
            {
                int id = (int)obj;
                DeleteService ds = new();
                if (await ds.Delete(opt =>
                {
                    opt?.DefaultRequestHeaders.Accept.Clear();
                    opt?.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    opt.BaseAddress = new Uri(_serviceProvider.GetRequiredService<IQueryBuilder>().GetRESTString(id.ToString(), "Del_Bi_Conn"));
                }))
                {
                    PopUpErrorMessage popUp = new("Велотранспорт успешно удален!", "Успех!");
                    popUp.ShowDialog();
                }
                else
                {
                    PopUpErrorMessage popUp = new("Ошибка при удалении!", "Ошибка!");
                    popUp.ShowDialog();
                }
            });
        }

        public ICommand DeleteBicyclesLocks
        {
            get => _deleteBicyclesLocks ??= new MyCommand(async obj =>
            {
                int id = (int)obj;
                DeleteService ds = new();
                if (await ds.Delete(opt =>
                {
                    opt?.DefaultRequestHeaders.Accept.Clear();
                    opt?.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    opt.BaseAddress = new Uri(_serviceProvider.GetRequiredService<IQueryBuilder>().GetRESTString(id.ToString(), "Del_BiL_Conn"));
                }))
                {
                    PopUpErrorMessage popUp = new("Велозамок успешно удален успешно удален!", "Успех!");
                    popUp.ShowDialog();
                }
                else
                {
                    PopUpErrorMessage popUp = new("Ошибка при удалении!", "Ошибка!");
                    popUp.ShowDialog();
                }
            });
        }

        #endregion

        public ObservableCollection<Journal> journals
        {
            get => _journal_1;
            set
            {
                SetProperty(ref _journal_1, value);
            }
        }

        public ObservableCollection<BicyclesLock> bicyclesLocks
        {
            get { return _bl_1; }
            set => SetProperty(ref _bl_1, value);
        }

        public ObservableCollection<Bicycle> bicycles
        {
            get => _bb_1;
            set
            {
                SetProperty(ref _bb_1, value);
            }
        }

        public GeneralVM(IServiceProvider Provider)
        {
            _serviceProvider = Provider;
            _shell = _serviceProvider.GetRequiredService<AppShell>();
            _CurrentOcc = _shell._Occ;

        }

        public void LoadData()
        {
            try
            {
                Task.Run(() =>
                {
                    while (true)
                    {

                        if (bicycles.Count > _shell.DataContext.bicycles.Count)
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                bicycles.Clear();
                            });
                        }

                        if (bicycles.Count == 0)
                        {
                            foreach (Bicycle bi in _shell.DataContext.bicycles)
                            {
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    bicycles.Add(bi);
                                });
                            }
                        }
                        else
                        {

                            for (int i = 0; i < _shell.DataContext.bicycles.Count; i++)
                            {
                                if (i >= bicycles.Count)
                                {
                                    Application.Current.Dispatcher.Invoke(() =>
                                    {
                                        bicycles.Add(_shell.DataContext.bicycles[i]);
                                    });

                                }
                                else
                                {
                                    if (!bicycles[i].Equals(_shell.DataContext.bicycles[i]))
                                    {
                                        if (!_shell.DataContext.bicycles.Contains(bicycles[i]))
                                        {
                                            Application.Current.Dispatcher.Invoke(() =>
                                            {
                                                bicycles.RemoveAt(i);
                                            });
                                        }
                                        else
                                        {
                                            Application.Current.Dispatcher.Invoke(() =>
                                            {
                                                bicycles[i] = _shell.DataContext.bicycles[i];
                                            });
                                        }
                                    }

                                }
                            }
                        }
                        Thread.Sleep(_shell.RefreshTimer);
                    }
                });

                Task.Run(() =>
                {
                    while (true)
                    {

                        if (bicyclesLocks.Count > _shell.DataContext.bicyclesLock.Count)
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                bicyclesLocks.Clear();
                            });
                        }

                        if (bicyclesLocks.Count == 0)
                        {
                            foreach (BicyclesLock bi in _shell.DataContext.bicyclesLock)
                            {
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    bicyclesLocks.Add(bi);
                                });
                            }
                        }
                        else
                        {

                            for (int i = 0; i < _shell.DataContext.bicyclesLock.Count; i++)
                            {
                                if (i >= bicyclesLocks.Count)
                                {
                                    Application.Current.Dispatcher.Invoke(() =>
                                    {
                                        bicyclesLocks.Add(_shell.DataContext.bicyclesLock[i]);
                                    });

                                }
                                else
                                {
                                    if (!bicyclesLocks[i].Equals(_shell.DataContext.bicyclesLock[i]))
                                    {
                                        Application.Current.Dispatcher.Invoke(() =>
                                        {
                                            bicyclesLocks[i] = _shell.DataContext.bicyclesLock[i];
                                        });
                                    }
                                }
                            }
                        }
                        Thread.Sleep(_shell.RefreshTimer);
                    }
                });

                Task.Run(() =>
                {
                    while (true)
                    {

                        List<Journal> Data = _shell.DataContext.journals.Where(d => d.JournalDate.Date >= DateFrom.Date && d.JournalDate.Date <= DateTo.Date).ToList();

                        if (journals.Count > Data.Count)
                        {

                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                journals.Clear();
                            });
                        }

                        if (journals.Count == 0)
                        {
                            foreach (Journal bi in Data)
                            {
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    journals.Add(bi);
                                });
                            }
                        }
                        else
                        {

                            for (int i = 0; i < Data.Count; i++)
                            {

                                if (i > journals.Count)
                                {
                                    Application.Current.Dispatcher.Invoke(() =>
                                    {
                                        journals.Add(Data[i]);
                                    });

                                }
                                else
                                {
                                    if (!journals[i].Equals(Data[i]))
                                    {
                                        Application.Current.Dispatcher.Invoke(() =>
                                        {
                                            journals[i] = Data[i];
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
