using LimonDijital.BusinessLayer.Abstract;
using LimonDijital.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LimonDijital.BusinessLayer.Results;
using LimonDijital.Entities.ValueObjects;
using LimonDijital.Common.Helpers;
using LimonDijital.Entities.Messages;

namespace LimonDijital.BusinessLayer
{
	public class UserManager : ManagerBase<LimonUser>
	{
		// kullanıcı kayıt metodu
		public BusinessLayerResult<LimonUser> RegisterUser(RegisterViewModel data)
		{
			// Kullanıcı username kontrolü
			// kullanıcı eposta kontrolü
			// Kayıt işlemi
			// Aktivasyon e-postası gönderimi

			LimonUser user = Find(x => x.Username == data.Username || x.Email == data.EMail);
			BusinessLayerResult<LimonUser> layerResult = new BusinessLayerResult<LimonUser>();

			if (user != null)
			{
				// throw new Exception("Kayıtlı kullanıcı adı veya e-posta adres."); hata dönderme metotu kullanılabilri
				if (user.Username == data.Username)
				{
					layerResult.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");
				}

				if (user.Email == data.EMail)
				{
					layerResult.AddError(ErrorMessageCode.UserCouldNotInserted, "E-posta adresi kayıtlı.");
				}
			}
			else
			{
				int dbResult = base.Insert(new LimonUser()
				{
					Username = data.Username,
					Email = data.EMail,
					Password = UserHelper.CryptoPassword(data.Password),
					ProfileImageFilename = "/User/user_boy.png",
					IsActive = false,
					IsAdmin = false
				});

				if (dbResult > 0)
				{
					layerResult.Result = Find(x => x.Email == data.EMail && x.Username == data.Username);

					// TODO : site admini tarafından etkinleştirilmesi istenicek
				}
			}


			return layerResult;
		}

		public BusinessLayerResult<LimonUser> GetUserById(int id)
		{
			BusinessLayerResult<LimonUser> res = new BusinessLayerResult<LimonUser>();
			res.Result = Find(x => x.Id == id);

			if (res.Result == null)
			{
				res.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı bulunamadı!");
			}

			return res;
		}

		// kullanıcı kayıt metodu
		public BusinessLayerResult<LimonUser> LoginUser(LoginViewModel data)
		{
			BusinessLayerResult<LimonUser> res = new BusinessLayerResult<LimonUser>();
			string password = UserHelper.CryptoPassword(data.Password);
			res.Result = Find(x => x.Username == data.Username && x.Password == password);

			if (res.Result != null)
			{
				if (!res.Result.IsActive)
				{
					res.AddError(ErrorMessageCode.UserIsNotActive, "Kullanıcı hesabı henüz aktifleştirilmemiştir");
					res.AddError(ErrorMessageCode.CheckYourEmail, "Lütfen site yöneticisinden hesabınızı aktif etmesini isteyiniz.");
				}
			}
			else
			{
				res.AddError(ErrorMessageCode.UsernameOrPassWrong, "Kullanıcı adı yada şifre hatalı.");
			}

			return res;
		}

		// Kullanıcı ekleme
		public new BusinessLayerResult<LimonUser> Insert(LimonUser data)
		{
			LimonUser user = Find(x => x.Username == data.Username || x.Email == data.Email);
			BusinessLayerResult<LimonUser> res = new BusinessLayerResult<LimonUser>();

			if (user != null)
			{
				// throw new Exception("Kayıtlı kullanıcı adı veya e-posta adres."); hata dönderme metotu kullanılabilri
				if (user.Username == data.Username)
				{
					res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");
				}

				if (user.Email == data.Email)
				{
					res.AddError(ErrorMessageCode.UserCouldNotInserted, "E-posta adresi kayıtlı.");
				}
			}
			else
			{
				int dbResult = base.Insert(new LimonUser()
				{
					Name = data.Name,
					Surname = data.Surname,
					Username = data.Username,
					Email = data.Email,
					Password = UserHelper.CryptoPassword(data.Password),
					ProfileImageFilename = "/User/user_boy.png",
					IsActive = data.IsActive,
					IsAdmin = data.IsAdmin
				});

				if (dbResult > 0)
				{
					res.Result = Find(x => x.Email == data.Email && x.Username == data.Username);

					// TODO : site admini tarafından etkinleştirilmesi istenicek
				}
				else
				{
					res.AddError(ErrorMessageCode.UserCouldNotInserted, "Kullanıcı Eklenemedi.");
				}
			}

			return res;
		}

		public new BusinessLayerResult<LimonUser> Update(LimonUser data)
		{
			LimonUser db_user = Find(x => x.Username == data.Username || x.Email == data.Email);
			BusinessLayerResult<LimonUser> res = new BusinessLayerResult<LimonUser>();
			res.Result = data;

			// aynı mail veya kullanıcı adında bir kullanıcı var ve bu kendi değil ise güncelleme yapamayız 
			// kullanılan bir kullanıcı adı veya maail istenmmiştir
			if (db_user != null && db_user.Id != data.Id)
			{
				// başkasının kullandığı kullancıı adı ile kayıt yenilemek istemiş
				if (db_user.Username == data.Username)
				{
					res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");
				}

				if (db_user.Email == data.Email)
				{
					res.AddError(ErrorMessageCode.UserCouldNotInserted, "E-posta adresi kayıtlı.");
				}

				return res;
			}

			res.Result = Find(x => x.Id == data.Id);
			res.Result.Email = data.Email;
			res.Result.Name = data.Name;
			res.Result.Surname = data.Surname;
			res.Result.Username = data.Username;
			res.Result.IsActive = data.IsActive;
			res.Result.IsAdmin = data.IsAdmin;

			if (base.Update(res.Result) == 0)
			{
				res.AddError(ErrorMessageCode.UserCouldNotUpdated, "Kullanıcı Güncellenemedi.");
			}

			return res;
		}

		// Kullanıcı düzenleme
		public BusinessLayerResult<LimonUser> UpdateProfile(LimonUser data)
		{
			LimonUser user = Find(x => x.Username == data.Username || x.Email == data.Email);
			BusinessLayerResult<LimonUser> res = new BusinessLayerResult<LimonUser>();

			if (user != null && user.Id != data.Id)
			{
				// throw new Exception("Kayıtlı kullanıcı adı veya e-posta adres."); hata dönderme metotu kullanılabilri
				if (user.Username == data.Username)
				{
					res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");
				}

				if (user.Email == data.Email)
				{
					res.AddError(ErrorMessageCode.UserCouldNotInserted, "E-posta adresi kayıtlı.");
				}
				return res;
			}
			res.Result = Find(x => x.Id == data.Id);
			res.Result.Email = data.Email;
			res.Result.Name = data.Name;
			res.Result.Surname = data.Surname;
			res.Result.Username = data.Username;

			if (user.Password != data.Password)
			{
				res.Result.Password = UserHelper.CryptoPassword(data.Password);
			}
			else
			{
				res.Result.Password = data.Password;
			}

			// resim günceleniyor ise aşağıdaki if bloğuna girecektir ve yeni dosya adını database kaydedecektir
			if (string.IsNullOrEmpty(data.ProfileImageFilename) == false)
			{
				res.Result.ProfileImageFilename = data.ProfileImageFilename; // resim güncellenmiş
			}

			if (base.Update(res.Result) == 0)
			{
				res.AddError(ErrorMessageCode.ProfileCouldNotUpdated, "Profil Güncellenemedi.");
			}

			return res;
		}

		// hesap silme metodu
		public BusinessLayerResult<UserManager> DeleteUserById(int id)
		{
			LimonUser db_user = Find(x => x.Id == id);
			BusinessLayerResult<UserManager> res = new BusinessLayerResult<UserManager>();

			// kullanıcı bulunamadı
			if (db_user != null)
			{
				// silme işlemi başarısız
				if (Delete(db_user) == 0)
				{
					res.AddError(ErrorMessageCode.UserCouldNotRemove, "Kullanıcı Silinemedi.");
					return res;
				}
			}
			else
			{
				res.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı Bulunamadı.");
			}

			return res;
		}
	}
}
