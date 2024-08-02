using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Core_Web_API
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            using (StreamWriter writetext = new StreamWriter("Log.txt"))
            {
                writetext.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
            }
            //Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");

            //Call the next middleware in the pipeline
            await _next(context);

            //using (StreamWriter writetext = new StreamWriter("Log.txt"))
            //{
            //    writetext.WriteLine($"Response: {context.Response.StatusCode}");
            //}
            //Console.WriteLine($"Response: {context.Response.StatusCode}");
        }
    }
}
