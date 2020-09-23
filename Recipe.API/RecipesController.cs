using System;
using Microsoft.AspNetCore.Mvc;

namespace Recipe.API
{
    [ApiController]
    [Route("api/recipes")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipesService recipesService;

        public RecipesController(IRecipesService recipesService)
        {
            this.recipesService = recipesService ?? throw new ArgumentNullException();
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetRecipies()
        {
            var recipes = recipesService.GetRecipies();

            return Ok(recipes);
        }
    }
}