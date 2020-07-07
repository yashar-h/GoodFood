using GoodFoodCore;
using System.ComponentModel.DataAnnotations;

namespace GoodFoodWeb.Models
{
    public class IngredientViewModel
    {
        [Required]
        [MaxLength(90,ErrorMessage = "Max length is 90 characters.")]
        public string Title { get; set; }

        public string Description { get; set; }
        
        [Required]
        [MaxLength(50, ErrorMessage = "Max length is 50 characters.")]
        public string Slug { get; set; }

        public IngredientViewModel()
        {
        }

        public IngredientViewModel(Ingredient ingredient)
        {
            Title = ingredient.Title;
            Description = ingredient.Description;
            Slug = ingredient.Slug;
        }
    }
}
