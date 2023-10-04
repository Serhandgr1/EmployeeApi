using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;
        private readonly IBuiseness _codes;
        public BackGroundServicesAsycn(IBackGroundService<BgMailModel> backGroundService , IBuiseness codes , IIsContunieMailService ısContunieMailService , IConfiguration configuration)
        {
            _configuration = configuration;
            _backGroundService = backGroundService;
            _ısContunieMailService = ısContunieMailService;
            _codes = codes; 
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
                var model = await _ısContunieMailService.IsContinueRead(stoppingToken);
                var db = new DataContext(_configuration);
                var backGroundServiceController = db.BackGroundServiceController.Find(1);
                backGroundServiceController.IsWork = model.IsWork;
                var newData = backGroundServiceController;
                db.BackGroundServiceController.Attach(backGroundServiceController);
                EntityEntry entry = db.Entry(backGroundServiceController);
                foreach (var data in new string[] { "IsWork" })
                {
                    var change = entry.CurrentValues[data];
                    var change1 = entry.OriginalValues[data];
                    var work1= Convert.ToBoolean(change1);
                    var work = Convert.ToBoolean(change);
                    if (work1 != work)
                    {
                        //BackGroundServiceControllerModel model1 = new BackGroundServiceControllerModel();
                        //model1.IsWork=work;
                        db.BackGroundServiceController.Update(newData);
                        db.SaveChanges();
                        await _ısContunieMailService.IsWorkingAdd(work);
                        // await _ısContunieMailService.IsContinueAdd(model1);
                    }
                ExecuteAsync(stoppingToken);
                // var mail = await _backGroundService.SenMail(stoppingToken);
                // await _codes.BgMailService(mail);
            }
        }
    }
}
