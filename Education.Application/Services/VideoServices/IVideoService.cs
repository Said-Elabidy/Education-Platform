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
        Task<List<Video>> GetAllAsync();
        Task<Video?> GetByIdAsync(int id);
        Task<Video?> CreateAsync(AddVideoDto addVideoDto);
        Task<bool> DeleteAsync(int id);
        Task<Video> UpdateAsync(int id, IFormFile newFile, string title);

        

    }
}
