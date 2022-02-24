
namespace AuthenticationService;
public class Claim {
    public string uid {get;set;}
    public string role {get;set;}
    public string deviceid {get;set;}
    public DateTimeOffset expiration {get;set;}

   
}