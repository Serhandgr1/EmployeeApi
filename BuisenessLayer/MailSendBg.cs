using DataAccessLayer.Models;
using EmailService;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace BuisenessLayer
{
    public class MailSendBg : IBackGroundService<BgMailModel> , IIsContunieMailService
    {
        private readonly Channel<BgMailModel> channel;
        private readonly Channel<bool> IsContinue;
        public MailSendBg(IConfiguration configuration)
        {
            int.TryParse(configuration["Capacity"], out int capacity);
            BoundedChannelOptions options = new(capacity)
            {
                    FullMode = BoundedChannelFullMode.Wait
            };
            channel = Channel.CreateBounded<BgMailModel>(options);
            IsContinue = Channel.CreateBounded<bool>(options);
        }
        public async ValueTask AddMail(BgMailModel mailsender)
        {
            ArgumentNullException.ThrowIfNull(mailsender);
            await channel.Writer.WriteAsync(mailsender);
        }
        public async ValueTask IsContinueAdd(bool item) 
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
        public ValueTask<bool> IsContinueRead(CancellationToken cancellationToken)
        {
            var item = IsContinue.Reader.ReadAsync(cancellationToken);
            return item;
            // buisennes coda bir metod yaz datarepostorye bir metod yaz veri tabanının mail gönderilenler kısmına ekle maili 
        }
    }
}
