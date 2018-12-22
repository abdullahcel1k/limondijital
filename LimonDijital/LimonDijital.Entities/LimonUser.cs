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
	/* Kullanıcı verilerinin tutulacağı model*/
	[Table("LimonUsers")]
	public class LimonUser : MyEntityBase
	{
		[DisplayName("İsim"), StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
		public string Name { get; set; }

		[DisplayName("Soyisim"), StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
		public string Surname { get; set; }

		[DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "{0} alanı gereklidir."),
			StringLength(30, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
		public string Username { get; set; }

		[Required(ErrorMessage = "{0} alanı gereklidir."), DisplayName("E-Posta"),
			StringLength(80, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
		public string Email { get; set; }

		[Required(ErrorMessage = "{0} alanı gereklidir."), DisplayName("Şifre"),
			StringLength(32, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
		public string Password { get; set; }

		[StringLength(50), ScaffoldColumn(false)]   // images/user_12.jpg user id bilgisi ile isimlendirme
		public string ProfileImageFilename { get; set; }

		[DisplayName("Is Active")]
		public bool IsActive { get; set; }

		[DisplayName("Is Admin")]
		public bool IsAdmin { get; set; }
	}
}
