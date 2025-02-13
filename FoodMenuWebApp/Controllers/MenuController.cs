using FoodMenuWebApp.Data;
using FoodMenuWebApp.Models;
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

        // be default the route endpoint is /Menu/Index
        public async Task<IActionResult> Index(string searchString)
        {
            var dishes = from d in _context.Dishes
                       select d;
            if (!string.IsNullOrEmpty(searchString))
            {
                dishes = dishes.Where(d => d.Name.Contains(searchString));
                return View(await dishes.ToListAsync());
            }
            //var dishes = await _context.Dishes.ToListAsync<Dish>();
            return View(await dishes.ToListAsync());
        }

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
