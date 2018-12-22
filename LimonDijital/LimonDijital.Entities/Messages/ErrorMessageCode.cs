using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimonDijital.Entities.Messages
{
	// enumlarda usinlger olmaz
	public enum ErrorMessageCode
	{
		// hata mesajlarına göre business katmanda ona göre değer veriyoruz
		// bu değerleri kullanarak da UI katmanımızda çeşiktli aktivasyonlar yapılabilir 
		// örneğin 151 kodlu kullanıcı aktif değil hatası için emaili tekrar gönderelimmi sayfasına yönlendirme gibi
		UsernameAlreadyExists = 101,
		EmailAlreadyExists = 102,
		UserIsNotActive = 151,
		UsernameOrPassWrong = 152,
		CheckYourEmail = 153,
		UserAlreadyActive = 154,
		ActivateIdDoesNotExists = 155,
		UserNotFound = 156,
		ProfileCouldNotUpdated = 157,
		UserCouldNotRemove = 158,
		UserCouldNotInserted = 159,
		UserCouldNotUpdated = 160,
		PostNotFound = 161,
		PortofiloCouldNotUpdated = 162,
		PortofiloCouldNotInserted = 163,
		PortofilNotDeleted = 164,
		ReferenceCouldNotInserted = 165,
		SliderCouldNotInserted = 166,
		SliderNotDeleted = 167,
		ReferenceCouldNotUpdated = 168,
		SiteSettingNotUpdated = 169,
	}
}
