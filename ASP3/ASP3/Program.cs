using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<CalcService>();
builder.Services.AddTransient<Clock>();
var app = builder.Build();
app.Run(async (context) =>
{
    CalculatorController calculator=new CalculatorController(app.Services.GetRequiredService<CalcService>());
    await context.Response.WriteAsync($"5+11={calculator.add(5,11)}\n");
    await context.Response.WriteAsync($"The time is {app.Services.GetRequiredService<Clock>().WhatTime()}");
});
app.Run();
public class CalculatorController: CalcService
{
    private readonly CalcService calcService;

    public CalculatorController(CalcService calcService)
    {
        this.calcService = calcService;
    }
    public int add(int a,int b)=>calcService.Add(a,b);
    public int sub(int a, int b) => calcService.Subtract(a, b);
    public int mult(int a, int b) => calcService.Multiply(a, b);
    public int div(int a, int b) => calcService.Divide(a, b);


}
public class CalcService
{
    public int Add(int a, int b) { return a + b; }
    public int Subtract(int a, int b) { return a - b; }
    public int Multiply(int a, int b) { return a * b; }
    public int Divide(int a, int b) { if (b != 0) return a / b; else return 0; }
}
public class Clock
{
    public string WhatTime()
    {
        var currentTime = DateTime.Now;
        if (currentTime.Hour >= 0 && currentTime.Hour < 6) { return "night"; }
        else
        if (currentTime.Hour >= 6 && currentTime.Hour < 12) { return "morning"; }
        else
        if (currentTime.Hour >= 12 && currentTime.Hour < 18) { return "day"; }
        else
        { return "evening"; }

    }
}
