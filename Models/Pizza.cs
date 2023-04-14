using la_mia_pizzeria_static.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {

        public int Id { get; set; }



        [Required(ErrorMessage = "Campo obbligatorio")]
        [StringLength(30, ErrorMessage = "Il nome non puo avere piu di 30 caratteri")]
        public string Name { get; set; } = string.Empty;



        [Required(ErrorMessage = "Campo obbligatorio")]
        [Column(TypeName = "text")]
        [MoreThanAWorld(5)]
        public string Description { get; set; } = string.Empty;



        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Foto { get; set; } = string.Empty;



        [Required(ErrorMessage = "Campo obbligatorio")]
        //[Range(1, 200, ErrorMessage = "Il prezzo non puo essere inferiore a 0")]
        [MoreThanZeroPrice]
        public int Price { get; set; }

        
        public int? CategoriaId { get; set; }
        public Categoria? Categoria { get; set;}

        public List<Ingredienti>? Ingredientii { get; set; }


        public Pizza() { 
            this.Ingredientii = new List<Ingredienti>();
        }
    }
}
