using Education.Application.DTO_s.FeedBackDTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Services.FeedBackServices
{
    public interface IFeedBackServices
    {
        Task<bool> Delete(int feedBackId);
        Task<bool> Update(int feedBackId, UpdateFeedBackDTO updateFeed);
        Task CreateFeedBack(FeedBackDTO feedBack);
        int GetSectionRate(int sectionId);
        Task<IEnumerable<FeedBackDTO>> GetByVedioId(int Id);
    }
}
