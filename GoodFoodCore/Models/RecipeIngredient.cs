using GoodFoodCore.Common;

namespace GoodFoodCore.Models
{
    public class RecipeIngredient : ValueObject<RecipeIngredient>
    {
        public int ID { get; set; }
        public string RecipeSlug { get; set; }
        public string IngredientSlug { get; set; }
        public string Amount { get; set; }


        public Recipe Recipe { get; set; }
        public Ingredient Ingredient { get; set; }

        protected override bool EqualsCore(RecipeIngredient other)
        {
            return RecipeSlug == other.RecipeSlug
                && IngredientSlug == other.IngredientSlug
                && Amount.Equals(other.Amount);
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                var hashcode = 397 * IngredientSlug.GetHashCode();
                hashcode = (hashcode * 397) ^ RecipeSlug.GetHashCode();
                return (hashcode * 397) ^ Amount.GetHashCode();
            }
        }
    }
}
