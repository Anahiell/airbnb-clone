using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.ValueObjects;

public class SafetyRule : ValueObject
{
    public int Id { get; private set; }
    public string Type { get; private set; }
    public string Label { get; private set; }

    private SafetyRule() { }

    public SafetyRule(int id, string type, string label)
    {
        Id = id;
        Type = type;
        Label = label;
    }
    
    public SafetyRule(string text)
    {
        Type = "Default";
        Label = text;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
        yield return Type;
        yield return Label;
    }
}