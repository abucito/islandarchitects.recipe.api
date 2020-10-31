using AutoMapper;
using Kooboos.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kooboos.API.IngredientsLists
{
    [ApiController]
    [Route("api/recipes/{recipeId}/ingredientslist")]
    public class IngredientsListController : ControllerBase
    {
        private readonly IIngredientsListService ingredientsListService;

        private readonly IMapper mapper;

        public IngredientsListController(IIngredientsListService ingredientsListService, IMapper mapper)
        {
            this.ingredientsListService = ingredientsListService;
            this.mapper = mapper;
        }

        [HttpGet(Name = "GetIngredientsList")]
        public IActionResult GetIngredientsList(int recipeId)
        {
            var ingredientsListDto = ingredientsListService.GetByRecipeId(recipeId);
            if (ingredientsListDto == null)
            {
                return NotFound();
            }

            return Ok(ingredientsListDto);
        }

        [HttpPost]
        [Route("item")]
        public IActionResult AddIngredientsListItem(
            int recipeId,
            [FromBody] IngredientsListItemForCreationDto ingredientListItemForCreationDto)
        {
            if (!ModelState.IsValid
                    || !ingredientsListService.UnitExists(ingredientListItemForCreationDto.UnitId)
                    || !ingredientsListService.IngredientExists(ingredientListItemForCreationDto.IngredientId))
            {
                return BadRequest(ModelState);
            }

            if (!ingredientsListService.RecipeExists(recipeId))
            {
                return NotFound();
            }
            var ingredientListItemDto = mapper.Map<IngredientsListItemDto>(ingredientListItemForCreationDto);

            var idOfNewIngredientsListItem = ingredientsListService.InsertIngredientsListItem(recipeId, ingredientListItemDto);

            if (idOfNewIngredientsListItem > 0)
            {
                return CreatedAtRoute("GetIngredientsList", new { recipeId = recipeId }, ingredientListItemDto);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}