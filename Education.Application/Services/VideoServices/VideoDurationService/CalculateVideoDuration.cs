using MediaToolkit.Model;
using MediaToolkit;
using Microsoft.AspNetCore.Http;

namespace Education.Application.Services.VideoServices.VideoDurationService
{
    public static class CalculateVideoDuration
    {
        public static async Task<TimeSpan> GetVideoDurationAsync(IFormFile file)
        {
            // Create a temporary file path
            var tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));

            try
            {
                // Save the uploaded file to temp location
                using (var stream = new FileStream(tempFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var inputFile = new MediaFile { Filename = tempFilePath };
                using (var engine = new Engine())
                {
                    engine.GetMetadata(inputFile);
                    return inputFile.Metadata.Duration;
                }
            }
            finally
            {
                // Clean up the temporary file
                if (File.Exists(tempFilePath))
                {
                    File.Delete(tempFilePath);
                }
            }
        }
    }
}
