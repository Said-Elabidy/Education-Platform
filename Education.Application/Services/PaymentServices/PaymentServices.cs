using Education.Application.DTO_s.PaymentDtos;
using Education.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Services.PaymentServices
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IpaymentReposatory<PaymentDtos> _paymentReposatory;
        public PaymentServices(IpaymentReposatory<PaymentDtos> paymentReposatory)
        {
            _paymentReposatory = paymentReposatory;
        }
        public async Task<IEnumerable<PaymentDtos>> GetPaymentsByUserId(string userId)
        {
            return await _paymentReposatory.GetPaymentsByUserId(userId);
        }
        public async Task<IEnumerable<PaymentDtos>> GetPaymentsByCourseId(int courseId)
        {
            return await _paymentReposatory.GetPaymentsByCourseId(courseId);
        }
        public async Task<PaymentDtos?> GetPayment(string userId, int courseId)
        {
            return await _paymentReposatory.GetPayment(userId, courseId);
        }
        public async Task<bool> IsPaid(string userId, int courseId)
        {
            return await _paymentReposatory.IsPaid(userId, courseId);
        }
        public async Task<bool> IsPaid(string userId)
        {
            return await _paymentReposatory.IsPaid(userId);
        }



    }
}
