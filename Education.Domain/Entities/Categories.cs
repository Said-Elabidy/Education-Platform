using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Entities
{
	public class Categories : BaseModal
	{
		public int CategorieId { get; set; }	
		public string Name { get; set; } 
		public ICollection<Courses>? courses { get; set; }	
	}
}
