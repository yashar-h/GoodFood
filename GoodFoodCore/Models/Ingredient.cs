using GoodFoodCore.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodFoodCore.Models
{
    public class Ingredient : Entity
    {
        [MaxLength(90)]
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }

        public Ingredient() { }

        public Ingredient(string title, string description, string slug)
        {
            Title = title;
            Description = description;
            Slug = slug;
        }


    }
}
