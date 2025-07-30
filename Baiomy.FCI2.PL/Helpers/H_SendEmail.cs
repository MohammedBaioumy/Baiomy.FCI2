using Baiomy.FCI2.DAL.Entities.Identity;
using System.Net;
using System.Net.Mail;
using System.Reflection.Metadata.Ecma335;

namespace Baiomy.FCI2.PL.Helpers
{
	public class H_SendEmail
	{
        public static Task SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("mhmdmahmod91@gmail.com", "cesx wmgm aqei amtm");

            client.SendAsync("mhmdmahmod91@gmail.com", email.To, email.Subject, email.Body,null);
            return Task.CompletedTask;
        }
    }
}
