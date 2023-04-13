using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            using(var ctx = new PizzeriaContext())
            {
                List<Categoria> categories = ctx.Categories.ToList();

                PizzaFormModel model = new PizzaFormModel();

                model.Pizza = new Pizza();
                model.Categories = categories;
                return View("Create", model );
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel data)
        {
             var ctx = new PizzeriaContext();

            if (!ModelState.IsValid)
            {
                using ( ctx = new PizzeriaContext())
                {
                    List<Categoria> categories = ctx.Categories.ToList();
                    data.Categories = categories;
                    return View("Create", data);
                }
            }
            using (ctx = new PizzeriaContext())
            {
                Pizza pizzaToCreate = new Pizza();
                pizzaToCreate.Name = data.Pizza.Name;
                pizzaToCreate.Description = data.Pizza.Description;
                pizzaToCreate.Foto = data.Pizza.Foto;
                pizzaToCreate.Price = data.Pizza.Price;
                pizzaToCreate.CategoriaId = data.Pizza.CategoriaId;
                ctx.Pizzas.Add(pizzaToCreate);
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public IActionResult Detail(int id)
        {
            using var ctx = new PizzeriaContext();


            var pizza = ctx.Pizzas
                .Include(p => p.Categoria)
                .SingleOrDefault(p => p.Id == id);

            if (pizza == null)
            {
                return NotFound();
            }
            
          
            return View( pizza);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {


            using PizzeriaContext ctx = new PizzeriaContext();
            Pizza? pizzaToUpdate = ctx.Pizzas.FirstOrDefault(p => p.Id == id);
            if (pizzaToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                List<Categoria> categories = ctx.Categories.ToList();
                PizzaFormModel model = new PizzaFormModel();
                model.Pizza = pizzaToUpdate;
                model.Categories = categories;

                return View(model);
            }




        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id , PizzaFormModel data) 
        {
            if (!ModelState.IsValid)
            {
                using PizzeriaContext context = new PizzeriaContext();
                List<Categoria> categories = context.Categories.ToList();
                data.Categories = categories;
                return View("Update", data);
            }
            using PizzeriaContext ctx = new PizzeriaContext();

            var pizzaEdit = ctx.Pizzas.FirstOrDefault(p => p.Id == id);

            if (pizzaEdit == null)
            {
                return NotFound();
            }
            pizzaEdit.Name = data.Pizza.Name;
            pizzaEdit.Description = data.Pizza.Description;
            pizzaEdit.Price = data.Pizza.Price;
            pizzaEdit.Foto = data.Pizza.Foto;
            pizzaEdit.CategoriaId = data.Pizza.CategoriaId;

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