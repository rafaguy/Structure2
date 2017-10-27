using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;

using Structure.Interface;
using System.Net.Mail;
using Structure.Utils;
using Xamarin.Forms;
using Structure.Droid.Implementation;
using System.Net;

[assembly: Dependency(typeof(ForgotPasswordEmail))]

namespace Structure.Droid.Implementation
{
   public class ForgotPasswordEmail : IForgotPasswordEmail
    {
        public async Task<bool> SendForgotPassword()
        {


            /*
            try
            {
                //TODO adding personal email add for testing
                MailMessage mail = new MailMessage(Constants.EmailAddress, "ksoman@astek.mu", "Forgot password", "This is an email");

                SmtpClient smtpclient = new SmtpClient("46.105.132.129")
                {
                    Credentials = new System.Net.NetworkCredential(Constants.EmailAddress, Constants.EmailPassword)
                };
               
               await smtpclient.SendMailAsync(mail);

                return true;
            }
            catch (Exception e)
            {
               return false;
            }

          */

            try
            {
                //TODO adding personal email add for testing
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(Constants.EmailAddress);
                mail.To.Add(new MailAddress("ksoman@astek.mu"));
                mail.Body="Forgot Password";
                mail.Subject="Forgot password Body";

                SmtpClient client = new SmtpClient();
                client.Host = "46.105.132.129";
                client.Credentials = new NetworkCredential(Constants.EmailAddress, Constants.EmailPassword);
                await client.SendMailAsync(mail);
    
                  
               /*  
                SmtpClient smtpclient = new SmtpClient("ssl0.ovh.net")
                {
                    Credentials = new System.Net.NetworkCredential("erreur@cygest.fr", "erreur2017")
                };
                await smtpclient.SendMailAsync(mail);
                */
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }
    }
}