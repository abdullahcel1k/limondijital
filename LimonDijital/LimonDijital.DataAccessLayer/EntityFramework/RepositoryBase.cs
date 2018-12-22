using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimonDijital.DataAccessLayer.EntityFramework
{
	//	Singleton patterni
	public class RepositoryBase
	{
		protected static DatabaseContext context;
		private static object _lockSync = new object();

		// sınıfın new lenmemesi için protected bir metot ekliyoruz
		protected RepositoryBase()
		{
			CreateContext();
		}

		// metot newlenemediği içinde static farklı bir metot oluşturduk
		private static void CreateContext()
		{
			if (context == null)
			{
				// multithread uygulamalarda birden fazla thread bu işlemi çalıştırmak isteyebilir
				// bunu önlemek için lock methodu kullanırız bu method obje ister bizden
				lock (_lockSync)
				{
					// işi sağlama alarak bir daha kontrol ettirdik nullmu değilmi diye
					if (context == null)
					{
						context = new DatabaseContext();
					}
				}
			}
		}
	}
}
