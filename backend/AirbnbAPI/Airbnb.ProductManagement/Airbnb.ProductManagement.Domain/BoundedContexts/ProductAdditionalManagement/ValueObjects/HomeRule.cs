using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.ValueObjects;

public class HomeRule : AggregateRoot
{
    public int Id { get; private set; }
    protected override void When(IDomainEvent @event)
    {
        throw new NotImplementedException();
    }

    public string Type { get; set; }
    public string Text { get; set; }

    private HomeRule() { }

    public HomeRule(int id, string type, string text)
    {
        Id = id;
        Type = type;
        Text = text;
    }
    
    public HomeRule(string text)
    {
        Type = "Default";
        Text = text;
    }
}