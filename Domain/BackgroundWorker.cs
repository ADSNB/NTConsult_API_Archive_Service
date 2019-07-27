using Domain.Model;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain
{
    public class BackgroundWorker : BackgroundService
    {
        private readonly IBackgroundWorkerDomain _domain;

        #region Database in memory
        private List<ClientModel> _clientList = new List<ClientModel>();
        private List<SalesmanModel> _salesmanList = new List<SalesmanModel>();
        private List<SaleModel> _saleList = new List<SaleModel>();
        #endregion

        public BackgroundWorker(IBackgroundWorkerDomain domain) => _domain = domain;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Log.Information($"Background service is starting.");

            stoppingToken.Register(() =>
                Log.Information($"Background service task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                Log.Information($"Background service proccess initiated.");

                _domain.ReadFile(_clientList, _salesmanList, _saleList);

                await Task.Delay(TimeSpan.FromSeconds(3), stoppingToken);

                Log.Information("Background service process finalized");

                await ExecuteAsync(new CancellationToken());
            }

            Log.Information($"Background service task is stopping.");
        }
    }
}
