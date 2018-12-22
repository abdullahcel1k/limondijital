using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimonDijital.Entities.ValueObjects
{
	public class LoginViewModel
	{
		[DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "{0} alanı boş geçilemez."),
			StringLength(25, ErrorMessage = "{0} max. {1} karakter olamlı.")]
		public string Username { get; set; }

		[DisplayName("Şifre"), Required(ErrorMessage = "{0} alanı bol geçilemez."),
			DataType(DataType.Password), StringLength(25, ErrorMessage = "{0} max. {1} karakter olamlı.")]
		public string Password { get; set; }
	}
}
