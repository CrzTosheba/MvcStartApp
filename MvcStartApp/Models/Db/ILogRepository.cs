namespace MvcStartApp.Models.Db
{
    public interface ILogRepository
    {
        Task AddRequest(Request request);
        Task<Request[]> GetRequests();
    }
}
