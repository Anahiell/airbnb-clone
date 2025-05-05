using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;

[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum SortState
{

    PriceAsc,

    PriceDesc,

    PublishedDateAsc,

    PublishedDateDesc,

    MostLikedAsc,

    MostLikedDesc,

    MostViewedAsc,

    MostViewedDesc
}