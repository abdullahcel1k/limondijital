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
	/*Şirketin hizmet grupları yani türlerinin tutulacağı model*/

	[Table("LimonServices")]
	public class LimonService : MyEntityBase
	{
		[Required(ErrorMessage = "{0} alanı boş geçilemez."), DisplayName("Hizmet Adı"),
			StringLength(50, ErrorMessage ="{0} alanı max. {1} karakter olmalıdır.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "{0} alanı boş geçilemez."), DisplayName("Açıklama"),
			StringLength(150, ErrorMessage ="{0} alanı max. {1} karakter olmalıdır.")]
		public string Description { get; set; }
	}
}
