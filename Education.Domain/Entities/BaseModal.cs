using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Entities
{
	public class BaseModal
	{
		public bool IsDeleted { get; set; }
		public DateTime CreateOn { get; set; } = DateTime.Now;
		public DateTime? LastUpdateOn { get; set; }
	}
}
