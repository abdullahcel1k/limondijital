using LimonDijital.BusinessLayer.Abstract;
using LimonDijital.BusinessLayer.Results;
using LimonDijital.Entities;
using LimonDijital.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimonDijital.BusinessLayer
{
	public class SliderManager : ManagerBase<LimonSlider>
	{
		public new BusinessLayerResult<LimonSlider> Insert(LimonSlider data)
		{
			BusinessLayerResult<LimonSlider> res = new BusinessLayerResult<LimonSlider>();

			res.Result = data;

			res.Result.SliderImageFilename = data.SliderImageFilename;

			if (base.Insert(res.Result) == 0)
			{
				res.AddError(ErrorMessageCode.SliderCouldNotInserted, "Slider eklenemedi.");
			}

			return res;
		}

		public new BusinessLayerResult<LimonSlider> Update(LimonSlider data)
		{
			BusinessLayerResult<LimonSlider> res = new BusinessLayerResult<LimonSlider>();

			res.Result = Find(x => x.Id == data.Id);
			res.Result.Text = data.Text;
			res.Result.QueueNumber = data.QueueNumber;

			//editlenecek resim seçildiyse
			if (string.IsNullOrEmpty(data.SliderImageFilename) == false)
			{
				res.Result.SliderImageFilename = data.SliderImageFilename;
			}

			if (base.Update(res.Result) == 0)
			{
				res.AddError(ErrorMessageCode.PortofiloCouldNotUpdated, "Slider Güncellenemedi.");
			}

			return res;
		}
	}
}
