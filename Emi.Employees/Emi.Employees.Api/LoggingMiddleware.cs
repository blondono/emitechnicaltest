using System.Text;

namespace Emi.Employees.App;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _logFilePath;

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
        _logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "request_log.txt");
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await LogRequestToFileAsync(context);
        await _next(context);
    }

    private async Task LogRequestToFileAsync(HttpContext context)
    {
        var logMessage = $"[{DateTime.UtcNow}] Incoming request: {context.Request.Method} {context.Request.Path}\n";

        logMessage += "Request Headers:\n";
        foreach (var (key, value) in context.Request.Headers)
        {
            logMessage += $"{key}: {value}\n";
        }
        logMessage += await ReadRequestBodyAsync(context.Request);
        await WriteLogToFileAsync(logMessage);
    }

    private async Task<string> ReadRequestBodyAsync(HttpRequest request)
    {
        request.EnableBuffering();

        using (var reader = new StreamReader(request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, bufferSize: 1024, leaveOpen: true))
        {
            var requestBody = await reader.ReadToEndAsync();
            request.Body.Position = 0;
            return $"Request Body: {requestBody}\n";
        }
    }

    private async Task WriteLogToFileAsync(string logMessage)
    {
        try
        {
            await using (var writer = new StreamWriter(_logFilePath, append: true))
            {
                await writer.WriteAsync(logMessage);
                await writer.FlushAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to log file: {ex.Message}");
        }
    }
}
