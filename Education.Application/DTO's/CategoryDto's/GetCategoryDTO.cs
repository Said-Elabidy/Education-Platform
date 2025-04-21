using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.DTO_s.CategoryDto_s
{
    public class GetCategoryDTO
    {
        public int CategorieId { get; set; }
        public string Name { get; set; }
        public DateTime CreateOn { get; set; } = DateTime.Now;
        public DateTime? LastUpdateOn { get; set; }
    }
}
