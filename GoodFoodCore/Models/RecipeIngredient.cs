using GoodFoodCore.Common;

namespace GoodFoodCore.Models
{
    public class RecipeIngredient : ValueObject<RecipeIngredient>
    {
        public Ingredient Ingredient { get; }
        public string Amount { get; }

        public RecipeIngredient(Ingredient ingredient, string amount)
        {
            Ingredient = ingredient;
            Amount = amount;
        }

        protected override bool EqualsCore(RecipeIngredient other)
        {
            return Ingredient.Slug == other.Ingredient.Slug && Amount.Equals(other.Amount);
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                var hashcode = 397 * Ingredient.Slug.GetHashCode();
                return (hashcode * 397) ^ Amount.GetHashCode();
            }
        }
    }
}
