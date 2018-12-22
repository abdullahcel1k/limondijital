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
	/* Şirket ile daha önce iş yapmış firmaların bilgisini tutan model*/

	[Table("LimonReferences")]
	public class LimonReference : MyEntityBase
	{
		[Required(ErrorMessage = "{0} alanı boş geçilemez."), DisplayName("Referans Adı"),
			StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter içerebilir.")]
		public string Name { get; set; }

		[StringLength(105), ScaffoldColumn(false)]
		public string ReferencesImageFilename { get; set; }
	}
}
