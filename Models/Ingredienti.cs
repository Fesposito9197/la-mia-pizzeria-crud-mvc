using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Ingredienti
    {
        public int Id { get; set; }



        [Required(ErrorMessage = "Campo obbligatorio")]
        [StringLength(50, ErrorMessage = "Il nome non puo avere piu di 50 caratteri")]
        public string Name { get; set; } = string.Empty;

        public IEnumerable<Pizza>? Pizzas { get; set; }
    }
}
