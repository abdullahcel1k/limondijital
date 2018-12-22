using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimonDijital.Common
{
	// diğer katmanlardan erişilecek class
	public static class App
	{
		public static ICommon Common = new DefaultCommon();
	}
}
