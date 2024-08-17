using ClientSamokat.HttpContext;
using ClientSamokat.Model;
using System.Net.Http.Headers;
using System.Security.Policy;

namespace ClientSamokat.LongPolling
{
    public class DataPoller
    {
        private readonly GetService _service;
        private readonly IQueryBuilder _queryBuilder;
        private DataContext DbContext { get; set; }
        private CancellationTokenSource cancellationTokenSource { get; set; } = new CancellationTokenSource();  

        public DataPoller(DataContext _context, IQueryBuilder queryBuilder)
        {
            DbContext = _context;
            _queryBuilder = queryBuilder;
            _service = new GetService();    

        }

        public void OpenConnection(int Occ_Id, TimeSpan RefreshTimer)
        {
            StartPolling(cancellationTokenSource, RefreshTimer, Occ_Id);
        }

        public void StartPolling(CancellationTokenSource cts, TimeSpan RequesTime, int Occ_id)
        {
            // Bicycle Background Load
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (cts.IsCancellationRequested)
                    {
                        break;
                    }
                    else
                    {
                        try
                        {
                            lock (_service)
                            {
                                DbContext.bicycles = (_service.Get<List<Bicycle>>(http_clt =>
                                {
                                    http_clt?.DefaultRequestHeaders.Accept.Clear();
                                    http_clt?.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                    http_clt.BaseAddress = new Uri(_queryBuilder.GetRESTString(Occ_id.ToString(), "BicycleModel_Conn"));

                                }).Result);
                            }
                            Thread.Sleep(RequesTime);

                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        
                    }
                }
            });
            // Bicycle  Locks Background Load
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (cts.IsCancellationRequested)
                    { break; }
                    else
                    {
                        try
                        {
                            lock (_service)
                            {
                                DbContext.bicyclesLock = (_service.Get<List<BicyclesLock>>(http_clt =>
                                {
                                    http_clt?.DefaultRequestHeaders.Accept.Clear();
                                    http_clt?.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                    http_clt.BaseAddress = new Uri(_queryBuilder.GetRESTString(Occ_id.ToString(), "BicycleLocks_Conn"));

                                }).Result);
                            }
                            Thread.Sleep(RequesTime);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            });
            // Journal Background Load
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (cts.IsCancellationRequested)
                    { break; }
                    else
                    {
                        try
                        {
                            lock (_service)
                            {
                                DbContext.journals = (_service.Get<List<Journal>>(http_clt =>
                                {
                                    http_clt?.DefaultRequestHeaders.Accept.Clear();
                                    http_clt?.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                    http_clt.BaseAddress = new Uri(_queryBuilder.GetRESTString(Occ_id.ToString(), "Journal_Conn"));

                                }).Result);
                            }
                            Thread.Sleep(RequesTime);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            });
            // Courier Background Load
            Task.Factory.StartNew(() => {
                while (true)
                {
                    if (cts.IsCancellationRequested)
                    { break; }
                    else
                    {
                        try
                        {
                            lock (_service)
                            {
                                DbContext.couriers = (_service.Get<List<Courier>>(http_clt =>
                                {
                                    http_clt?.DefaultRequestHeaders.Accept.Clear();
                                    http_clt?.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                    http_clt.BaseAddress = new Uri(_queryBuilder.GetQueryString(null, "Courier_Conn"));

                                }).Result);
                            }
                            Thread.Sleep(RequesTime);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            });

        }

        public void StopPolling()
        {
            this.cancellationTokenSource.Cancel();  
        }

    }
    
}
