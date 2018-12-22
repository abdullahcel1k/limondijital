using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LimonDijital.WebApp.Filters
{
	public class Exc : FilterAttribute, IExceptionFilter
	{
		public void OnException(ExceptionContext filterContext)
		{
			//filterContext.Exception.StackTrace() gibi hatayı alıp loglayabiliriz
			// throw new Exception("herhangi bir hata oluştu."); böyle bir kullanımla kendi hata mesajımızı
			// verebilir daha anlamlı ve kullanıcı dostu hata sayfalaıar oluşturabiliriz

			filterContext.Controller.TempData["LastError"] = filterContext.Exception;

			filterContext.ExceptionHandled = true;
			filterContext.Result = new RedirectResult("/Home/HasError");
		}
	}
}