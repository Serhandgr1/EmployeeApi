using DataAccessLayer.Models;
using EmailService;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace BuisenessLayer
{
    public class MailSendBg : IBackGroundService<BgMailModel> , IIsContunieMailService
    {
        private readonly Channel<BgMailModel> channel;
        private readonly Channel<BackGroundServiceControllerModel> IsContinue;
        private readonly Channel<bool> IsWork;
        public bool Stop;

        public MailSendBg(IConfiguration configuration)
        {
            int.TryParse(configuration["Capacity"], out int capacity);
            BoundedChannelOptions options = new(capacity)
            {
                    FullMode = BoundedChannelFullMode.Wait
            };
            channel = Channel.CreateBounded<BgMailModel>(options);
            IsContinue = Channel.CreateBounded<BackGroundServiceControllerModel>(options);
            IsWork = Channel.CreateBounded<bool>(options);
        }
        public async ValueTask AddMail(BgMailModel mailsender)
        {
            ArgumentNullException.ThrowIfNull(mailsender);
            await channel.Writer.WriteAsync(mailsender);
        }
        public async ValueTask IsContinueAdd(BackGroundServiceControllerModel item) 
        {
            ArgumentNullException.ThrowIfNull(item);
            await IsContinue.Writer.WriteAsync(item);
        }

        public ValueTask<BgMailModel> SenMail(CancellationToken cancellationToken)
        {
           var mail = channel.Reader.ReadAsync(cancellationToken);
            return mail;
            // buisennes coda bir metod yaz datarepostorye bir metod yaz veri tabanının mail gönderilenler kısmına ekle maili 
        }
        public ValueTask<BackGroundServiceControllerModel> IsContinueRead(CancellationToken cancellationToken)
        {
            var item = IsContinue.Reader.ReadAsync(cancellationToken);
            return item;
            // buisennes coda bir metod yaz datarepostorye bir metod yaz veri tabanının mail gönderilenler kısmına ekle maili 
        }
        public async  ValueTask IsWorkingAdd(bool item) 
        {
            ArgumentNullException.ThrowIfNull(item);
            await IsWork.Writer.WriteAsync(item);
            Stop = item;
        }
        public ValueTask<bool> IsWorkingRead(CancellationToken cancellationToken)
        {
            var wrok = IsWork.Reader.ReadAsync(cancellationToken);
            return wrok;
        }
        public bool IsWorkingStop()
        {
            return Stop;
        }
    }
}
