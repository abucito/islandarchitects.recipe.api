using System.Collections.Generic;
using Recipe.API.Models;

namespace Recipe.API
{
    public interface IRecipesRepository
    {
        IList<RecipeDto> GetRecipes();

        RecipeDto GetRecipe(int id);

        int InsertRecipe(RecipeDto recipeDto);

        void DeleteRecipe(RecipeDto recipeDto);

        void UpdateRecipe(RecipeDto recipeToUpdate, RecipeDto recipeDtoWithNewValues);
    }
}