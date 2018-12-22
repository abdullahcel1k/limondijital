using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimonDijital.Entities
{
	/* Kullanıcıların ürün isterken sorabileceği soruları websitesi 
		üzerinden cevaplandıracağımız model*/

	[Table("LimonQuestions")]
	public class LimonQuestion : MyEntityBase
	{
		[Required(ErrorMessage ="{0} alanı boş geçilemez"),
			DisplayName("Soru"),
			StringLength(80 , ErrorMessage ="{0} alanı max. {1} karakter içermelidir.")]
		public string Title { get; set; }

		[Required(ErrorMessage = "{0} alanı boş geçilemez"),
			DisplayName("Cevap"),
			StringLength(250, ErrorMessage = "{0} alanı max. {1} karakter içermelidir.")]
		public string Text { get; set; }
	}
}
