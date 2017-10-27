using System;
using System.Collections.Generic;
using System.Linq;
using Structure.Interface;
using System.Threading.Tasks;
using System.Net.Mail;
using Structure.Utils;
using Xamarin.Forms;
using Structure.iOS.Implementation;

[assembly: Dependency(typeof(ForgotPasswordEmail))]
namespace Structure.iOS.Implementation
{
    public class ForgotPasswordEmail : IForgotPasswordEmail
    {
        public async Task<bool> SendForgotPassword()
        {
            bool success;

            try
            {
                //TODO adding personal email add for testing
                MailMessage mail = new MailMessage(Constants.EmailAddress, "ksoman@astek.mu", "Forgot password", "This is an email");

                SmtpClient smtpclient = new SmtpClient("ssl0.ovh.net")
                {
                    Credentials = new System.Net.NetworkCredential(Constants.EmailAddress, Constants.EmailPassword)
                };

                await smtpclient.SendMailAsync(mail);

                success = true;
            }
            catch (Exception e)
            {
                success = false;
            }

            return success;
        }
    }
}