using GoodFoodCore.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodFoodCore.Models
{
    public class Recipe : AggregateRoot
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public RecipeCategory Category { get; set; }
        public List<RecipeIngredient> Ingredients { get; set; }

        public enum RecipeCategory
        {
            Starters = 0,
            Main_Course = 1,
            Dessert = 2
        }
    }
}
