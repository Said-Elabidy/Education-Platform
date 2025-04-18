using Education.Application.DTO_s.FeedBackDTO_s;
using Education.Domain.Entities;
using Education.Domain.Repository;
namespace Education.Application.Services.FeedBackServices
{
    public class FeedBackService : IFeedBackServices
    {
        private readonly IFeedBackRepo<FeedBackDTO> _feedBackRepo;

        public FeedBackService(IFeedBackRepo<FeedBackDTO> feedBackRepo)
        {
            _feedBackRepo = feedBackRepo;
        }

        public async Task CreateFeedBack(FeedBackDTO feedBack)
        {
            var entity = new FeedBack { Comment = feedBack.Comment, IsDeleted = false,
                CreateOn = DateTime.Now, Rating = feedBack.Rating, UserId = feedBack.UserId,
                VideoId = feedBack.VideoId, LastUpdateOn = DateTime.Now };
            await _feedBackRepo.AddAsync(entity);
            await _feedBackRepo.SaveChangesAsync();
        }

        public async Task<bool> Delete(int feedBackId)
        {
           var deleted= await _feedBackRepo.Delete(feedBackId);
            await _feedBackRepo.SaveChangesAsync();
            return deleted;
        }

        public async Task<IEnumerable<FeedBackDTO>> GetByVedioId(int Id)
        {
            return await _feedBackRepo.FeedBackByVedio(Id);
        }

        public int GetSectionRate(int sectionId)
        {
            return _feedBackRepo.GetRatingBySection(sectionId);
        }

        public async Task<bool> Update(int feedBackId, UpdateFeedBackDTO updateFeed)
        {
            var entity=await _feedBackRepo.GetByIdAsync(feedBackId);
            if (entity != null)
            {
                entity.Comment = updateFeed.Comment;
                entity.Rating = updateFeed.Rating;
                entity.LastUpdateOn = DateTime.Now;
                _feedBackRepo.Update(entity);
               await _feedBackRepo.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
