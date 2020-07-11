using GoodFoodCore.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodFoodCore.Models
{
    public class Recipe : AggregateRoot
    {
        [MaxLength(90)]
        public string Title { get; set; }
        public string Description { get; set; }
        public RecipeCategory Category { get; set; }
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }

        public enum RecipeCategory
        {
            Starters = 0,
            MainCourse = 1,
            Dessert = 2
        }

        public Recipe() { }

        public Recipe(string title, string description, string slug, RecipeCategory category)
        {
            Title = title;
            Description = description;
            Slug = slug;
            Category = category;
        }
    }
}
