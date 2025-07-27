namespace Resturant_System.Middleware
{
    public class BusinessHoursMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<BusinessHoursMiddleware> _logger;

        public BusinessHoursMiddleware(RequestDelegate next, ILogger<BusinessHoursMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var currentHour = DateTime.Now.Hour;
            if (currentHour < 9 || currentHour >= 23)
            {
                await context.Response.WriteAsync(" Access is restricted outside business hours (9 AM - 11 PM).");
                return;
            }

            await _next(context);
        }
    }

}
