using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.ValueObjects;

public class HomeRule : ValueObject
{
    public int Id { get; private set; }
    public string Type { get; private set; }
    public string Text { get; private set; }

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

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
        yield return Type;
        yield return Text;
    }
}