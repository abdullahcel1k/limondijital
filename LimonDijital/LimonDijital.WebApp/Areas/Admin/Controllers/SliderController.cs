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
    public class SliderController : Controller
    {
        private SliderManager sliderManager = new SliderManager();
		
        public ActionResult Index()
        {
            return View(sliderManager.List().OrderBy(x => x.QueueNumber));
        }
		
        public ActionResult Create()
        {
            return View();
        }
		
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LimonSlider slider, HttpPostedFileBase SliderImage)
        {
			ModelState.Remove("ModifiedUsername");
			
			if (ModelState.IsValid)
			{
				if (SliderImage != null &&
				(SliderImage.ContentType == "image/jpeg" ||
				 SliderImage.ContentType == "image/jpg" ||
				 SliderImage.ContentType == "image/png"))
				{
                    Random rand = new Random();
                    int rundNumb = rand.Next(20000, 30000);

                    string filename = $"/Slider/slider_{rundNumb}.{ SliderImage.ContentType.Split('/')[1]}";
					// split ile / karakterini silip ikinci kelimeyi yani jpg png vs yi alıyoruz

					WebImage img = new WebImage(SliderImage.InputStream);
					img.Resize(1500, 728, false);
					img.Save(Server.MapPath($"~/images{filename}"));
					//PortofiloImage.SaveAs(Server.MapPath($"~/images{filename}"));
					slider.SliderImageFilename = filename;
				}

				BusinessLayerResult<LimonSlider> res = sliderManager.Insert(slider);

				if (res.Errors.Count > 0)
				{
					ErrorViewModel errorNotifyObj = new ErrorViewModel()
					{
						Title = "Slider Eklenemedi.",
						Items = res.Errors,
						RedirectingUrl = "/Admin/Slider/Create",
						RedirectingTimeout = 3000
					};

					return View("Error", errorNotifyObj);
				}


				return RedirectToAction("Index");
			}

			return View(slider);
		}
		
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			LimonSlider slider = sliderManager.Find(x => x.Id == id.Value);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LimonSlider slider, HttpPostedFileBase SliderImage)
        {
			ModelState.Remove("ModifiedUsername");
			
			if (ModelState.IsValid)
			{
				if (SliderImage != null &&
				(SliderImage.ContentType == "image/jpeg" ||
				 SliderImage.ContentType == "image/jpg" ||
				 SliderImage.ContentType == "image/png"))
				{
					string filename = $"/Slider/slider_{slider.Id}.{ SliderImage.ContentType.Split('/')[1]}";

					WebImage img = new WebImage(SliderImage.InputStream);
					img.Resize(1500, 728, false);
					img.Save(Server.MapPath($"~/images{filename}"));

					//PortofiloImage.SaveAs(Server.MapPath($"~/images{filename}"));
					slider.SliderImageFilename = filename;
				}

				BusinessLayerResult<LimonSlider> res = sliderManager.Update(slider);

				if (res.Errors.Count > 0)
				{
					ErrorViewModel errorNotifyObj = new ErrorViewModel()
					{
						Title = "Slider Güncellenemedi.",
						Items = res.Errors,
						RedirectingUrl = "/Admin/Slider/Edit/" + slider.Id,
						RedirectingTimeout = 3000
					};

					return View("Error", errorNotifyObj);
				}


				return RedirectToAction("Index");
			}

			return View(slider);
		}

        // GET: Admin/Slider/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			LimonSlider limonSlider = sliderManager.Find(x => x.Id == id.Value);
            if (limonSlider == null)
            {
                return HttpNotFound();
            }
            return View(limonSlider);
        }
		
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			LimonSlider portofilo = sliderManager.Find(x => x.Id == id);
			sliderManager.Delete(portofilo);

			return RedirectToAction("Index");
		}
    }
}
