using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LimonDijital.Entities;
using LimonDijital.WebApp.Models;
using LimonDijital.BusinessLayer;
using System.Web.Helpers;
using LimonDijital.BusinessLayer.Results;
using LimonDijital.WebApp.ViewModels;
using LimonDijital.Entities.Messages;
using LimonDijital.WebApp.Filters;

namespace LimonDijital.WebApp.Areas.Admin.Controllers
{
	[Exc]
	[Auth]
    public class ReferenceController : Controller
    {
        private ReferenceManager referenceManager = new ReferenceManager();
		
        public ActionResult Index()
        {
            return View(referenceManager.List());
        }
		
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			LimonReference reference = referenceManager.Find(x => x.Id == id.Value);
            if (reference == null)
            {
                return HttpNotFound();
            }
            return View(reference);
        }
		
        public ActionResult Create()
        {
            return View();
        }
		
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LimonReference reference, HttpPostedFileBase ReferenceImage)
        {
			ModelState.Remove("ModifiedUsername");
			ModelState.Remove("CreatedOn");
			ModelState.Remove("ModifiedOn");
			ModelState.Remove("PortofiloImageFilaname");
			
			if (ModelState.IsValid)
			{
				if (ReferenceImage != null &&
				(ReferenceImage.ContentType == "image/jpeg" ||
				 ReferenceImage.ContentType == "image/jpg" ||
				 ReferenceImage.ContentType == "image/png"))
				{
                    Random rand = new Random();
                    int rundNumb = rand.Next(20000, 30000);
					string filename = $"/References/referans_{rundNumb}.{ ReferenceImage.ContentType.Split('/')[1]}";
					// split ile / karakterini silip ikinci kelimeyi yani jpg png vs yi alıyoruz

					WebImage img = new WebImage(ReferenceImage.InputStream);
					img.Resize(800, 600, false);
					img.Save(Server.MapPath($"~/images{filename}"));

					reference.ReferencesImageFilename = filename;
				}

				BusinessLayerResult<LimonReference> res = referenceManager.Insert(reference);

				if (res.Errors.Count > 0)
				{
					ErrorViewModel errorNotifyObj = new ErrorViewModel()
					{
						Title = "Referans Eklenemedi.",
						Items = res.Errors,
						RedirectingUrl = "/Admin/Reference/Create/",
						RedirectingTimeout = 3000
					};

					return View("Error", errorNotifyObj);
				}


				return RedirectToAction("Index");
			}

			return View(reference);
        }
		
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			LimonReference limonReference = referenceManager.Find(x => x.Id == id.Value);
            if (limonReference == null)
            {
                return HttpNotFound();
            }
            return View(limonReference);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LimonReference reference, HttpPostedFileBase ReferenceImage)
		{
			ModelState.Remove("ModifiedUsername");
			
			if (ModelState.IsValid)
			{
				if (ReferenceImage != null &&
				(ReferenceImage.ContentType == "image/jpeg" ||
				 ReferenceImage.ContentType == "image/jpg" ||
				 ReferenceImage.ContentType == "image/png"))
				{
					string filename = $"/References/{reference.Name}_{reference.Id}.{ ReferenceImage.ContentType.Split('/')[1]}";
					// split ile / karakterini silip ikinci kelimeyi yani jpg png vs yi alıyoruz

					WebImage img = new WebImage(ReferenceImage.InputStream);
					img.Resize(800, 600, false);
					img.Save(Server.MapPath($"~/images{filename}"));

					//PortofiloImage.SaveAs(Server.MapPath($"~/images{filename}"));
					reference.ReferencesImageFilename = filename;
				}

				BusinessLayerResult<LimonReference> res = referenceManager.Update(reference);

				if (res.Errors.Count > 0)
				{
					ErrorViewModel errorNotifyObj = new ErrorViewModel()
					{
						Title = "Referans Güncellenemedi.",
						Items = res.Errors,
						RedirectingUrl = "/Admin/Reference/Edit/" + reference.Id,
						RedirectingTimeout = 3000
					};

					return View("Error", errorNotifyObj);
				}


				return RedirectToAction("Index");
			}

			return View(reference);
		}

        // GET: Admin/Reference/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LimonReference reference = referenceManager.Find(x => x.Id == id.Value);
            if (reference == null)
            {
                return HttpNotFound();
            }
            return View(reference);
        }

        // POST: Admin/Reference/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LimonReference reference = referenceManager.Find(x => x.Id == id);
			string fullPath = Request.MapPath("~/images" + reference.ReferencesImageFilename);

			if (System.IO.File.Exists(fullPath))
			{
				System.IO.File.Delete(fullPath);
			}

			referenceManager.Delete(reference);

			return RedirectToAction("Index");
        }
    }
}
