using DataAccessLayer.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisenessLayer
{
    public class BackGroundServicesAsycn : BackgroundService
    {
        private readonly IBackGroundService<BgMailModel> _backGroundService;
        private readonly IIsContunieMailService _ısContunieMailService;
        private readonly IBuiseness _codes;
        public BackGroundServicesAsycn(IBackGroundService<BgMailModel> backGroundService , IBuiseness codes , IIsContunieMailService ısContunieMailService)
        {
            _backGroundService = backGroundService;
            _ısContunieMailService = ısContunieMailService;
            _codes = codes; 
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {     
            var item = await _ısContunieMailService.IsContinueRead(stoppingToken);
            bool a = Convert.ToBoolean(item);
            while (a)
            {
                var item2 = await _ısContunieMailService.IsContinueRead(stoppingToken);
                a = Convert.ToBoolean(item2);
                // var mail = await _backGroundService.SenMail(stoppingToken);
                // await _codes.BgMailService(mail);
            }
            ExecuteAsync(stoppingToken);
        }
    }
}
