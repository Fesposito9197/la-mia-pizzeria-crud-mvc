using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Models
{
    public class PizzeriaContext : IdentityDbContext<IdentityUser>
    {
       

        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Categoria> Categories { get; set; }
        public DbSet<Ingredienti> Ingredientii { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=LocalHost;Initial Catalog=PizzeriaDb;Integrated Security=True;Pooling=False;Encrypt=False;");
        }
        public void seed()
        {
                var pizzaseed = new Pizza[]
                {
                    new Pizza
                    {
                        Name = "Margherita",
                        Description = "La pizza Margherita è la tipica pizza napoletana, condita con pomodoro, mozzarella, basilico fresco, sale e olio;",
                        Foto = "/img/pizza-napoletana.jpg",
                        Price = 5,
                    },
                    new Pizza
                    {
                        Name = "Marinara",
                        Description = "La pizza alla marinara è una tipica pizza napoletana condita con pomodoro, aglio, origano, olio.",
                        Foto = "/img/Pizza_marinara.jpg",
                        Price = 4,
                    },
                    new Pizza
                    {
                        Name = "Capricciosa",
                        Description = "La pizza capricciosa è una pizza tipica della cucina italiana caratterizzata da un condimento di pomodoro, mozzarella, prosciutto cotto, funghi, olive verdi e nere, e carciofini e spesso uova.",
                        Foto ="/img/Pizza_capricciosa.jpg",
                        Price = 8,
                    },
                    new Pizza
                    {
                        Name = "Diavola",
                        Description = "La pizza diavola è una variazione della pizza margherita con l'aggiunta di salame piccante.",
                        Foto = "/img/pizza_diavola.jpg",
                        Price = 7,
                    }
                };
                if (!Pizzas.Any())
                {
                    Pizzas.AddRange(pizzaseed);

                }
                if (!Categories.Any())
                {
                    var seed = new Categoria[]
                    {
                        new Categoria
                        {
                            Name = "Pizze classiche",
                            Pizzas = pizzaseed
                            
                        },
                        new Categoria
                        {
                            Name = "Pizze bianche"
                        },
                        new Categoria
                        {
                            Name = "Pizze vegetariane"
                        },
                        new Categoria
                        {
                            Name = "Pizze di mare"
                        }

                    };
                    Categories.AddRange(seed);


                }
                if (!Ingredientii.Any())
                {
                    var ingredientiseed = new Ingredienti[]
                    {
                        new Ingredienti
                        {
                            Name = "Pomodoro",
                            Pizzas = pizzaseed
                        },
                        new Ingredienti
                        {
                            Name = "Mozzarella"
                        },
                        new Ingredienti
                        {
                            Name = "Basilico"
                        },
                        new Ingredienti
                        {
                            Name = "Origano"
                        },
                        new Ingredienti
                        {
                            Name = "Aglio"
                        }
                    };
                    Ingredientii.AddRange(ingredientiseed);
                    SaveChanges();
                }
        }
    }
}
