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
	[Table("LimonPortofilos")]
	public class LimonPortofilo : MyEntityBase
	{
		[Required(ErrorMessage ="{0} alanı boş geçilemez."), DisplayName("Proje Adı"),
			StringLength(50, ErrorMessage ="{0} alanı max. {1} karakter olmalıdır.")]
		public string Name { get; set; }

		[DisplayName("Proje Açıklaması"), 
			StringLength(250, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
		public string Description { get; set; }

		[StringLength(75), ScaffoldColumn(false)]
		public string PortofiloImageFilaname { get; set; } // resim dosyasının adı resme verilenad+id olacak
	}
}
