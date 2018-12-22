using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimonDijital.Entities.ValueObjects
{
	public class HomePageViewModel
	{
		public List<LimonPortofilo> limonPortofilos { get; set; }
		public List<LimonQuestion> limonQuestions { get; set; }
		public List<LimonReference> limonReferences { get; set; }
		public List<LimonService> limonServices { get; set; }
		public LimonSiteInfo limonSiteInfo { get; set; }
		public List<LimonSlider> limonSliders { get; set; }
	}
}
