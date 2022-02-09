using DataService;

public interface IDataAccess {
     Task<ListResponse> GetUsers(int page, int pageSize);
}