using FoodMenuWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodMenuWebApp.Data
{
    // ef core allow crud using CLR objects (entities)
    // materialize data returned from database as entity objects
    // track changes that made to the objects
    // handle concurrency
    // propagate object changes back to db
    // bind objects to controls

    // db context interact with data as objects
    public class MenuContext : DbContext
    {
        public MenuContext(DbContextOptions<MenuContext> dbContext) : base(dbContext)
        {

        }

        // when you need to define custom table relationships, constraints, or seed data in EF Core
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite key for join table (DishIngredient)
            // Define primary key for join table (as there are 2 key it is composite key)
            modelBuilder.Entity<DishIngredient>()
                .HasKey(di => new { di.DishId, di.IngredientId });

            // Define relationships (many to many relationship)
            modelBuilder.Entity<DishIngredient>()
                .HasOne(di => di.Dish)
                .WithMany(d => d.DishIngredients)
                .HasForeignKey(di => di.DishId);

            modelBuilder.Entity<DishIngredient>()
                .HasOne(di => di.Ingredient)
                .WithMany(i => i.DishIngredients)
                .HasForeignKey(di => di.IngredientId);

            // Seed data (initial data that is automatically inserted into the database)
            // When it is created or migrated used for populating tables with default values
            modelBuilder.Entity<Dish>().HasData(new Dish
            {
                Id = 1,
                Name = "Pizza",
                Price = 10.99,
                ImageUrl = "https://www.simplyrecipes.com/thmb/KE6iMblr3R2Db6oE8HdyVsFSj2A=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/__opt__aboutcom__coeus__resources__content_migration__simply_recipes__uploads__2019__09__easy-pepperoni-pizza-lead-3-1024x682-583b275444104ef189d693a64df625da.jpg"
            }, new Dish
            {
                Id = 2,
                Name = "Pepperoni",
                Price = 8.5,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/0/0c/Pepperoni_Pizza_%2829204589095%29.jpg"
            }, new Dish
            {
                Id = 3,
                Name = "Vegetarian",
                Price = 7.8,
                ImageUrl = "https://cdn.loveandlemons.com/wp-content/uploads/2023/02/vegetarian-pizza.jpg"
            }
            );

            // Seed data 
            // Used to populate tables with default values
            // it is only created when the database is created or migrated
            modelBuilder.Entity<Ingredient>().HasData(new Ingredient
            {
                Id = 1,
                Name = "Cheese"
            }, new Ingredient
            {
                Id = 2,
                Name = "Tomato"
            }, new Ingredient
            {
                Id = 3,
                Name = "Mushroom"
            }, new Ingredient
            {
                Id = 4,
                Name = "Pepperoni"
            }, new Ingredient
            {
                Id = 5,
                Name = "Paprika"
            });

            // seeding data only insert raw data and it not included the navigation properties
            modelBuilder.Entity<DishIngredient>().HasData(new DishIngredient
            {
                DishId = 1,
                IngredientId = 1
            }, new DishIngredient
            {
                DishId = 1,
                IngredientId = 2
            }, new DishIngredient
            {
                DishId = 1,
                IngredientId = 3
            }, new DishIngredient
            {
                DishId = 2,
                IngredientId = 1
            }, new DishIngredient
            {
                DishId = 2,
                IngredientId = 4
            }, new DishIngredient
            {
                DishId = 3,
                IngredientId = 3
            }, new DishIngredient
            {
                DishId = 3,
                IngredientId = 5
            });

            // Call base implementation
            base.OnModelCreating(modelBuilder);
        }

        // db set represent collections of the entity in the context
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<DishIngredient> DishIngredients { get; set; }
    }
}
