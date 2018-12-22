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
	/* şirkete ait slider resimlerini tutan model */

	[Table("LimonSliders")]
	public class LimonSlider : MyEntityBase
	{
		[Required, DisplayName("Resim Sloganı"),
			StringLength(40, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
		public string Text { get; set; }

		[Required, DisplayName("Resim Sırası")]
		public int QueueNumber { get; set; }

		[ScaffoldColumn(false), StringLength(55)]
		public string SliderImageFilename { get; set; }
	}
}
