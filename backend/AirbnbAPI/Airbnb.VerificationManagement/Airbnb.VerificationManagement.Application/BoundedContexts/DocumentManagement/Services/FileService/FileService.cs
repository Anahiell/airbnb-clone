using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Airbnb.PictureManagement.Application.BoundedContext.FileService;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _env;

    public FileService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<string> SaveAsync(IFormFile file, string category, CancellationToken cancellationToken = default)
    {
        var basePath = Path.Combine(_env.ContentRootPath, "Files", category);
        Directory.CreateDirectory(basePath);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var fullPath = Path.Combine(basePath, fileName);

        await using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream, cancellationToken);

        return $"/files/{category}/{fileName}";
    }

    public Task DeleteAsync(string relativePath, string category)
    {
        var fullPath = Path.Combine(_env.ContentRootPath, "Files", category, Path.GetFileName(relativePath));

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        return Task.CompletedTask;
    }
}