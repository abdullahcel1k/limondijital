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
	/* site ile ilgili iletişim sosyal medya müşteri sayısı vs gibi
	 bilgileri saklayacağımız model*/

	[Table("LimonSiteInfo")]
	public class LimonSiteInfo
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required(ErrorMessage = "{0} alanı boş geçilemez."),
			DisplayName("Site Adı"),
			StringLength(150, ErrorMessage = "{0} alanı max. {1} karakter içermelidir.")]
		public string SiteName { get; set; }

		[DisplayName("Anahtar Kelimeler"),
			StringLength(1500, ErrorMessage = "{0} alanı max. {1} karakter içerebilir.")]
		public string SiteKeywords { get; set; }

		[Required(ErrorMessage = "{0} alanı boş geçilemez."), DisplayName("Site Başlığı"),
			StringLength(150, ErrorMessage = "{0} alanı max. {1} karakter içerebilir.")]
		public string SiteTitle { get; set; }

		[DisplayName("Müşteri Sayısı")]
		public int CustomerCount { get; set; }

		[DisplayName("Proje Sayısı")]
		public int ProjectCount { get; set; }

		[DisplayName("Linked-in Profiliniz"),
			StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter içerebilir.")]
		public string LinkedinProfile { get; set; }

		[DisplayName("Facebook Profiliniz"),
			StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter içerebilir.")]
		public string FacebookProfile { get; set; }

		[DisplayName("Twitter Profiliniz"),
			StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter içerebilir.")]
		public string TwitterProfile { get; set; }

		[Required(ErrorMessage = "{0} alanı boş geçilemez."), DisplayName("Adres"),
			StringLength(150, ErrorMessage ="{0} alanı max. {1} karakter içerebilir.")]
		public string Address { get; set; }

		[Required(ErrorMessage = "{0} alanı boş geçilemez."), DisplayName("Telefon1"),
			StringLength(15, ErrorMessage = "{0} alanı max. {1} karakter içerebilir.")]
		public string Phone1 { get; set; }

		[DisplayName("Telefon2"),
			StringLength(15, ErrorMessage = "{0} alanı max. {1} karakter içerebilir.")]
		public string Phone2 { get; set; }

		[Required(ErrorMessage = "{0} alanı boş geçilemez."), DisplayName("Mail1"),
			StringLength(150, ErrorMessage = "{0} alanı max. {1} karakter içerebilir.")]
		public string Mail1 { get; set; }

		[DisplayName("Mail2"),
			StringLength(150, ErrorMessage = "{0} alanı max. {1} karakter içerebilir.")]
		public string Mail2 { get; set; }

		[Required(ErrorMessage = "{0} alanı boş geçilemez."), DisplayName("Google Map Url"),
			StringLength(400, ErrorMessage = "{0} alanı max. {1} karakter içerebilir.")]
		public string MapSrc { get; set; }

		[Required(ErrorMessage = "{0} alanı boş geçilemez."), DisplayName("Hizmetlerimiz Yazısı"),
			StringLength(400, ErrorMessage = "{0} alanı max. {1} karakter içerebilir.")]
		public string HomeServiceText { get; set; }

		[Required(ErrorMessage = "{0} alanı boş geçilemez."), DisplayName("Portföy Yazısı"),
			StringLength(400, ErrorMessage = "{0} alanı max. {1} karakter içerebilir.")]
		public string HomePortofiloText { get; set; }

		[Required(ErrorMessage = "{0} alanı boş geçilemez."), DisplayName("Sorular Yazısı"),
			StringLength(400, ErrorMessage = "{0} alanı max. {1} karakter içerebilir.")]
		public string HomeQuestionText { get; set; }

		[Required(ErrorMessage = "{0} alanı boş geçilemez."), DisplayName("Referanslarımız Yazısı"),
			StringLength(400, ErrorMessage = "{0} alanı max. {1} karakter içerebilir.")]
		public string HomeReferenceText { get; set; }

	}
}
