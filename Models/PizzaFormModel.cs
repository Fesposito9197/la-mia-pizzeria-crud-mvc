using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria_static.Models
{
    public class PizzaFormModel
    {
        public Pizza Pizza { get; set; }
        public List<Categoria>? Categories { get; set; }

        public List<SelectListItem>? Ingredientii { get; set; }
        public List<string>? SelectedIngredientii { get; set;}
    }
}
