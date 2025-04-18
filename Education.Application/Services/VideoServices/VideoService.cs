using Education.Application.Services.Storage_Services;
using Education.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Education.Domain.Repository;
using Education.Application.Services.VideoServices.VideoDurationService;

namespace Education.Application.Services.VideoServices
{
    public class VideoService : IVideoService
    {
        private readonly IStorageService _storageService;
        private readonly IVideoRepository _videoRepository;

        public VideoService(IStorageService storageService, IVideoRepository videoRepository)
        {
            _storageService = storageService;
            _videoRepository = videoRepository;
        }

        public async Task<List<Video>> GetAllAsync()
        {
            var videos = await _videoRepository.GetAllAsync();

            if (videos == null || !videos.Any())
            {
                return new List<Video>();
            }

            return videos.ToList();
        }

        public async Task<Video?> GetByIdAsync(int id)
        {
            var video = await _videoRepository.GetByIdAsync(id);
            if (video == null)
            {
                return null;
            }
            return video;
        }

        public async Task<Video?> CreateAsync(AddVideoDto addVideoDto)
        {
            // Upload video to Supabase
            var videoFileUrl = await _storageService.UploadToSupabaseAsync(
                addVideoDto.VideoFile,
                "videos" // bucket name
            );

            // Upload thumbnail if provided
            string? thumbnailUrl = null;
            if (addVideoDto.ThumbnailImage != null)
            {
                thumbnailUrl = await _storageService.UploadToSupabaseAsync(
                    addVideoDto.ThumbnailImage,
                    "thumbnails" // different bucket for images
                );
            }

            // Get video duration
            var videoDuration = await CalculateVideoDuration.GetVideoDurationAsync(addVideoDto.VideoFile);

            var video = new Video
            {
                Title = addVideoDto.Title,
                Description = addVideoDto.Description,
                VideoDuration = videoDuration,
                IsFree = addVideoDto.IsFree,
                VideoFileUrl = videoFileUrl,
                VideoImageUrl = thumbnailUrl, // Use the uploaded URL
                SectionId = addVideoDto.SectionId
            };

            if (video != null)
            {
                await _videoRepository.AddAsync(video);
                await _videoRepository.SaveChangesAsync();
            }
           
            return video;
            
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Video> UpdateAsync(int id, IFormFile newFile, string title)
        {
            throw new NotImplementedException();
        }

        public Task<TimeSpan> GetVideoDurationAsync(string filePath)
        {
            throw new NotImplementedException();
        }

        //public async Task<Domain.Entities.Video> CreateAsync(IFormFile file, AddVideoDto addVideoDto)
        //{
        //    // Save file temporarily
        //    var tempFilePath = Path.GetTempFileName();
        //    using (var stream = new FileStream(tempFilePath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(stream);
        //    }

        //    // Upload to Supabase
        //    await _storageService.UploadToSupabaseAsync(tempFilePath, file.FileName);

        //    // Get video duration
        //    var duration = await GetVideoDurationAsync(tempFilePath);

        //    // Create the Video object
        //    Domain.Entities.Video video = new Domain.Entities.Video
        //    {
        //        Title = addVideoDto.Title,
        //        Description = addVideoDto.Description,
        //        VideoDuration = duration,
        //        IsFree = addVideoDto.IsFree,
        //        SectionId = addVideoDto.SectionId,
        //        VideoFileUrl = $"https://jsspgwwxxozqehdbqpge.supabase.co/storage/v1/object/public/videos/{file.FileName}",
        //        VideoImageUrl = addVideoDto.VideoImageUrl
        //    };


        //    return video;
        //}


        //public async Task<bool> DeleteAsync(int id)
        //{
        //    var video = _videos.FirstOrDefault(v => v.VideoId == id);
        //    if (video == null) return false;

        //    // Delete the video file from Supabase
        //    await _storageService.DeleteFromSupabaseAsync(video.VideoFileUrl);
        //    _videos.Remove(video);
        //    return true;
        //}

        /*  public async Task<Video> UpdateAsync(int id, IFormFile newFile, string title, string description, bool isFree)
          {
              var video = _videos.FirstOrDefault(v => v.VideoId == id);
              if (video == null) return null;

              var filePath = Path.GetTempFileName();

              // Save the new file locally temporarily
              using (var stream = new FileStream(filePath, FileMode.Create))
              {
                  await newFile.CopyToAsync(stream);
              }

              // Upload the new file to Supabase
              await _storageService.UpdateFileInSupabaseAsync(filePath, newFile.FileName);

              // Get the new video duration
              var videoDuration = await GetVideoDurationAsync(filePath);

              // Update video properties
              video.Title = title;
              video.Description = description;
              video.VideoDuration = videoDuration.TotalSeconds;
              video.IsFree = isFree;
              video.VideoFileUrl = $"https://jsspgwwxxozqehdbqpge.supabase.co/storage/v1/object/public/videos/{newFile.FileName}";
              video.VideoImageUrl = "your-image-url-here"; // Update if you have image logic

              return video;
          }
        */

        // Method to extract the video duration

    }
}

