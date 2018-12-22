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
	public class ReferenceManager : ManagerBase<LimonReference>
	{
		public new BusinessLayerResult<LimonReference> Insert(LimonReference data)
		{
			BusinessLayerResult<LimonReference> res = new BusinessLayerResult<LimonReference>();
			res.Result = data;

			res.Result.ReferencesImageFilename = data.ReferencesImageFilename;

			if (base.Insert(res.Result) == 0)
			{
				res.AddError(ErrorMessageCode.ReferenceCouldNotInserted, "Referans eklenemedi.");
			}

			return res;
		}

		public new BusinessLayerResult<LimonReference> Update(LimonReference data)
		{
			BusinessLayerResult<LimonReference> res = new BusinessLayerResult<LimonReference>();

			res.Result = Find(x => x.Id == data.Id);
			res.Result.Name = data.Name;

			//editlenecek resim seçildiyse
			if (string.IsNullOrEmpty(data.ReferencesImageFilename) == false)
			{
				res.Result.ReferencesImageFilename = data.ReferencesImageFilename;
			}

			if (base.Update(res.Result) == 0)
			{
				res.AddError(ErrorMessageCode.ReferenceCouldNotUpdated, "Referans Güncellenemedi.");
			}

			return res;
		}
	}
}
