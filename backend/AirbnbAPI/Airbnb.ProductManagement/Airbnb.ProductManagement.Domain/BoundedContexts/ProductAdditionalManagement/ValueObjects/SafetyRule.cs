using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.ValueObjects;

public class SafetyRule : AggregateRoot
{
    public int Id { get; private set; }
    protected override void When(IDomainEvent @event)
    {
        throw new NotImplementedException();
    }

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
}