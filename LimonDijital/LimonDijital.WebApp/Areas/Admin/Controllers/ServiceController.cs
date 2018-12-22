using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LimonDijital.Entities;
using LimonDijital.BusinessLayer;
using LimonDijital.WebApp.Filters;

namespace LimonDijital.WebApp.Areas.Admin.Controllers
{
	[Exc]
	[Auth]
    public class ServiceController : Controller
    {

		private ServiceManager serviceManager = new ServiceManager();

        public ActionResult Index()
        {
            return View(serviceManager.List());
        }
		
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			LimonService limonService = serviceManager.Find(x => x.Id == id);

            if (limonService == null)
            {
                return HttpNotFound();
            }
            return View(limonService);
        }
		
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LimonService limonService)
        {
			ModelState.Remove("ModifiedUsername");
			ModelState.Remove("CreatedOn");
			ModelState.Remove("ModifiedOn");

			if (ModelState.IsValid)
            {
				serviceManager.Insert(limonService);
                return RedirectToAction("Index");
            }

            return View(limonService);
        }
		
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			LimonService limonService = serviceManager.Find(x => x.Id == id);
            if (limonService == null)
            {
                return HttpNotFound();
            }
            return View(limonService);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LimonService service)
        {
			ModelState.Remove("ModifiedUsername");
			ModelState.Remove("CreatedOn");
			ModelState.Remove("ModifiedOn");

			if (ModelState.IsValid)
            {
				LimonService ser = serviceManager.Find(x => x.Id == service.Id);
				ser.Name = service.Name;
				ser.Description = service.Description;

				serviceManager.Update(ser);
				return RedirectToAction("Index");
            }
            return View(service);
        }
		
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			LimonService limonService = serviceManager.Find(x => x.Id == id.Value);
            if (limonService == null)
            {
                return HttpNotFound();
            }
            return View(limonService);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			LimonService service = serviceManager.Find(x => x.Id == id);
			serviceManager.Delete(service);

            return RedirectToAction("Index");
        }
    }
}
