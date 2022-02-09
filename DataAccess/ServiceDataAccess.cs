using DataService;
using MassTransit;
namespace AuthenticationService.DataAccess
{
    public class ServiceDataAccess : IDataAccess
    {
        //for dataservice
        private IRequestClient<GetList> _list;
        private IRequestClient<AddData> _add;
        private IRequestClient<RemoveData> _remove;
        private IRequestClient<UpdateData> _update;

        

        public ServiceDataAccess(IRequestClient<RemoveData> remove,IRequestClient<GetList> list, IRequestClient<AddData> add,IRequestClient<UpdateData> update)
        {
            _list = list;
            _add = add;
            _remove = remove;
            _update = update;

        }

        public async Task<ListResponse> GetUsers(int page, int pageSize)
        {
            var request = new GetRequest{
                tables="users", pageSize= pageSize, page= page,
                fields = "id,nrc,mobile_no,createdat",
                orderBy = "id desc",
                /*filter = new Filter{
                    where = "id = 4",
                    parameters = new Dictionary<string, object>{
                        {"id" , 4 }
                    }.toParameterList()
                }*/
            };
           
           var result = await _list.GetResponse<ListData>(new {request=request});

           // var result = await _db.GetListAsync(request);
           
            return  result.Message.response;
        }
    }
}