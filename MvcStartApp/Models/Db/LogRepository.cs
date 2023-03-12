using Microsoft.EntityFrameworkCore;

namespace MvcStartApp.Models.Db
{
    public class LogRepository : ILogRepository
    {
        // ссылка на контекст
        private readonly BlogContext _context;

        // Метод-конструктор для инициализации
        public LogRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task AddRequest(Request request)
        {
            request.Id = Guid.NewGuid();
            request.Date = DateTime.Now;

            // Добавление 
            var entry = _context.Entry(request);
            if (entry.State == EntityState.Detached)
                await _context.Requests.AddAsync(request);

            // Сохранение изенений
            await _context.SaveChangesAsync();
        }

        public async Task<Request[]> GetRequests()
        {
            return await _context.Requests.OrderByDescending(o => o.Date).ToArrayAsync();
        }
    }
}
