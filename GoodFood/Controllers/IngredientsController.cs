using System.Linq;
using GoodFoodCore.AppServices;
using GoodFoodCore.Utils;
using GoodFoodWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoodFoodWeb.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly Dispatcher _dispatcher;

        public IngredientsController(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public IActionResult Index()
        {
            var ingredients = _dispatcher.Dispatch(new GetIngredientsQuery());
            return View(ingredients);
        }

        public ActionResult Details(string slug)
        {
            var ingredient = _dispatcher.Dispatch(new GetIngredientsQuery()).First(i => i.Slug == slug);
            IngredientViewModel ingredientDetailViewModel = new IngredientViewModel(ingredient);

            return View(ingredientDetailViewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(IngredientViewModel ingredient)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = "Model is invalid" });
            }

            var result = _dispatcher.Dispatch(new AddIngredientCommand
                (ingredient.Title, ingredient.Description, ingredient.Slug));

            if (result.IsFailure)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = result.Error });
            }

            return RedirectToAction(nameof(Details), new { slug = ingredient.Slug });
        }

        public ActionResult Edit(string slug)
        {
            var ingredient = _dispatcher.Dispatch(new GetIngredientQuery(slug));
            return View(new IngredientViewModel(ingredient));
        }

        [HttpPost]
        public IActionResult Edit(IngredientViewModel ingredient)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = "Model is invalid" });
            }

            var result = _dispatcher.Dispatch(new UpdateIngredientCommand
                (ingredient.Title, ingredient.Description, ingredient.Slug));

            if (result.IsFailure)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = result.Error });
            }

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(string slug)
        {
            var result = _dispatcher.Dispatch(new DeleteIngredientCommand(slug));
            if (result.IsFailure)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = result.Error });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
