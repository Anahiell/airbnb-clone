using Microsoft.AspNetCore.Http;

namespace Airbnb.PictureManagement.Application.BoundedContext.FileService;

public interface IFileService
{
    public Task<string> SaveAsync(IFormFile file, string category, CancellationToken cancellationToken = default);
    Task DeleteAsync(string relativePath, string category);
}