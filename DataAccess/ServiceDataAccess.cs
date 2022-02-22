using DataService;
using MassTransit;
namespace AuthenticationService.DataAccess
{
    public class ServiceDataAccess : IDataAccess
    {
        //for dataservice
        private IRequestClient<DataServiceContract> _client;



        public ServiceDataAccess(IRequestClient<DataServiceContract> client)
        {
            _client = client;

        }

        public async Task<ListResponse> GetUsers(int page, int pageSize)
        {

            var contract = new Query("users").Select("name,id").Page(page).Limit(pageSize).Contract();
            var result = await _client.GetResponse<ListData>(contract);

            return result.Message.response;
        }
    }
}