using Microsoft.AspNetCore.Http;

namespace Airbnb.IntegrationTesting.Helpers;

public static class FileHelper
{
    public static IFormFile LoadFormFile(string directory, string fileName, string contentType = "image/png")
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), directory, fileName);

        if (!File.Exists(path))
            throw new FileNotFoundException($"Test file not found: {path}");

        var stream = new FileStream(path, FileMode.Open, FileAccess.Read);

        return new FormFile(stream, 0, stream.Length, "formFile", fileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = contentType
        };
    }
}