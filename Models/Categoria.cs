using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        [StringLength(50 , ErrorMessage = "Il nome non puo avere piu di 30 caratteri")]
        public string Name { get; set; } = string.Empty;

        public List<Pizza>? Pizzas { get; set; } 
    }
}
