using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Enpratik.Core
{
    public class MailHelper
    {
        public bool SendMail(string toAddress, string subject, string body)
        {
            try
            {
                var smtp = new SmtpClient
                {
                    Host = ConfigurationManager.AppSettings["SmtpHost"],
                    Port = ConfigurationManager.AppSettings["SmtpPort"].ToInt32(),
                    EnableSsl = ConfigurationManager.AppSettings["SmtpEnableSsl"].ToBoolean(),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = ConfigurationManager.AppSettings["SmtpUseDefaultCredentials"].ToBoolean(),
                    Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailFromAddress"], ConfigurationManager.AppSettings["MailFromPassword"])
                };
                using (var message = new MailMessage(ConfigurationManager.AppSettings["MailFromAddress"], toAddress)
                {
                    IsBodyHtml = true,
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                    return true;
                }
            }
            catch (Exception ex)
            {
                string aaaa = ex.Message;
                return false;
            }

        }
    }
}
