using LimonDijital.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimonDijital.DataAccessLayer.EntityFramework
{
	public class DatabaseContext : DbContext
	{
		public DbSet<LimonUser> LimonUsers { get; set; }
		public DbSet<LimonPortofilo> LimonPortofilos { get; set; }
		public DbSet<LimonQuestion> LimonQuestions { get; set; }
		public DbSet<LimonReference> LimonReferences { get; set; }
		public DbSet<LimonService> LimonServices { get; set; }
		public DbSet<LimonSiteInfo> LimonSiteInfos { get; set; }
		public DbSet<LimonSlider> LimonSliders { get; set; }

		public DatabaseContext()
		{
			Database.SetInitializer(new MyInitializer());
		}
	}
}
