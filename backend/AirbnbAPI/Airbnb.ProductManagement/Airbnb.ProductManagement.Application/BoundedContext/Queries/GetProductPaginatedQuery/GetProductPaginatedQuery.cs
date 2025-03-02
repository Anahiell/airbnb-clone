using Airbnb.Domain;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.Queries;

public class GetProductPaginatedQuery : IRequest<(IEnumerable<ProductEntityInfo> Items, long TotalCount)>
{
    public string? Country { get; set; }
    public string? City { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public DateTime? DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
    public IEnumerable<object>? Tags { get; set; }
    public int MinRating { get; set; }
    public int MaxRating { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public SortState SortOrder { get; set; }
}