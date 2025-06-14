namespace Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;

public class CancelPolicyEntityInfo
{
    public int FreeCancelationDays { get; private set; }
    public int PartCancelationDays { get; private set; }
    public int PartCancelationPercent { get; private set; }
}