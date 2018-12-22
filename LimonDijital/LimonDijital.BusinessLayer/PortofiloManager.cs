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
	public class PortofiloManager : ManagerBase<LimonPortofilo>
	{

		public new BusinessLayerResult<LimonPortofilo> Insert(LimonPortofilo data)
		{
			BusinessLayerResult<LimonPortofilo> layerResult = new BusinessLayerResult<LimonPortofilo>();

			layerResult.Result = data;

			layerResult.Result.PortofiloImageFilaname = data.PortofiloImageFilaname;

			if(base.Insert(layerResult.Result) == 0)
			{
				layerResult.AddError(ErrorMessageCode.PortofiloCouldNotInserted, "Portföy eklenemedi.");
			}

			return layerResult;
		}

		public new BusinessLayerResult<LimonPortofilo> Update(LimonPortofilo data)
		{
			BusinessLayerResult<LimonPortofilo> res = new BusinessLayerResult<LimonPortofilo>();

			res.Result = Find(x => x.Id == data.Id);
			res.Result.Name = data.Name;
			res.Result.Description = data.Description;
			
			//editlenecek resim seçildiyse
			if(string.IsNullOrEmpty(data.PortofiloImageFilaname) == false)
			{
				res.Result.PortofiloImageFilaname = data.PortofiloImageFilaname;
			}

			if(base.Update(res.Result) == 0)
			{
				res.AddError(ErrorMessageCode.PortofiloCouldNotUpdated, "Portföy Güncellenemedi.");
			}

			return res;
		}
	}
}
