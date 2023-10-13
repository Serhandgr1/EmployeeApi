using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisenessLayer
{
    public class BackGroundServiceSecond : BackgroundService
    {
        private readonly IIsContunieMailService _ısContunieMailService;
        public BackGroundServiceSecond(IIsContunieMailService ısContunieMailService)
        {
            _ısContunieMailService = ısContunieMailService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var item2 = await _ısContunieMailService.IsWorkingRead(stoppingToken);
            bool  a = Convert.ToBoolean(item2.IsWork);
            while (a) 
            {
                await Task.Delay(10000);
                var b =  _ısContunieMailService.IsWorkingStop();               
                if (b != null) 
                {
                    a = Convert.ToBoolean(b.IsWork);
                }
            }
            ExecuteAsync(stoppingToken);
        }
    }
}
