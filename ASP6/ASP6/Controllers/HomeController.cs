using ASP6.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP6.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.Age >= 16)
                {
                    return View("OrderView");
                }
                else
                {
                    return Content("Вік має бути 16 або більше.");
                }
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult OrderCreate([Bind("Amount")] OrderAmount orderAmount)
        {
            if (orderAmount.Amount > 0) { return View("OrderCreate", orderAmount); }
            else{ return Content("Кількість замовлень має бути більше 0."); }
        }

        [HttpPost]
        public IActionResult OrderSummary(List<OrderModel> orders)
        {

            return View("OrderSummary", orders);
        }
    }
}