﻿using System.Collections.Generic;

namespace LimonDijital.WebApp.ViewModels
{
	public class NotifyViewModelBase<T>
	{
		public List<T> Items { get; set; }
		public string Header { get; set; }
		public string Title { get; set; }
		public bool IsRedirecitng { get; set; }
		public string RedirectingUrl { get; set; }
		public int RedirectingTimeout { get; set; }


		public NotifyViewModelBase()
		{
			Header = "Yönlendiriliyorsunuz!";
			Title = "Geçersiz İşlem";
			IsRedirecitng = true;
			RedirectingUrl = "/Home/Index";
			RedirectingTimeout = 10000;
			Items = new List<T>();
		}
	}
}