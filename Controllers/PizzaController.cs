using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        private readonly ILogger<PizzaController> _logger;

        public PizzaController(ILogger<PizzaController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            using var ctx = new PizzeriaContext();

            var pizzas = ctx.Pizzas.ToArray();
            return View(pizzas);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza pizza)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", pizza);
            }
            using var ctx = new PizzeriaContext();
            ctx.Pizzas.Add(pizza);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Detail(int id)
        {
            using var ctx = new PizzeriaContext();

            var pizza = ctx.Pizzas.Single(p => p.Id == id);

            return View(pizza);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            using var ctx = new PizzeriaContext();

            var pizza = ctx.Pizzas.FirstOrDefault(p => p.Id == id);

            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id , Pizza pizza) 
        {
            if (!ModelState.IsValid)
            {
                return View(pizza);
            }
            using var ctx = new PizzeriaContext();

            var pizzaEdit = ctx.Pizzas.FirstOrDefault(p => p.Id == id);

            if(pizzaEdit == null)
            {
                return NotFound();
            }
            pizzaEdit.Name = pizza.Name;
            pizzaEdit.Description = pizza.Description;
            pizzaEdit.Price = pizza.Price;
            pizzaEdit.Foto = pizza.Foto;

            ctx.SaveChanges();

            return RedirectToAction("Index");



        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using var ctx = new PizzeriaContext();

            var pizzaDelete = ctx.Pizzas.FirstOrDefault(p =>p.Id == id);
            if(pizzaDelete == null)
            {
                return NotFound();
            }

            ctx.Pizzas.Remove(pizzaDelete);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}