using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.ValueObjects;

public class CancelPolicy : ValueObject
{
    public int FreeCancelationDays { get; private set; }
    public int PartCancelationDays { get; private set; }
    public int PartCancelationPercent { get; private set; }

    private CancelPolicy() { }

    public CancelPolicy(
        int freeCancelationDays,
        int partCancelationDays,
        int partCancelationPercent)
    {
        FreeCancelationDays = freeCancelationDays;
        PartCancelationDays = partCancelationDays;
        PartCancelationPercent = partCancelationPercent;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FreeCancelationDays;
        yield return PartCancelationDays;
        yield return PartCancelationPercent;
    }
}