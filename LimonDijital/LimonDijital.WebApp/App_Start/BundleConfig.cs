using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace LimonDijital.WebApp.App_Start
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			// css için
			bundles.Add(new StyleBundle("~/css/webcss")
				.Include("~/Content/animate.css",
				"~/Content/icomoon.css",
				"~/Content/themify-icons.css",
				"~/Content/bootstrap.css",
				"~/Content/magnific-popup.css",
				"~/Content/owl.carousel.min.css",
				"~/Content/owl.theme.default.min.css",
				"~/Content/flexslider.css",
				"~/Content/style.css",
				"~/Content/alertify.min.css"));

			bundles.Add(new StyleBundle("~/css/admincss")
				.Include("~/Content/bootstrap.min.css",
				"~/Content/sb-admin.css",
				"~/font-awesome/css/font-awesome.min.css",
				"~/Content/alertify.min.css"));

			// js için
			bundles.Add(new ScriptBundle("~/js/webjs").
				Include("~/Scripts/modernizr-2.6.2.js",
				"~/Scripts/jquery.min.js",
				"~/Scripts/jquery.easing.1.3.js",
				"~/Scripts/bootstrap.min.js",
				"~/Scripts/jquery.waypoints.min.js",
				"~/Scripts/owl.carousel.min.js",
				"~/Scripts/jquery.countTo.js",
				"~/Scripts/jquery.flexslider-min.js",
				"~/Scripts/jquery.magnific-popup.min.js",
				"~/Scripts/magnific-popup-options.js",
				"~/Scripts/main.js",
				"~/Scripts/alertify.min.js"));

			bundles.Add(new ScriptBundle("~/js/adminjs")
				.Include("~/Scripts/jquery.min.js",
				"~/Scripts/bootstrap.min.js",
				"~/Scripts/alertify.min.js"));

			BundleTable.EnableOptimizations = true;
		}
	}
}