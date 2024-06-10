using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.BaseEntities
{
	public class BaseEntity : IBaseEntity
	{
		public virtual int Id { get; set; }
		public virtual string CreatedDate { get; set; } = null!;
		public virtual string? UpdatedDate { get; set; }
		public virtual byte[] RowVersion { get; set; } = null!;
	}
}
