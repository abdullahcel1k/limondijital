using LimonDijital.Entities;
using System;
using System.Data.Entity;

namespace LimonDijital.DataAccessLayer.EntityFramework
{
	public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>
	{
		// TODO : sınıflara fake datalar basılacak  businesslayer de test metodu oluşturulacak
		protected override void Seed(DatabaseContext context)
		{

			// admin kullanıcı
			LimonUser admin = new LimonUser()
			{
				Name = "Abdullah",
				Surname = "ÇELİK",
				Email = "abdullahcelkk@gmail.com",
				IsActive = true,
				IsAdmin = true,
				Username = "abdullahcelik",
				Password = "e10adc3949ba59abbe56e057f20f883e", //123456
				ProfileImageFilename = "/User/user_boy.png",
				CreatedOn = DateTime.Now,
				ModifiedOn = DateTime.Now.AddMinutes(5),
				ModifiedUsername = "abdullahcelik",
			};

			// standart kullanıcı
			LimonUser standartuser = new LimonUser()
			{
				Name = "Abdullah",
				Surname = "ÇELİK2",
				Email = "celkkabdullah@gmail.com",
				IsActive = true,
				IsAdmin = false,
				Username = "abdullahc",
				Password = "c33367701511b4f6020ec61ded352059", //654321
				ProfileImageFilename = "/User/user_boy.png",
				CreatedOn = DateTime.Now,
				ModifiedOn = DateTime.Now.AddMinutes(5),
				ModifiedUsername = "abdullahcelik",
			};

			context.LimonUsers.Add(admin);
			context.LimonUsers.Add(standartuser);
			context.SaveChanges();

			// hizmelter ve portofiloların eklenmesi
			for(int i = 0; i < 3; i++)
			{
				LimonService services = new LimonService()
				{
					Name = FakeData.NameData.GetFirstName(),
					Description = FakeData.TextData.GetSentence(),
					CreatedOn = DateTime.Now,
					ModifiedOn = DateTime.Now.AddMinutes(5),
					ModifiedUsername = "abdullahcelik"
				};

				context.LimonServices.Add(services);				
			}
			context.SaveChanges();

            for (int j = 0; j < 9; j++)
            {
                LimonPortofilo portofilos = new LimonPortofilo()
                {
                    Name = FakeData.NameData.GetFullName(),
                    Description = FakeData.TextData.GetSentence(),
                    PortofiloImageFilaname = "/Portofilo/img_" + j + ".jpg",
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now.AddMinutes(5),
                    ModifiedUsername = "abdullahcelik"
                };

                context.LimonPortofilos.Add(portofilos);
            }
            context.SaveChanges();

            // soruların eklenmesi
            for (int i = 1; i < 7; i++)
			{
				LimonQuestion questions = new LimonQuestion()
				{
					Title = FakeData.NameData.GetFullName(),
					Text = FakeData.TextData.GetSentence(),
					CreatedOn = DateTime.Now,
					ModifiedOn = DateTime.Now.AddMinutes(5),
					ModifiedUsername = "abdullahcelik",
				};
				context.LimonQuestions.Add(questions);
			}
			context.SaveChanges();

			// referansların eklenmesi
			for(int i = 0; i < 2; i++)
			{
				for(int j = 1; j < 7; j++)
				{
					LimonReference references = new LimonReference()
					{
						Name = FakeData.NameData.GetFullName(),
						ReferencesImageFilename = "/References/img_" + j+".jpg",
						CreatedOn = DateTime.Now,
						ModifiedOn = DateTime.Now.AddMinutes(5),
						ModifiedUsername = "abdullahcelik",
					};
					context.LimonReferences.Add(references);
				}
			}
			context.SaveChanges();

			// sliderların eklenmesi
			for(int i = 1; i < 4; i++)
			{
				LimonSlider sliders = new LimonSlider()
				{
					Text = FakeData.NameData.GetFullName(),
					QueueNumber = i,
					SliderImageFilename = "/Slider/img_bg_" + i+".jpg",
					CreatedOn = DateTime.Now,
					ModifiedOn = DateTime.Now.AddMinutes(5),
					ModifiedUsername = "abdullahcelik"
				};
				context.LimonSliders.Add(sliders);
			}
			context.SaveChanges();

			LimonSiteInfo siteInfo = new LimonSiteInfo()
			{
				SiteName = "Limon Dijital",
				SiteTitle = "Limon Dijital | Tabela modelleri ve reklama dair herşey",
				Address = FakeData.PlaceData.GetAddress(),
				CustomerCount = 1500,
				ProjectCount = 350,
				SiteKeywords = "tabela, görsel, tabela modelleri, reklam, istanbul reklam, bayrampaşa reklam",
				Phone1 = "0212 438 72 47",
				Phone2 = "0532 360 60 27",
				Mail1 = "info@limondijital.com",
				Mail2 = "yenilimondijital@gmail.com",
				MapSrc = "https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d3008.401421259659!2d28.8965919!3d41.0602173!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x14cab07bb1e09ad7%3A0x30bcb9b2ef55567b!2sKartaltepe+Mahallesi%2C+61.+Sk.+No%3A44%2C+34040+Bayrampa%C5%9Fa%2F%C4%B0stanbul!5e0!3m2!1str!2str!4v1513787219015",
				FacebookProfile = "https://www.facebook.com/Abdullahcelik123",
				LinkedinProfile = "https://www.linkedin.com/in/abdullah-çelik-b6308410a/",
				TwitterProfile = "https://twitter.com/userabdullahc",
				HomeServiceText = "İstanbul/Bayrampaşada genç ve dinamik ekimizle birlikte sizlere sağladığımız hizmetlerimiz hakkında kısa bilgiler. Daha detaylı bilgi edinmek için bizimle iletişime geçin lütfen.",
				HomePortofiloText = "Daha önce sunduğumuz hizmetlerden bağzıları aşağıda yer almaktadır, inceleyip kendi yaptırmak istediğiniz ürün hakkında öngörü sahibi olabilirsiniz.",
				HomeQuestionText = "Tüm sorularınıza cevap almak için iletişime geçebilir veya ziyaretimize gelebilirsiniz.",
				HomeReferenceText = "Daha önce bizi tercih eden müşterilimiz, mutlu müşterilerimiz arasına katılmak istemezmisiniz ?",
			};
			context.LimonSiteInfos.Add(siteInfo);
			context.SaveChanges();
		}
	}
}