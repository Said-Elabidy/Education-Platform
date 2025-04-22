using Microsoft.AspNetCore.Http;

namespace Education.Application.Services.Storage_Services;

public interface IStorageService
{
    Task<string> UploadToSupabaseAsync(IFormFile file, string bucketName);
    Task<bool> DeleteFromSupabaseAsync(string fileName, string bucketName);
    //public Task DeleteFromSupabaseAsync(string fileName);
    //public Task UpdateFileInSupabaseAsync(string filePath, string fileName);
}
