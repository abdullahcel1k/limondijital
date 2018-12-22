using LimonDijital.BusinessLayer;
using LimonDijital.BusinessLayer.Results;
using LimonDijital.Entities;
using LimonDijital.Entities.Messages;
using LimonDijital.Entities.ValueObjects;
using LimonDijital.WebApp.Filters;
using LimonDijital.WebApp.Models;
using LimonDijital.WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace LimonDijital.WebApp.Areas.Admin.Controllers
{
	[Exc]
    public class UserController : Controller
    {
		private UserManager userManager = new UserManager();
		private SiteInfoManager siteManager = new SiteInfoManager();

		[AuthAdmin]
		public ActionResult Index()
        {
            return View(userManager.List());
        }

		[AuthAdmin]
		public ActionResult Create()
		{
			return View();
		}

		[AuthAdmin]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(LimonUser user)
		{
			ModelState.Remove("ModifiedUsername");
			ModelState.Remove("CreatedOn");
			ModelState.Remove("ModifiedOn");

			if (ModelState.IsValid)
			{

				BusinessLayerResult<LimonUser> res = userManager.Insert(user);

				if (res.Errors.Count > 0)
				{
					ErrorViewModel errorNotifyObj = new ErrorViewModel()
					{
						Title = "Kullanıcı Eklenemedi.",
						Items = res.Errors,
						RedirectingUrl = "/Admin/User/Create",
						RedirectingTimeout = 3000
					};

					return View("Error", errorNotifyObj);
				}


				return RedirectToAction("Index");
			}

			return View(user);
		}

		[AuthAdmin]
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			LimonUser user = userManager.Find(x => x.Id == id.Value);
			
			if (user == null)
			{
				return HttpNotFound();
			}
			return View(user);
		}

		[AuthAdmin]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(LimonUser user)
		{
			ModelState.Remove("Password");
			ModelState.Remove("ModifiedUsername");
			ModelState.Remove("CreatedOn");
			ModelState.Remove("ModifiedOn");

			if (ModelState.IsValid)
			{
				BusinessLayerResult<LimonUser> res = userManager.Update(user);

				if (res.Errors.Count > 0)
				{
					ErrorViewModel errorNotifyObj = new ErrorViewModel()
					{
						Title = "Kullanıcı Güncellenemedi.",
						Items = res.Errors,
						RedirectingUrl = "/Admin/User/Edit/" + user.Id,
						RedirectingTimeout = 3000
					};

					return View("Error", errorNotifyObj);
				}


				return RedirectToAction("Index");
			}

			return View(user);
		}

		[AuthAdmin]
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			LimonUser user = userManager.Find(x => x.Id == id.Value);
			if (user == null)
			{
				return HttpNotFound();
			}
			return View(user);
		}

		[AuthAdmin]
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			LimonUser user = userManager.Find(x => x.Id == id.Value);
			if (user == null)
			{
				return HttpNotFound();
			}
			return View(user);
		}
		
		[AuthAdmin]
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			LimonUser user = userManager.Find(x => x.Id == id);
			string fullPath = Request.MapPath("~/images" + user.ProfileImageFilename);

			if(user.ProfileImageFilename.ToString() == "/User/user_boy.png")
			{
				userManager.Delete(user);
			}
			else
			{
				if (System.IO.File.Exists(fullPath))
				{
					System.IO.File.Delete(fullPath);
					userManager.Delete(user);
				}
				else
				{
					List<ErrorMessageObj> obj = new List<ErrorMessageObj>();
					obj.Add(new ErrorMessageObj
					{
						Code = ErrorMessageCode.PortofilNotDeleted,
						Message = "Silinmek istenen portföy daha önce silinmiş"
					});

					ErrorViewModel errorNotifyObj = new ErrorViewModel()
					{
						Title = "Portföy Silinemedi.",
						Items = obj,
						RedirectingUrl = "/Admin/Portofilo",
						RedirectingTimeout = 3000
					};

					return View("Error", errorNotifyObj);
				}
			}
			

			return RedirectToAction("Index");
		}

		[Auth]
		public ActionResult ShowProfile()
		{
			BusinessLayerResult<LimonUser> res = userManager.GetUserById(CurrentSession.User.Id);

			if (res.Errors.Count > 0)
			{
				ErrorViewModel errorNotifyObj = new ErrorViewModel()
				{
					Title = "Hata Oluştu",
					Items = res.Errors
				};

				return View("Error", errorNotifyObj);
			}

			return View(res.Result);
		}

		[Auth]
		public ActionResult EditProfile()
		{
			BusinessLayerResult<LimonUser> res = userManager.GetUserById(CurrentSession.User.Id);
			if (res.Errors.Count > 0)
			{
				ErrorViewModel errorNotifyObj = new ErrorViewModel()
				{
					Title = "Hata Oluştu",
					Items = res.Errors
				};

				return View("Error", errorNotifyObj);
			}

			return View(res.Result);
		}

		[Auth]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditProfile(LimonUser user, HttpPostedFileBase ProfileImage)
		{
			ModelState.Remove("ModifiedUsername"); // editprofile sayfamızda bu alanı zorunlu istemediğimiz
			// ve bu alanı miras alınına evernoteuser classından otomatik geldiği için modalstate den kontrolünü kaldırdık

			if (ModelState.IsValid)
			{
				if (ProfileImage != null &&
				(ProfileImage.ContentType == "image/jpeg" ||
				 ProfileImage.ContentType == "image/jpg" ||
				 ProfileImage.ContentType == "image/png"))
				{
					string filename = $"/User/{user.Username}.{ ProfileImage.ContentType.Split('/')[1]}";
					// split ile / karakterini silip ikinci kelimeyi yani jpg png vs yi alıyoruz

					WebImage img = new WebImage(ProfileImage.InputStream);
					img.Resize(800, 600, false);
					img.Save(Server.MapPath($"~/images{filename}"));

					user.ProfileImageFilename = filename;
				}

				BusinessLayerResult<LimonUser> res = userManager.UpdateProfile(user);

				if (res.Errors.Count > 0)
				{
					ErrorViewModel errorNotifyObj = new ErrorViewModel()
					{
						Title = "Profil Güncellenemedi.",
						Items = res.Errors,
						RedirectingUrl = "/Admin/User/EditProfile",
						RedirectingTimeout = 3000
					};

					return View("Error", errorNotifyObj);
				}

				// current sessionda set metodumuza hangi anahtar kelimesi ve gelecek değer ise veriyoruz bu şekilde set etmiş oluyoruz
				CurrentSession.Set<LimonUser>("login", res.Result); // profildeki değişiklikleri sessionada kaydediyoruz

				return RedirectToAction("ShowProfile");
			}

			return View(user);
		}

		[Auth]
		public ActionResult DeleteProfile()
		{
			BusinessLayerResult<UserManager> res = userManager.DeleteUserById(CurrentSession.User.Id);

			if (res.Errors.Count > 0)
			{
				ErrorViewModel errorNotifyObj = new ErrorViewModel()
				{
					Items = res.Errors,
					Title = "Profil Silinemedi.",
					RedirectingUrl = "/Admin/User/ShowProfile"
				};

				return View("Error", errorNotifyObj);
			}

			Session.Clear();

			return RedirectToAction("Login");
		}

		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(LoginViewModel model)
		{

			if (ModelState.IsValid)
			{
				BusinessLayerResult<LimonUser> res = userManager.LoginUser(model);

				if (res.Errors.Count > 0)
				{
					res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));

					if (res.Errors.Find(x => x.Code == ErrorMessageCode.UserAlreadyActive) != null)
					{
						// admini aktif etmesi gereken bi kullanıcı bu na uygun bir sayfa
						// gösterilebilir adminden aktif edilmesi istenilmeli
					}
					return View(model);
				}
				CurrentSession.Set<LimonUser>("login", res.Result);
				return RedirectToAction("Index","Dashboard");
			}

			return View(model);
		}

		[Auth]
		public ActionResult Logout()
		{
			Session.Clear();

			return RedirectToAction("Login");
		}

		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{

				BusinessLayerResult<LimonUser> res = userManager.RegisterUser(model);

				if (res.Errors.Count > 0)
				{
					res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
					return View(model);
				}

				OkViewModel notifyObj = new OkViewModel()
				{
					Title = "Kayıt Başarılı",
					RedirectingUrl = "/Admin/User/Login"
				};
				notifyObj.Items.Add("Lütfen site yöneticinizden hesabınızı aktif etmesini isteyiniz.");

				return View("Ok", notifyObj);
			}


			return View(model);
		}

		[AuthAdmin]
		public ActionResult SiteSettings()
		{
			return View(siteManager.List().FirstOrDefault());
		}

		[AuthAdmin]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult SiteSettings(LimonSiteInfo siteInfo)
		{
			ModelState.Remove("ModifiedUsername");
            ModelState.Remove("CustomerCount");
            ModelState.Remove("ProjectCount");



            if (ModelState.IsValid)
			{
				LimonSiteInfo site = siteManager.Find(x => x.Id == siteInfo.Id);

				site.Address = siteInfo.Address;
				site.FacebookProfile = siteInfo.FacebookProfile;
				site.LinkedinProfile = siteInfo.LinkedinProfile;
				site.Mail1 = siteInfo.Mail1;
				site.Mail2 = siteInfo.Mail2;
				site.MapSrc = siteInfo.MapSrc;
				site.Phone1 = siteInfo.Phone1;
				site.Phone2 = siteInfo.Phone2;
				site.SiteKeywords = siteInfo.SiteKeywords;
				site.SiteName = siteInfo.SiteName;
				site.SiteTitle = siteInfo.SiteTitle;
				site.TwitterProfile = siteInfo.TwitterProfile;

				if (siteManager.Update(site) == 0)
				{
					ErrorViewModel errorNotifyObj = new ErrorViewModel()
					{
						Title = "Site ayarları düzenlenemedi.",
						RedirectingUrl = "/Admin/User/SiteSettings"
					};
					return View("Error", errorNotifyObj);
				}
				else
				{
					return View(siteManager.List().FirstOrDefault());

				}
			}

			return View(siteInfo);
		}
	}
}