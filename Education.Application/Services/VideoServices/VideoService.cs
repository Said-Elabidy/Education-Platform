using Education.Application.Services.Storage_Services;
using Education.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Education.Domain.Repository;
using Education.Application.Services.VideoServices.VideoDurationService;
using Education.Application.DTO_s.VideoDto_s;

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
                "videos" // bucket name for Videos
            );

            // Upload thumbnail if provided
            string? thumbnailUrl = null;
            if (addVideoDto.ThumbnailImage != null)
            {
                thumbnailUrl = await _storageService.UploadToSupabaseAsync(
                    addVideoDto.ThumbnailImage,
                    "thumbnails" // bucket for images
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

      
        public async Task<bool> UpdateAsync(int id, UpdateVideoDto updateDto )
        {
            var video =await _videoRepository.GetByIdAsync(id);
            if(video == null) return false;

            if(updateDto.Title!=null) video.Title = updateDto.Title;
            if (updateDto.Description != null) video.Description = updateDto.Description;
            if (updateDto.IsFree != null) video.IsFree = (bool)updateDto.IsFree;


            if (updateDto.ThumbnailImage != null)
            {
                // Delete old thumbnail if exists
                if (!string.IsNullOrEmpty(video.VideoImageUrl))
                {
                    await _storageService.DeleteFromSupabaseAsync(video.VideoImageUrl, "thumbnails");
                }
                video.VideoImageUrl = await _storageService.UploadToSupabaseAsync(
                    updateDto.ThumbnailImage, "thumbnails");
            }

            if (updateDto.VideoFile != null)
            {
                // Delete old video if exists
                if (!string.IsNullOrEmpty(video.VideoFileUrl))
                {
                    await _storageService.DeleteFromSupabaseAsync(video.VideoFileUrl, "videos");
                }
                video.VideoFileUrl = await _storageService.UploadToSupabaseAsync(
                    updateDto.VideoFile, "videos");

                // Get new video duration

                video.VideoDuration=  await CalculateVideoDuration.GetVideoDurationAsync(updateDto.VideoFile);

            }


            _videoRepository.Update(video);
            await _videoRepository.SaveChangesAsync();
            return true;
            
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var video = await _videoRepository.GetByIdAsync(id);
            if (video == null) return false;

            // Delete the Image file from Supabase
            if (!string.IsNullOrEmpty(video.VideoImageUrl))
            {
             var IsImageDeleted = await _storageService.DeleteFromSupabaseAsync(video.VideoImageUrl, "thumbnails");
            }
            // Delete the video file from Supabase
            var IsVideoDeleted = await _storageService.DeleteFromSupabaseAsync(video.VideoFileUrl, "videos");

            _videoRepository.Delete(video);
            await _videoRepository.SaveChangesAsync();
            return true;
        }




    }
}

