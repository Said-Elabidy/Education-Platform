using Education.Application.DTO_s.VideoDto_s;
using Education.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Services.VideoServices
{
    public interface IVideoService
    {
       
        Task<Video?> GetByIdAsync(int id);
        Task <IEnumerable<GetVideosBySectionIdDto?>> GetAllBySectionIdAsync(int id);
        Task<Video?> CreateAsync(AddVideoDto addVideoDto);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(int id, UpdateVideoDto updateVideoDto);

    }
}
