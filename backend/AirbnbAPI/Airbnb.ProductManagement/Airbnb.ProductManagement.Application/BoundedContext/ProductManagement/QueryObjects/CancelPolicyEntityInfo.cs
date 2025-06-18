namespace Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;

public class CancelPolicyEntityInfo
{
    public int FreeCancelationDays { get; set; }
    public int PartCancelationDays { get; set; }
    public int PartCancelationPercent { get; set; }
}