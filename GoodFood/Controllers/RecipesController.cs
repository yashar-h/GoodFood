using System.Linq;
using System.Threading.Tasks;
using GoodFoodCore.AppServices.Recipes;
using GoodFoodCore.Utils;
using GoodFoodWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoodFoodWeb.Controllers
{
    public class RecipesController : Controller
    {
        private readonly Dispatcher _dispatcher;

        public RecipesController(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        // GET: Recipes
        public async Task<IActionResult> Index()
        {
            var recipes = await _dispatcher.Dispatch(new GetRecipesQuery());

            var recipeViewModels = recipes.Select(recipe => new RecipeViewModel(recipe));

            return View(recipeViewModels);
        }

        // GET: Recipes/Details/{slug}
        public async Task<IActionResult> Details(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                return NotFound();
            }

            var recipe = await _dispatcher.Dispatch(new GetRecipeQuery(slug));

            if (recipe == null)
            {
                return NotFound();
            }

            return View(new RecipeViewModel(recipe));
        }

        // GET: Recipes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Slug,Title,Description,Category")] RecipeViewModel recipe)
        {
            if (ModelState.IsValid)
            {
                var result = await _dispatcher.Dispatch(new AddRecipeCommand(recipe.Title, recipe.Description, recipe.Slug,
                    recipe.Category));

                if (result.IsFailure)
                {
                    ModelState.AddModelError("", result.Error);
                    return View(recipe);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(recipe);
        }

        // GET: Recipes/Edit/{slug}
        public async Task<IActionResult> Edit(string slug)
        {
            if (slug == null)
            {
                return NotFound();
            }

            var recipe = await _dispatcher.Dispatch(new GetRecipeQuery(slug));
            if (recipe == null)
            {
                return NotFound();
            }

            return View(new RecipeViewModel(recipe));
        }

        // POST: Recipes/Edit/{slug}
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string slug, [Bind("Slug,Title,Description,Category")] RecipeViewModel recipe)
        {
            if (slug != recipe.Slug)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _dispatcher.Dispatch(new UpdateRecipeCommand(recipe.ToDomainModel()));

                if (result.IsFailure)
                {
                    ModelState.AddModelError("", result.Error);
                    return View(recipe);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(recipe);
        }

        // GET: Recipes/Delete/{slug}
        public async Task<IActionResult> Delete(string slug, bool? saveChangesError = false)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                return NotFound();
            }

            var recipe = await _dispatcher.Dispatch(new GetRecipeQuery(slug));
            if (recipe == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(new RecipeViewModel(recipe));
        }

        // POST: Recipes/Delete/{slug}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string slug)
        {
            var recipe = await _dispatcher.Dispatch(new GetRecipeQuery(slug));
            if (recipe == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var result = await _dispatcher.Dispatch(new DeleteRecipeCommand(slug));

            if (result.IsFailure)
            {
                ModelState.AddModelError("", result.Error);
                return RedirectToAction(nameof(Delete), new { slug, saveChangesError = true });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
