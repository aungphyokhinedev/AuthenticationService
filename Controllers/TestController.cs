using Microsoft.AspNetCore.Mvc;


namespace AuthenticationService.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class TestController : ControllerBase
{
    private IDataAccess _data;
    private IAuthenticate _auth;
    private readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger, IAuthenticate auth, IDataAccess data)
    {
        _data = data;
        _logger = logger;
        _auth = auth;

    }
    [HttpPost]
    public async Task<object> Login(string uid)
    {
        //getting token and set to response for login
        var token = await _auth.getTokenAsync(uid, Roles.Consumer.ToString());
        HttpContext.Response.setToken(token);

        return "token set in response header!";
    }

    //can add any roles sperated by comman
    //can initialize this in constructor also
    private const string roles = Roles.Consumer + "," + "Admin";

    [Allowed(roles)]
    //for all roles use *
    //[Allowed("*")]
    [HttpPost]
    public async Task<object> GetList(int page, int pageSize)
    {
        //getting claim from auth filter
        var claim = HttpContext.Claim();


        var result = await _data.GetUsers(page, pageSize);
        return result;
    }


}
