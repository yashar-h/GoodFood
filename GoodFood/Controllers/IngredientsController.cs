﻿using System.Linq;
using System.Threading.Tasks;
using GoodFoodCore.AppServices.Ingredients;
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

        public async Task<IActionResult> Index()
        {
            var ingredients = await _dispatcher.Dispatch(new GetIngredientsQuery());

            var ingredientViewModels = ingredients.Select(i => new IngredientViewModel(i));

            return View(ingredientViewModels);
        }

        public async Task<ActionResult> Details(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                return NotFound();
            }

            var ingredient = await _dispatcher.Dispatch(new GetIngredientQuery(slug));

            if (ingredient == null)
            {
                return NotFound();
            }

            return View(new IngredientViewModel(ingredient));
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: Ingredients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Slug,Title,Description")] IngredientViewModel ingredient)
        {
            if (ModelState.IsValid)
            {
                var result = await _dispatcher.Dispatch(new AddIngredientCommand
                    (ingredient.Title, ingredient.Description, ingredient.Slug));

                if (result.IsFailure)
                {
                    ModelState.AddModelError("", result.Error);
                    return View(ingredient);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(ingredient);
        }

        public async Task<ActionResult> Edit(string slug)
        {
            if (slug == null)
            {
                return NotFound();
            }

            var ingredient = await _dispatcher.Dispatch(new GetIngredientQuery(slug));
            if (ingredient == null)
            {
                return NotFound();
            }

            return View(new IngredientViewModel(ingredient));
        }

        // POST: Recipes/Edit/{slug}
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string slug, [Bind("Slug,Title,Description")] IngredientViewModel ingredient)
        {
            if (slug != ingredient.Slug)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _dispatcher.Dispatch(new UpdateIngredientCommand
                    (ingredient.Title, ingredient.Description, ingredient.Slug));

                if (result.IsFailure)
                {
                    ModelState.AddModelError("", result.Error);
                    return View(ingredient);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(ingredient);
        }

        public async Task<IActionResult> Delete(string slug, bool? saveChangesError = false)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                return NotFound();
            }

            var ingredient = await _dispatcher.Dispatch(new GetIngredientQuery(slug));
            if (ingredient == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(new IngredientViewModel(ingredient));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string slug)
        {
            var ingredient = await _dispatcher.Dispatch(new GetIngredientQuery(slug));
            if (ingredient == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var result = await _dispatcher.Dispatch(new DeleteIngredientCommand(slug));

            if (result.IsFailure)
            {
                ModelState.AddModelError("", result.Error);
                return RedirectToAction(nameof(Delete), new { slug, saveChangesError = true });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
