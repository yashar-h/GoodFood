using GoodFoodCore.Common;
using System;

namespace GoodFoodCore
{
    public class Ingredient : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public Ingredient(string title, string description, string slug)
        {
            Title = title;
            Description = description;
            Slug = slug;
        }


    }
}
