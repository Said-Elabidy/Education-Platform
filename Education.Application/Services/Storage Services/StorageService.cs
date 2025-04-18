using Microsoft.AspNetCore.Http;
using RestSharp;
using System.Net.Http.Headers;

namespace Education.Application.Services.Storage_Services;

public class StorageService : IStorageService
{

    public async Task<string> UploadToSupabaseAsync(IFormFile file, string bucketName)
    {
        string anonKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9." +
     "eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Impzc3Bnd3d4eG96cWVoZGJxcGdlIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDQ4ODk0MzAsImV4cCI6MjA2MDQ2NTQzMH0." +
            "VsQ5mxxZVQTtiAtxGDnRw0ARzp1HiKvUQijO1K5NUG0";

        string projectUrl = "https://jsspgwwxxozqehdbqpge.supabase.co";

        // Generate unique filename

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

        var url = $"{projectUrl}/storage/v1/object/{bucketName}/{fileName}";

        using var client = new HttpClient();

        using var formData = new MultipartFormDataContent();

        using var fileStream = file.OpenReadStream();

        var fileContent = new StreamContent(fileStream);

        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(file.ContentType);

        formData.Add(fileContent, "file", fileName);

        var request = new HttpRequestMessage(HttpMethod.Post, url);

        request.Headers.Add("apikey", anonKey);

        request.Headers.Add("Authorization", $"Bearer {anonKey}");

        request.Content = formData;

        var response = await client.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();

            throw new Exception($"Upload failed: {response.StatusCode} - {errorContent}");
        }

        return $"{projectUrl}/storage/v1/object/public/{bucketName}/{fileName}";
    }

    public async Task DeleteFromSupabaseAsync(string fileName)
    {
        string anonKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9." +
        "eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Impzc3Bnd3d4eG96cWVoZGJxcGdlIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDQ4ODk0MzAsImV4cCI6MjA2MDQ2NTQzMH0." +
        "VsQ5mxxZVQTtiAtxGDnRw0ARzp1HiKvUQijO1K5NUG0";
        string projectUrl = "https://jsspgwwxxozqehdbqpge.supabase.co";
        string bucketName = "videos";

        var url = $"{projectUrl}/storage/v1/object/{bucketName}/{fileName}";

        var client = new RestClient();
        var request = new RestRequest(url, Method.Delete);

        request.AddHeader("apikey", anonKey);
        request.AddHeader("Authorization", $"Bearer {anonKey}");

        var response = await client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            Console.WriteLine("✅ File deleted successfully!");
        }
        else
        {
            Console.WriteLine($"❌ Deletion failed: {response.StatusCode} - {response.Content}");
        }
    }

    //public async Task UpdateFileInSupabaseAsync(string filePath, string fileName)
    //{
    //    await UploadToSupabaseAsync(filePath, fileName);
    //}

    
}

