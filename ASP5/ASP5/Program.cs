using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseStatusCodePages(async statusCodeContext =>
    {
        var response = statusCodeContext.HttpContext.Response;
        var path = statusCodeContext.HttpContext.Request.Path;
        response.ContentType = "text/plain; charset=UTF-8";

        if (response.StatusCode == 403)
        {
            var logger = statusCodeContext.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogError($"Access Denied for {path}");
            await response.WriteAsync($"Path: {path}. Access Denied ");
        }
        else if (response.StatusCode == 404)
        {
            var logger = statusCodeContext.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogError($"Resource Not Found for {path}");
            await response.WriteAsync($"Resource {path} Not Found");
        }
    });

    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            var exceptionHandlerPathFeature =
                context.Features.Get<IExceptionHandlerPathFeature>();

            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogError($"Error: {exceptionHandlerPathFeature.Error}");

            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Error 500");
        });
    });
}
app.MapGet("/", (context) =>
{
    return context.Response.WriteAsync(@"
       <!DOCTYPE html>
        <html lang=""en"">
    <head>
    <meta charset=""UTF-8"">
    </head>
    <body>
    <h1>Форма</h1>
    
    <form id=""dataForm"">
        <label for=""valueInput"">Значення:</label>
        <input type=""text"" id=""valueInput"" name=""value"" required>
        
        <label for=""dateTimeInput"">Дата та час:</label>
        <input type=""datetime-local"" id=""dateTimeInput"" name=""datetime"" required>
        
        <button type=""button"" onclick=""submitForm()"">Відправити</button>
    </form>

    <script>
        function submitForm() {
            var value = document.getElementById('valueInput').value;
            var datetime = document.getElementById('dateTimeInput').value;
            console.log('Значення:', value);
            console.log('Дата та час:', datetime);
            document.cookie = `value=${value}; expires=${datetime}`;
            document.cookie = `datetime=${datetime}; expires=${datetime}`;
        }
    </script>
    </body>
    </html>");
});

app.MapPost("/submit", (HttpContext context) =>
{
    var value = context.Request.Cookies["value"];
    var datetime = context.Request.Cookies["datetime"];
    Console.WriteLine($"Recieved Data: Value = {value}, date-time = {datetime}");
    return context.Response.WriteAsync($"Recieved Data: Value = {value}, date-time = {datetime}");
});
app.MapGet("/coockie", async context =>
{
    var valueCookie = context.Request.Cookies["value"];
    var dateTimeCookie = context.Request.Cookies["dateTime"];

    string result = "No values in coockies";

    if (valueCookie != null && dateTimeCookie != null)
    {
        result = $"Coockies: Value = {valueCookie}, date-time = {dateTimeCookie}";
    }

    await context.Response.WriteAsync(result);
});

app.Run();
