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
using LimonDijital.BusinessLayer.Results;
using LimonDijital.WebApp.ViewModels;
using LimonDijital.WebApp.Models;
using LimonDijital.Entities.Messages;
using System.Web.Helpers;
using LimonDijital.WebApp.Filters;

namespace LimonDijital.WebApp.Areas.Admin.Controllers
{
	[Exc]
	[Auth]
    public class PortofiloController : Controller
    {
		private PortofiloManager portofiloManager = new PortofiloManager();
		private ServiceManager serviceManager = new ServiceManager();

        public ActionResult Index()
        {
            return View(portofiloManager.List());
        }

        // GET: Admin/Portofilo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			LimonPortofilo limonPortofilo = portofiloManager.Find(x => x.Id == id.Value);
            if (limonPortofilo == null)
            {
                return HttpNotFound();
            }
            return View(limonPortofilo);
        }

        // GET: Admin/Portofilo/Create
        public ActionResult Create()
        {
			ViewBag.LimonServiceId = new SelectList(serviceManager.List(), "Id", "Name");

			return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LimonPortofilo portofilo, HttpPostedFileBase PortofiloImage)
        {
			ModelState.Remove("ModifiedUsername");
			ModelState.Remove("CreatedOn");
			ModelState.Remove("ModifiedOn");
            
			if (ModelState.IsValid)
            {
				if (PortofiloImage != null &&
				(PortofiloImage.ContentType == "image/jpeg" ||
				 PortofiloImage.ContentType == "image/jpg" ||
				 PortofiloImage.ContentType == "image/png"))
				{
                    Random rand = new Random();
                    int rundNumb = rand.Next(20000, 30000);
                    string filename = $"/Portofilo/portofilo_{rundNumb}.{ PortofiloImage.ContentType.Split('/')[1]}";
					// split ile / karakterini silip ikinci kelimeyi yani jpg png vs yi alıyoruz

					WebImage img = new WebImage(PortofiloImage.InputStream);
					img.Resize(800, 600, false);
					img.Save(Server.MapPath($"~/images{filename}"));
					//PortofiloImage.SaveAs(Server.MapPath($"~/images{filename}"));
					portofilo.PortofiloImageFilaname = filename;
				}

				BusinessLayerResult<LimonPortofilo> res = portofiloManager.Insert(portofilo);

				if (res.Errors.Count > 0)
				{
					ErrorViewModel errorNotifyObj = new ErrorViewModel()
					{
						Title = "Portföy Eklenemedi.",
						Items = res.Errors,
						RedirectingUrl = "/Admin/Portofilo/Create",
						RedirectingTimeout = 3000
					};

					return View("Error", errorNotifyObj);
				}
				

				return RedirectToAction("Index");
			}
            
			return View(portofilo);
        }
		
        public ActionResult Edit(int? id)
        {

			if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			LimonPortofilo portofilo = portofiloManager.Find(x => x.Id == id.Value);
            

			if (portofilo == null)
            {
                return HttpNotFound();
            }
            return View(portofilo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LimonPortofilo portofilo, HttpPostedFileBase PortofiloImage)
        {
			ModelState.Remove("ModifiedUsername");
            
			if (ModelState.IsValid)
			{
				if (PortofiloImage != null &&
				(PortofiloImage.ContentType == "image/jpeg" ||
				 PortofiloImage.ContentType == "image/jpg" ||
				 PortofiloImage.ContentType == "image/png"))
				{
					string filename = $"/Portofilo/portofilo_{portofilo.Id}.{ PortofiloImage.ContentType.Split('/')[1]}";
					// split ile / karakterini silip ikinci kelimeyi yani jpg png vs yi alıyoruz

					WebImage img = new WebImage(PortofiloImage.InputStream);
					img.Resize(800, 600, false);
					img.Save(Server.MapPath($"~/images{filename}"));

					//PortofiloImage.SaveAs(Server.MapPath($"~/images{filename}"));
					portofilo.PortofiloImageFilaname = filename;
				}

				BusinessLayerResult<LimonPortofilo> res = portofiloManager.Update(portofilo);

				if (res.Errors.Count > 0)
				{
					ErrorViewModel errorNotifyObj = new ErrorViewModel()
					{
						Title = "Portföy Güncellenemedi.",
						Items = res.Errors,
						RedirectingUrl = "/Admin/Portofilo/Edit/" + portofilo.Id,
						RedirectingTimeout = 3000
					};

					return View("Error", errorNotifyObj);
				}


				return RedirectToAction("Index");
			}

			return View(portofilo);
        }

        // GET: Admin/Portofilo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			LimonPortofilo portofilo = portofiloManager.Find(x => x.Id == id.Value);
            if (portofilo == null)
            {
                return HttpNotFound();
            }
            return View(portofilo);
        }

        // POST: Admin/Portofilo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			LimonPortofilo portofilo = portofiloManager.Find(x => x.Id == id);
			string fullPath = Request.MapPath("~/images" + portofilo.PortofiloImageFilaname);

			if (System.IO.File.Exists(fullPath))
			{
				System.IO.File.Delete(fullPath);
				
			}

			portofiloManager.Delete(portofilo);
			return RedirectToAction("Index");
        }
    }
}
