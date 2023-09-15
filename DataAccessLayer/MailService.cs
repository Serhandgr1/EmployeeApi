using System;
using System.Net;
using System.Net.Mail;

namespace EmailService
{
    public class MailSender
    {
        private static string senderEmail = "mail göndercek hesap";
        private static string senderPassword = "hesap şifresi";
        //private static string senderEmail = "baj.mezir@gmail.com";
        //private static string senderPassword = "xyja doun coip qwai";
        public async void SendMail(string receiver, string subject, string message)
        {
            using (var client = new SmtpClient("smtp.gmail.com"))
            {
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);

                var mail =  new MailMessage(senderEmail, receiver, subject, message);

                try
                {
                    client.Send(mail);
                }
                catch (Exception ex)
                {
                    
                }
            }
        }
    }
}