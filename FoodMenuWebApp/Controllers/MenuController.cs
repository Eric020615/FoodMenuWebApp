using FoodMenuWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodMenuWebApp.Controllers
{
    // this controller acts as front controller to handle all route to menu
    public class MenuController : Controller
    {
        private readonly MenuContext _context;
        public MenuController(MenuContext context)
        {
            _context = context;
        }

        // The Index method handles the route: /Menu/Index
        // - The "searchString" parameter is NOT part of the route template.
        // - ASP.NET Core automatically treats it as a query parameter.
        // - Example URL: /Menu/Index?searchString=pizza
        public async Task<IActionResult> Index(string searchString)
        {
            var dishes = from d in _context.Dishes select d;
            if (!string.IsNullOrEmpty(searchString))
            {
                dishes = dishes.Where(d => d.Name.Contains(searchString));
                return View(await dishes.ToListAsync());
            }
            //var dishes = await _context.Dishes.ToListAsync<Dish>();
            return View(await dishes.ToListAsync());
        }

        // The Details method handles the route: /Menu/Details/{id}
        // - By default, ASP.NET Core follows the route pattern: {controller}/{action}/{id?}
        // - Since "id" matches this pattern, ASP.NET Core treats it as a path variable instead of a query parameter.
        // - Example URL: /Menu/Details/5 (id is passed as a route parameter)
        public async Task<IActionResult> Details(int? id)
        {
            var dish = await _context.Dishes
                .Include(di => di.DishIngredients)
                .ThenInclude(i => i.Ingredient)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }
    }
}
