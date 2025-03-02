using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Airbnb.Domain.BoundedContexts.PropertyTypeManagement.ValueObjects;

public enum PropertyTypeEnum
{
    [Display(Name = "Apartment")]
    Apartment = 1,  // Квартира
    [Display(Name = "House")]
    House = 2,      // Дом
    [Display(Name = "Townhouse")]
    Townhouse = 3,  // Таунхаус
    [Display(Name = "Loft")]
    Loft = 4        // Лофт
}