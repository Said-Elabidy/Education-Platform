using Microsoft.AspNetCore.Http;
using RestSharp;
using System.Net;
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

    public async Task<bool> DeleteFromSupabaseAsync(string publicUrl, string bucketName)
    {
        if (string.IsNullOrEmpty(publicUrl))
            return true; // Nothing to delete

        try
        {
            var uri = new Uri(publicUrl);
            var segments = uri.Segments; // This gives you the 7 segments

            // Verify we have the expected URL structure
            if (segments.Length != 7 ||
                segments[1] != "storage/" ||
                segments[2] != "v1/" ||
                segments[3] != "object/" ||
                segments[4] != "public/")
            {
                Console.WriteLine("❌ Invalid Supabase URL format");
                return false;
            }

            // Reconstruct the delete path (skip the 'public/' segment)
            var filePath = segments[5] + segments[6]; // "thumbnails/" + "filename.jpg"
            filePath = filePath.TrimEnd('/'); // Remove trailing slash if exists

            var projectUrl = "https://jsspgwwxxozqehdbqpge.supabase.co";
            var deleteUrl = $"{projectUrl}/storage/v1/object/{filePath}";

            Console.WriteLine($"🗑️ DELETE URL: {deleteUrl}"); // Debug output

            using var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Delete, deleteUrl);

            string anonKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9." +
     "eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Impzc3Bnd3d4eG96cWVoZGJxcGdlIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDQ4ODk0MzAsImV4cCI6MjA2MDQ2NTQzMH0." +
            "VsQ5mxxZVQTtiAtxGDnRw0ARzp1HiKvUQijO1K5NUG0";

            request.Headers.Add("apikey", anonKey);
            request.Headers.Add("Authorization", $"Bearer {anonKey}");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("✅ File deleted successfully");
                return true;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine("⚠️ File already deleted (404)");
                return true;
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"❌ Delete failed: {response.StatusCode} - {error}");
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"💥 Exception: {ex.Message}");
            return false;
        }
    }




}

