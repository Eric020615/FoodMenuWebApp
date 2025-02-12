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
        public async Task<IActionResult> Index()
        {
            var dishes = await _context.Dishes.ToListAsync<Dish>();
            return View(dishes);
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
