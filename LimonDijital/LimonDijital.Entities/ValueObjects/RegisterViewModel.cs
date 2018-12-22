using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimonDijital.Entities.ValueObjects
{
	public class RegisterViewModel
	{
		[DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "{0} alanı boş geçilemez"),
			StringLength(25, ErrorMessage = "{0} max. {1} karakter olamlı.")]
		public string Username { get; set; }


		[DisplayName("E-Post"), Required(ErrorMessage = "{0} alanı boş geçilemez"),
			StringLength(70, ErrorMessage = "{0} max. {1} karakter olamlı."),
			EmailAddress(ErrorMessage = "{0} alanı için geçerli bir e-post adresi giriniz.")]
		public string EMail { get; set; }


		[DisplayName("Şifre"), Required(ErrorMessage = "{0} alanı boş geçilemez"),
			StringLength(25, ErrorMessage = "{0} max. {1} karakter olamlı.")]
		public string Password { get; set; }


		[DisplayName("Şifre Tekrar"), Required(ErrorMessage = "{0} alanı boş geçilemez"),
			StringLength(25, ErrorMessage = "{0} max. {1} karakter olamlı."),
			Compare("Password", ErrorMessage = "{0} ile {1} uyuşmuyor.")]
		public string RePassword { get; set; }
	}
}
