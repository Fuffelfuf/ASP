using ASP8.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASP8.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var productslist = CreateList();
            return View(productslist);
        }
        public List<Product> CreateList()
        {
            var products = new List<Product>
        {
            new Product { ID = 1, Name = "Product 1", Price = 18.8m, CreatedDate = new DateTime(2023, 11, 18) },
            new Product { ID = 2, Name = "Product 2", Price = 34.5m, CreatedDate = new DateTime(2023, 12, 11) },
            new Product { ID = 3, Name = "Product 3", Price = 31.3m, CreatedDate = new DateTime(2023, 9, 16) }
        };
            return products;
        }
    }
}