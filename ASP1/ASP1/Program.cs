
using System;

Company company = new("Intel", 345);
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.Run(async (context) =>
{
var path = context.Request.Path;

if (path == "/company")
    await context.Response.WriteAsync(company.ToString());
    else
    {
        Random rnd = new Random();
        await context.Response.WriteAsync((rnd.Next(101)).ToString());
    }
}
);
app.Run();

public class Company
{
    public string Name;
    public int Id;
    public Company(string Name,int Id) {
        this.Name = Name;
        this.Id = Id;
    }
    public override string ToString()
    {
        return $"ID: {Id} Name: {Name}";
    }
}

