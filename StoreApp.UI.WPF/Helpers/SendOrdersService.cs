using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using StoreApp.DAL;

namespace StoreApp.UI.WPF.Helpers
{
    // for send orders to provisioners by post
    public class SendOrdersService
    {
        public async Task SendMailMessage(string toMail, string subject, string body, bool isBodyHtml = false, ICollection<Attachment> attachments = null)
        {
            MailAddress mailFrom = new MailAddress(Settings.MailFrom, Settings.SenderName);
            MailAddress mailTo = new MailAddress(toMail);

            MailMessage mailMessage = new MailMessage(mailFrom, mailTo)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = isBodyHtml,
            };

            if (attachments != null)
            {
                foreach (var attachment in attachments)
                {
                    mailMessage.Attachments.Add(attachment);
                }
            }

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(Settings.MailFrom, Settings.MailPassword),
            };
            await smtpClient.SendMailAsync(mailMessage);
        }


        internal Attachment[] GetAttachments(string[] selectedFiles)
        {
            if (selectedFiles is null)
            {
                return null;
            }
            else
            {
                List<Attachment> attachments = new List<Attachment>();
                foreach (var file in selectedFiles)
                {
                    attachments.Add(new Attachment(file));
                }
                return attachments.ToArray();
            }
        }
    }
}
