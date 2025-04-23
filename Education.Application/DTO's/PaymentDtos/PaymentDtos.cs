using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.DTO_s.PaymentDtos
{
    public class PaymentDtos
    {
        public string UserId { get; set; }
        public int CourseId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
