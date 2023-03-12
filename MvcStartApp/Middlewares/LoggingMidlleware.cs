using MvcStartApp.Models.Db;

namespace MvcStartApp.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        

        /// <summary>
        ///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
        /// </summary>
        public LoggingMiddleware(RequestDelegate next)  //, LogRepository repo
        {
            _next = next;
      
        }

        private async Task LogDb(HttpContext context, ILogRepository _repo)
        {
            Request newRequest = new Request();
            newRequest.Url = context.Request.Host.Value + context.Request.Path;
            await _repo.AddRequest(newRequest);
        }

        private void LogConsole(HttpContext context)
        {
            // Для логирования данных о запросе используем свойста объекта HttpContext
            System.Diagnostics.Debug.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");
        }
                /// <summary>
        ///  Необходимо реализовать метод Invoke  или InvokeAsync
        /// </summary>
        public async Task InvokeAsync(HttpContext context, ILogRepository repo)
        {
            LogConsole(context);
      
            await LogDb(context, repo);

            // Передача запроса далее по конвейеру
            await _next.Invoke(context);
        }
    }
}
