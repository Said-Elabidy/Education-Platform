using Education.Application.DTO_s.PaymentDtos;
using Education.Domain.Entities;
using Education.Domain.Repository;
using Education.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Infrastructure.Repository
{
    public class PaymentReposatory : GenericRepository<Payment>, IpaymentReposatory<PaymentDtos>
    {
        
        public PaymentReposatory(EducationPlatformDBContext context) : base(context)
        {
            
        }
        public async Task<IEnumerable<PaymentDtos>> GetPaymentsByUserId(string userId)
        {
            var payments = await _context.payments
                .Where(p => p.UserId == userId)
                .Select(p => new PaymentDtos
                {
                    UserId = p.UserId,
                    CourseId = p.CourseId,
                    TotalPrice = p.TotalPrice
                })
                .ToListAsync();

            return payments;
        }
        public async Task<IEnumerable<PaymentDtos>> GetPaymentsByCourseId(int courseId)
        {
            var payments = await _context.payments
                .Where(p => p.CourseId == courseId)
                .Select(p => new PaymentDtos
                {
                    UserId = p.UserId,
                    CourseId = p.CourseId,
                    TotalPrice = p.TotalPrice
                })
                .ToListAsync();

            return payments;
        }
        public async Task<PaymentDtos?> GetPayment(string userId, int courseId)
        {
            var payment = await _context.payments
                .FirstOrDefaultAsync(p => p.UserId == userId && p.CourseId == courseId);
            if (payment == null) return null;
            return new PaymentDtos
            {
                UserId = payment.UserId,
                CourseId = payment.CourseId,
                TotalPrice = payment.TotalPrice
            };
        }

        public async Task<bool> IsPaid(string userId, int courseId)
        {
            var payment = await _context.payments
                .FirstOrDefaultAsync(p => p.UserId == userId && p.CourseId == courseId);
            return payment != null;
        }
        public async Task<bool> IsPaid(string userId)
        {
            var payment = await _context.payments
                .FirstOrDefaultAsync(p => p.UserId == userId);
            return payment != null;
        }
    }
    

    

     
}
