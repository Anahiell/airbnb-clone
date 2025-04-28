namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Services;

public class JwtSettings
{
    public string Issuer { get; set; }
    public string Secret { get; set; }
    public int ExpirationInMinutes { get; set; }
}