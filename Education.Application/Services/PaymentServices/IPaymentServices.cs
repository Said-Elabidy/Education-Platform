using Education.Application.DTO_s.PaymentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Services.PaymentServices
{
    public interface IPaymentServices
    {
        
        Task<IEnumerable<PaymentDtos>> GetPaymentsByUserId(string userId);
        Task<IEnumerable<PaymentDtos>> GetPaymentsByCourseId(int courseId);
        Task<PaymentDtos?> GetPayment(string userId, int courseId);
        Task<bool> IsPaid(string userId, int courseId);
        Task<bool> IsPaid(string userId);
    }
}
