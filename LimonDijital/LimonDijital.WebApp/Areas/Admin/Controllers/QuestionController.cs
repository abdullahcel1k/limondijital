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
using LimonDijital.WebApp.Filters;

namespace LimonDijital.WebApp.Areas.Admin.Controllers
{
	[Exc]
	[Auth]
    public class QuestionController : Controller
    {
		private QuestionManager questionManager = new QuestionManager();
        public ActionResult Index()
        {
            return View(questionManager.List());
        }
		
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			LimonQuestion limonQuestion = questionManager.Find(x => x.Id == id.Value);
            if (limonQuestion == null)
            {
                return HttpNotFound();
            }
            return View(limonQuestion);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LimonQuestion question)
        {
			ModelState.Remove("ModifiedUsername");
			ModelState.Remove("CreatedOn");
			ModelState.Remove("ModifiedOn");

			if (ModelState.IsValid)
            {
				questionManager.Insert(question);

                return RedirectToAction("Index");
            }

            return View(question);
        }

        // GET: Admin/Question/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			LimonQuestion question = questionManager.Find(x => x.Id == id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }
		
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LimonQuestion question)
        {
			ModelState.Remove("ModifiedUsername");
			ModelState.Remove("CreatedOn");
			ModelState.Remove("ModifiedOn");

			// düzenlenen soruyu bulup verileri güncelliyoruz
			if (ModelState.IsValid)
            {
				LimonQuestion ques = questionManager.Find(x => x.Id == question.Id);
				ques.Title = question.Title;
				ques.Text = question.Text;
				questionManager.Update(ques);

                return RedirectToAction("Index");
            }
            return View(question);
        }

        // GET: Admin/Question/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			LimonQuestion limonQuestion = questionManager.Find(x => x.Id == id.Value);
            if (limonQuestion == null)
            {
                return HttpNotFound();
            }
            return View(limonQuestion);
        }
		
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			LimonQuestion question = questionManager.Find(x => x.Id == id);
			questionManager.Delete(question);

            return RedirectToAction("Index");
        }
    }
}
