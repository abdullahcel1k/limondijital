using LimonDijital.BusinessLayer;
using LimonDijital.Entities.ValueObjects;
using LimonDijital.WebApp.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LimonDijital.WebApp.Areas.Admin.Controllers
{
	[Exc]
    public class DashboardController : Controller
    {
		private UserManager userManager = new UserManager();
		private SliderManager sliderManager = new SliderManager();
		private PortofiloManager portofiloManager = new PortofiloManager();
		private ReferenceManager referenceManager = new ReferenceManager();
		private QuestionManager questionManager = new QuestionManager();
		private ServiceManager serviceManager = new ServiceManager();
		private SiteInfoManager infoManager = new SiteInfoManager();

		[Auth]
		public ActionResult Index()
        {
			AdminHomePageViewModel models = new AdminHomePageViewModel();
			models.limonPortofilos = portofiloManager.List();
			models.limonQuestions = questionManager.List();
			models.limonReferences = referenceManager.List();
			models.limonServices = serviceManager.List();
			models.limonSiteInfo = infoManager.List().FirstOrDefault();
			models.limonSliders = sliderManager.List();
			models.limonUsers = userManager.List();

			return View(models);
        }

		public ActionResult AccessDenied()
		{
			return View();
		}
	}
}