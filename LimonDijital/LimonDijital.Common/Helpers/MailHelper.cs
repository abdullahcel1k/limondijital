using Limilabs.Client.POP3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LimonDijital.Common.Helpers
{
	public class MailHelper
	{
		public static bool SendMail(string body, string to, string subject, bool isHtml = true)
		{
			// tek adresi listeye dönüştürüp aşağıdaki metot üzerinden mail gönderme işlemini tamamlıyoruz
			return SendMail(body, new List<string> { to }, subject, isHtml);
		}

		// toplu mail gönderme
		public static bool SendMail(string body, List<string> to, string subject, bool isHtml = true)
		{
			bool result = false;

			try
			{
				var message = new MailMessage();
				message.From = new MailAddress(ConfigHelper.Get<string>("MailUser"));

				// listedeki mail adreslerini sıra ile ekliyoruz
				to.ForEach(x =>
				{
					message.To.Add(new MailAddress(x));
				});

				message.Subject = subject;
				message.Body = body;
				message.IsBodyHtml = isHtml;

				using (var smtp = new SmtpClient(ConfigHelper.Get<string>("MailHost"), ConfigHelper.Get<int>("MailPort")))
				{
					smtp.EnableSsl = true;
					smtp.Credentials = new NetworkCredential(ConfigHelper.Get<string>("MailUser"),
						ConfigHelper.Get<string>("MailPass"));
					smtp.Send(message);
					result = true;
				}
			}
			catch (Exception ex)
			{
				return false;
			}

			return result;
		}
	}
}
