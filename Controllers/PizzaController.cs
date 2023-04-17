using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace la_mia_pizzeria_static.Controllers
{
    [Authorize(Roles ="ADMIN,USER")]
    public class PizzaController : Controller
    {
        private readonly ILogger<PizzaController> _logger;

        
        public PizzaController(ILogger<PizzaController> logger)
        {
            _logger = logger;
           
        }


        [Authorize(Roles = "ADMIN,USER")]
        public IActionResult Index()
        {
            using var ctx = new PizzeriaContext();

            var pizzas = ctx.Pizzas.ToArray();
            return View(pizzas);
        }


        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult Create()
        {
            using var ctx = new PizzeriaContext();
            List<Categoria> categories = ctx.Categories.ToList();

            PizzaFormModel model = new PizzaFormModel();

            List<SelectListItem> listIngrendienti = new List<SelectListItem>();

            foreach (Ingredienti ingredienti in ctx.Ingredientii)
            {
                listIngrendienti.Add(new SelectListItem()
                { Text = ingredienti.Name, Value = ingredienti.Id.ToString() });
            }
            model.Pizza = new Pizza();
            model.Categories = categories;
            model.Ingredientii = listIngrendienti;
            return View("Create", model);
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
                    List<Ingredienti> ingrendientii = ctx.Ingredientii.ToList();
                    List<SelectListItem> listIngrendientii = new List<SelectListItem>();
                    foreach (Ingredienti ingredienti in ingrendientii)
                    {
                        listIngrendientii.Add(
                            new SelectListItem()
                            { Text = ingredienti.Name, Value = ingredienti.Id.ToString() });
                    }
                    List<Categoria> categories = ctx.Categories.ToList();
                    data.Categories = categories;
                    data.Ingredientii = listIngrendientii;
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
                if (data.SelectedIngredientii != null)
                {
                    foreach (string selectedIngredientiId in data.SelectedIngredientii)
                    {
                        int selectedIntIngredientiId = int.Parse(selectedIngredientiId);
                        Ingredienti? ingredienti = ctx.Ingredientii
                                                   .Where(m => m.Id == selectedIntIngredientiId)
                                                   .FirstOrDefault();
                        pizzaToCreate.Ingredientii.Add(ingredienti);

                    }
                }
                ctx.Pizzas.Add(pizzaToCreate);
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }
        }


        [Authorize(Roles = "ADMIN,USER")]
        public IActionResult Detail(int id)
        {
            using var ctx = new PizzeriaContext();


            var pizza = ctx.Pizzas
                .Include(p => p.Categoria)
                .Include(p => p.Ingredientii)
                .SingleOrDefault(p => p.Id == id);

            if (pizza == null)
            {
                return NotFound();
            }
            
          
            return View( pizza);
        }


        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult Update(int id)
        {


            using PizzeriaContext ctx = new PizzeriaContext();
            //Pizza? pizzaToUpdate = ctx.Pizzas.Where(p => p.Id == id).Include("Ingredientii").FirstOrDefault();
            Pizza? pizzaToUpdate = ctx.Pizzas.Where(p => p.Id == id).Include(p => p.Ingredientii).FirstOrDefault();


            if (pizzaToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                PizzaFormModel model = new PizzaFormModel();
                List<Categoria> categories = ctx.Categories.ToList();
                List<Ingredienti> ingredientii = ctx.Ingredientii.ToList();
                List<SelectListItem> listingredienti = new List<SelectListItem>();
                foreach (Ingredienti ingredienti in ingredientii)
                {
                    listingredienti.Add(
                        new SelectListItem()
                        {
                            Text = ingredienti.Name,
                            Value = ingredienti.Id.ToString(),
                            Selected = pizzaToUpdate.Ingredientii.Any(m => m.Id == ingredienti.Id)
                        });
                }
               
                model.Pizza = pizzaToUpdate;
                model.Categories = categories;
                model.Ingredientii = listingredienti;
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
                List<Ingredienti> ingredientii = context.Ingredientii.ToList();
                List<SelectListItem> listIngredienti = new List<SelectListItem>();
                foreach (Ingredienti ingredienti in ingredientii)
                {
                    listIngredienti.Add(
                        new SelectListItem()
                        { Text = ingredienti.Name , Value = ingredienti.Id.ToString() });
                }
                data.Ingredientii = listIngredienti;
                data.Categories = categories;
                return View("Update", data);
            }
            using PizzeriaContext ctx = new PizzeriaContext();

            Pizza? pizzaEdit = ctx.Pizzas.Where(p => p.Id == id).Include(p => p.Ingredientii).FirstOrDefault();

            if (pizzaEdit == null)
            {
                return NotFound();
            }
            pizzaEdit.Name = data.Pizza.Name;
            pizzaEdit.Description = data.Pizza.Description;
            pizzaEdit.Price = data.Pizza.Price;
            pizzaEdit.Foto = data.Pizza.Foto;
            pizzaEdit.CategoriaId = data.Pizza.CategoriaId;
            if (data.SelectedIngredientii != null)
            {
                pizzaEdit.Ingredientii.Clear(); 
                foreach (string selectedIngredienteId in data.SelectedIngredientii)
                {
                    int selectedIntIngredienteId = int.Parse(selectedIngredienteId);
                    Ingredienti? ingredienti = ctx.Ingredientii
                                              .Where(m => m.Id == selectedIntIngredienteId)
                                              .FirstOrDefault();
                    pizzaEdit.Ingredientii.Add(ingredienti);
                }
            }

            ctx.SaveChanges();

            return RedirectToAction("Index");



        }



        [Authorize(Roles = "ADMIN")]
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