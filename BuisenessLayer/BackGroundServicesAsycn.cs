using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
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
        public BackGroundServicesAsycn(IBackGroundService<BgMailModel> backGroundService, IBuiseness codes, IIsContunieMailService ısContunieMailService, IConfiguration configuration)
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
            BackGroundServiceControllerModel backGroundServiceController = new BackGroundServiceControllerModel();
            BackGroundServiceControllerModel newData = new BackGroundServiceControllerModel();
            bool count = true;
            bool control=true;
            if (model.Id >= 1)
            {
                control= await db.BackGroundServiceController.AnyAsync(x => x.Id == model.Id);
                if (control) 
                {
                    backGroundServiceController = await db.BackGroundServiceController.Where(x => x.Id == model.Id).FirstAsync();
                    backGroundServiceController.Id = model.Id;
                    backGroundServiceController.IsWork = model.IsWork;
                    backGroundServiceController.JobName = model.JobName;
                    backGroundServiceController.JobArgs = model.JobArgs;
                    backGroundServiceController.TryCount = model.TryCount;
                    backGroundServiceController.CreationTime = model.CreationTime;
                    backGroundServiceController.NextTryTime = model.NextTryTime;
                    backGroundServiceController.LastTryTime = model.LastTryTime;
                    backGroundServiceController.IsAbandoned = model.IsAbandoned;
                    backGroundServiceController.Priority = model.Priority;
                    backGroundServiceController.ExtraProperties = model.ExtraProperties;
                    backGroundServiceController.ConcurrencyStamp = model.ConcurrencyStamp;
                    newData = backGroundServiceController;
                }
            }
            if (control) 
            {
                db.BackGroundServiceController.Attach(backGroundServiceController);
                EntityEntry entry = db.Entry(backGroundServiceController);
                foreach (var data in new string[] { "Id", "IsWork", "JobName", "JobArgs", "TryCount", "CreationTime", "NextTryTime", "LastTryTime", "IsAbandoned", "Priority", "ExtraProperties", "ConcurrencyStamp" })
                {
                    string change = entry.CurrentValues[data].ToString();
                    string change1 = entry.OriginalValues[data].ToString();
                    //  var work1= Convert.ToBoolean(change1);
                    //  var work = Convert.ToBoolean(change);
                    bool a = change1 == change ? true : false;
                    if (!a)
                    {

                        switch (backGroundServiceController.Id)
                        {
                            case 1:     // Id'si 1 olan jop güncellenecek 
                                        //   db.BackGroundServiceController.Update(newData);
                                        // db.SaveChanges();
                                        //await _ısContunieMailService.IsWorkingAdd(newData);

                                break;
                            case 2: // Id'si 2 olan jop güncellenecek 
                                    //  db.BackGroundServiceController.Update(newData);
                                    // db.SaveChanges();
                                    //    await _ısContunieMailService.IsWorkingAdd(work);
                                break;
                        }
                        //BackGroundServiceControllerModel model1 = new BackGroundServiceControllerModel();
                        //model1.IsWork=work;
                        //  db.BackGroundServiceController.Update(newData);
                        // db.SaveChanges();
                        //    await _ısContunieMailService.IsWorkingAdd(work);
                        // await _ısContunieMailService.IsContinueAdd(model1);
                        break;
                    }
                    // var mail = await _backGroundService.SenMail(stoppingToken);
                    // await _codes.BgMailService(mail);
                }
            }
            ExecuteAsync(stoppingToken);
        }
    }
}
