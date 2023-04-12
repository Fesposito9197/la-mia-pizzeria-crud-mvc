using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Models
{
    public class PizzeriaContext : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=LocalHost;Initial Catalog=PizzeriaDb;Integrated Security=True;Pooling=False;Encrypt=False;");
        }
        public void seed()
        {
            if (!Pizzas.Any())
            {
                var seed = new Pizza[]
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
                Pizzas.AddRange(seed);

                SaveChanges();
            }
        }
    }
}
