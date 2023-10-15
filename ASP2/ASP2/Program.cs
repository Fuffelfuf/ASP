using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("conf.json")
    .AddXmlFile("conf.xml")
    .AddIniFile("conf.ini")
    .AddJsonFile("user.json");

var app = builder.Build();
var company1 = new Company();
var company2 = new Company();
var company3 = new Company();


app.Configuration.GetSection("CompanyJson").Bind(company1);
app.Configuration.GetSection("CompanyXml").Bind(company2);
app.Configuration.GetSection("CompanyIni").Bind(company3);
var user = app.Configuration.GetSection("User");
int maxEmloyees=Math.Max(company1.Employees, Math.Max(company2.Employees, company3.Employees));
string maxCompany = "";
if (maxEmloyees == company1.Employees) { maxCompany = company1.Name; }
else if (maxEmloyees == company2.Employees) { maxCompany = company2.Name; } 
else { maxCompany = company3.Name; }
app.Run(async (context) =>
{
    await context.Response.WriteAsync($"{company1.Name} - {company1.Employees}\n");
    await context.Response.WriteAsync($"{company2.Name} - {company2.Employees}\n");
    await context.Response.WriteAsync($"{company3.Name} - {company3.Employees}\n");
    await context.Response.WriteAsync($"Company with most eployees is {maxCompany}\n");
    await context.Response.WriteAsync($" Name-{user["Name"]}\n Age-{user["Age"]}\n Gender-{user["Gender"]}\n");
});

app.Run();
public class Company
{
    public string Name { get; set; } = "";
    public int Employees { get; set; } = 0;
}