using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimonDijital.Entities
{
	public class MyEntityBase
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required, DisplayName("Oluşturulma Tarihi")]
		public DateTime CreatedOn { get; set; }

		[Required, DisplayName("Güncellenme Tarihi")]
		public DateTime ModifiedOn { get; set; }

		[Required, StringLength(30), DisplayName("Güncelleyen")]
		public string ModifiedUsername { get; set; }
	}
}
