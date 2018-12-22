using LimonDijital.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimonDijital.BusinessLayer.Results
{
	public class BusinessLayerResult<T> where T : class
	{
		// birden fazla hata mesajı gönderebilmek için ürettiğimiz geneeric sınıf

		public List<ErrorMessageObj> Errors { get; set; }
		public T Result { get; set; }

		public BusinessLayerResult()
		{
			Errors = new List<ErrorMessageObj>();
		}


		public void AddError(ErrorMessageCode code, string message)
		{
			Errors.Add(new ErrorMessageObj() { Code = code, Message = message });
		}
	}
}
