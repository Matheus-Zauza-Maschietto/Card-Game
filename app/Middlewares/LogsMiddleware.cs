using System.Security.Claims;
using app.DTOs;
using app.Services;

namespace app.Middlewares;

public class LogsMiddleware : IMiddleware
{
    private readonly KafkaService _kafkaService;

    public LogsMiddleware(KafkaService kafkaService)
    {
        _kafkaService = kafkaService;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.Request.HttpContext.User.Identity.IsAuthenticated)
        {
            _kafkaService.SendLog(new LogsKafkaMessage()
            {
                UserEmail = context?.User?.FindFirstValue(ClaimTypes.Email),
                HttpMethod = context.Request.Method,
                Url = context.Request.Path,
            });
        }
        await next(context);
    }
}