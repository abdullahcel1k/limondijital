using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimonDijital.Common.Helpers
{
	public class ConfigHelper
	{
		// web configdeki appsetting altındaki eklediğimiz verilerden hangisini istiyorsak anahtar kelime olarak 
		// gönderip onun tipinde değeri geri dönderiyoruz int, double vs
		public static T Get<T>(string key)
		{
			return (T)Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(T));
		}
	}
}
