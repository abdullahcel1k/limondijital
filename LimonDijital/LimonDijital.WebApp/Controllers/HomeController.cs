using LimonDijital.BusinessLayer;
using LimonDijital.Common.Helpers;
using LimonDijital.Entities.ValueObjects;
using LimonDijital.WebApp.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LimonDijital.WebApp.Controllers
{
	[Exc]
    public class HomeController : Controller
    {
		private UserManager userManager = new UserManager();
		private SliderManager sliderManager = new SliderManager();
		private PortofiloManager portofiloManager = new PortofiloManager();
		private ReferenceManager referenceManager = new ReferenceManager();
		private QuestionManager questionManager = new QuestionManager();
		private ServiceManager serviceManager = new ServiceManager();
		private SiteInfoManager infoManager = new SiteInfoManager();

		public ActionResult Index()
        {
			Test test = new Test();
			HomePageViewModel models = new HomePageViewModel();
			models.limonPortofilos = portofiloManager.List();
			models.limonQuestions = questionManager.List();
			models.limonReferences = referenceManager.List();
			models.limonServices = serviceManager.List();
			models.limonSiteInfo = infoManager.List().FirstOrDefault();
			models.limonSliders = sliderManager.List();


            return View(models);
        }

		[HttpPost]
		public ActionResult Contact(string name, string email, string phone, string mesaj)
		{
			if(!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(phone) && !string.IsNullOrEmpty(mesaj))
			{
				string body = $"Websitesi iletişim formundan gelen iletişim talebi, kullanıcı detayları ve mesaj aşağıdadır.<br/>" +
					$"İsim: {name}<br/> E-mail: {email}<br/>Telefon: {phone}<br/><br/> Mesaj İçeriği: {mesaj}";

				if(MailHelper.SendMail(body, "info@limondijital.com", "Limondijital.com İletişim Formundan Gelen Mesaj"))
				{
					return Json(new { hasError = false, errorMessage = string.Empty, name = name });
				}
				else
				{
					return Json(new { hasError = true, errorMessage = "Mesajınız gönderilirken bir hata meydana geldi, lütfen daha sonra deneyiniz." });
				}
			}
			else
			{
				return Json(new { hasError = true, errorMessage = "Lütfen iletişim formunun tamamını doldurunuz.", name = name });
			}
		}

		public ActionResult HasError()
		{
			return View();
		}
    }
}