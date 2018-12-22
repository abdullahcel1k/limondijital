using LimonDijital.Common;
using LimonDijital.Entities;
using LimonDijital.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LimonDijital.WebApp.Init
{
	// common katmanı ile iletişime geçen sınıfımız
	public class WebComon : ICommon
	{
		public string GetCurrentUsername()
		{
			LimonUser user = CurrentSession.User;

			if (user != null)
				return user.Username;
			else
				return "system";
		}
	}
}