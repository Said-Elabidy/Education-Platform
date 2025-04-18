using Education.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Repository
{
    public interface IFeedBackRepo<TFeedBack> : IGenericRepository<FeedBack>
    {
        int GetRatingBySection(int sectionId); 
        Task<IEnumerable<TFeedBack>> FeedBackByVedio(int vedioId); 
    }
}
