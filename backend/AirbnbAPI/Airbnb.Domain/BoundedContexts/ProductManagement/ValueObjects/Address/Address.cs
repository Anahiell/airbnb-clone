using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductManagement.ValueObjects.Address;

public class Address : ValueObject
{
    public string FullAddress { get; private set; } = default!;
    public string Street { get; private set; } = default!;
    public string House { get; private set; } = default!;
    public string Apartment { get; private set; } = default!;
    
    public Address()
    {
        
    }
    
    public static ValueObjectValidationResult Create(string inn)
    {
        List<string> errors = new();

        if (inn.Length != 12 || !inn.All(char.IsDigit))
        {
            errors.Add("Invalid INN format. INN must be 12 digits long.");
        }

        if (errors.Any())
            return new ValueObjectValidationResult(null, errors);

        var subjectCode = new SubjectCode(inn.Substring(0, 2));
        var taxOfficeCode = new TaxOfficeCode(inn.Substring(2, 2));
        var serialNumber = new SerialNumber(inn.Substring(4, 6));
        var checkDigit = new CheckDigit(inn.Substring(10, 2));

        return new ValueObjectValidationResult(new INN(subjectCode, taxOfficeCode, serialNumber, checkDigit), errors);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return SubjectCode;
        yield return TaxOfficeCode;
        yield return SerialNumber;
        yield return CheckDigit;
    }
}