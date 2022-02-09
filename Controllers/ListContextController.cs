using Microsoft.AspNetCore.Mvc;
using MassTransit;
using DataService;
using TokenService;

namespace AuthenticationService.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ListController : ControllerBase
{
    private IDataAccess _data;

    private readonly ILogger<ListController> _logger;
    private readonly IRequestClient<ValidateToken> _validate;
   
    public ListController(ILogger<ListController> logger,IDataAccess data, IRequestClient<ValidateToken> validate)
    {
        _data = data;
        _logger = logger;
        _validate = validate;
     
    }

   [TypeFilter(typeof(AuthenticateAttribute))]
   [HttpPost]
        public async Task<object> GetList(int page, int pageSize)
        {
           
           var result = await _data.GetUsers(page,pageSize);

           // var result = await _db.GetListAsync(request);
           
            return  result;
        }


}
