using Airbnb.TagsManagement.Domain.BoundedContexts.TagsManagement.Aggregates;

namespace Airbnb.TagsManagement.Domain.BoundedContexts.TagsManagement.Interfaces;

public interface ITagRepository
{
    Task<int> CreateTagAsync(DomainTag tag, CancellationToken cancellationToken = default);
    Task<DomainTag> GetTagByIdAsync(int tagId);
    Task<IEnumerable<DomainTag>> GetAllTagsAsync();
}