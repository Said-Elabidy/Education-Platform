using Education.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Repository
{
    public interface IpaymentReposatory<TPayment>:IGenericRepository<Payment>
    {
        Task<IEnumerable<TPayment>> GetPaymentsByUserId(string userId);
        Task<IEnumerable<TPayment>> GetPaymentsByCourseId(int courseId);
        Task<TPayment?> GetPayment(string userId, int courseId);
        Task<bool> IsPaid(string userId, int courseId);
        Task<bool> IsPaid(string userId);
    }
}
