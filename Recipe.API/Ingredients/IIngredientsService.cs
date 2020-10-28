using System.Collections.Generic;
using Recipe.API.Base;
using Recipe.API.Entities;
using Recipe.API.Models;

namespace Recipe.API.Ingredients
{
    public interface IIngredientsService : IBaseCrudService<Ingredient, IngredientDto>
    {

    }
}