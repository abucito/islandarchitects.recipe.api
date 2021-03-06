using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Kooboos.API.Ingredients.AutomapperProfile;
using Kooboos.API.Ingredients;
using Kooboos.API.Ingredients.Models;

namespace Kooboos.Tests
{
    [TestFixture]
    public class IngredientsControllerTest
    {
        private IngredientsController ingredientsController;

        private Mock<IIngredientsService> ingredientsServiceMock;

        private IMapper mapper;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var mapperConfiguration = new MapperConfiguration(c => c.AddProfile<IngredientProfile>());
            mapper = mapperConfiguration.CreateMapper();
        }

        [Test]
        public void GetIngredients_Empty_ReturnsOk()
        {
            // Arrange
            ingredientsServiceMock = new Mock<IIngredientsService>();
            ingredientsServiceMock
                .Setup(m => m.GetAll())
                .Returns(new List<IngredientDto>());
            ingredientsController = new IngredientsController(ingredientsServiceMock.Object, mapper);

            // Act
            var result = ingredientsController.GetIngredients();

            // Assert
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult));
        }

        [Test]
        public void GetIngredient_ValidId_ReturnsOk()
        {
            // Arrange
            int validRecipeId = 1;
            ingredientsServiceMock = new Mock<IIngredientsService>();
            ingredientsServiceMock
                .Setup(m => m.GetById(validRecipeId))
                .Returns(new IngredientDto());

            ingredientsController = new IngredientsController(ingredientsServiceMock.Object, mapper);

            // Act
            var result = ingredientsController.GetIngredient(validRecipeId);

            // Assert
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult));
        }

        [Test]
        public void GetIngredient_InvalidId_ReturnsNotFound()
        {
            // Arrange
            int invalidRecipeId = 2;
            ingredientsServiceMock = new Mock<IIngredientsService>();
            ingredientsServiceMock
                .Setup(m => m.GetById(invalidRecipeId))
                .Returns(null as IngredientDto);

            ingredientsController = new IngredientsController(ingredientsServiceMock.Object, mapper);

            // Act
            var result = ingredientsController.GetIngredient(invalidRecipeId);

            // Assert
            Assert.IsTrue(result.GetType() == typeof(NotFoundResult));
        }

        [Test]
        public void CreateIngredient_WithValidationError_ReturnsBadRequestResult()
        {
            // Arrange
            ingredientsServiceMock = new Mock<IIngredientsService>();
            ingredientsController = new IngredientsController(ingredientsServiceMock.Object, mapper);
            ingredientsController.ModelState.AddModelError("Some Key", "Some Error Message");

            // Act
            var result = ingredientsController.CreateIngredient(new IngredientForCreationDto());

            // Assert
            Assert.IsTrue(result.GetType() == typeof(BadRequestObjectResult));
        }

        [Test]
        public void CreateIngredient_WithNameAndDescription_ReturnsCreatedResult()
        {
            // Arrange
            ingredientsServiceMock = new Mock<IIngredientsService>();
            ingredientsServiceMock
                .Setup(m => m.Insert(It.IsAny<IngredientDto>()))
                .Returns(1);
            ingredientsController = new IngredientsController(ingredientsServiceMock.Object, mapper);

            var ingredientForCreation = new IngredientForCreationDto
            {
                Name = "Cooking is fun",
                Description = "You should cook the meal"
            };

            // Act
            var result = ingredientsController.CreateIngredient(ingredientForCreation);

            // Assert
            Assert.IsTrue(result.GetType() == typeof(CreatedAtRouteResult));
        }

        [Test]
        public void CreateIngredient_SomethinWentWrongWhileInserting_Returns500InternalServerError()
        {
            // Arrange
            ingredientsServiceMock = new Mock<IIngredientsService>();
            ingredientsServiceMock
                .Setup(m => m.Insert(It.IsAny<IngredientDto>()))
                .Returns(0);
            ingredientsController = new IngredientsController(ingredientsServiceMock.Object, mapper);

            var ingredientForCreation = new IngredientForCreationDto
            {
                Name = "Black Pepper",
                Description = "Black Pepper is spicy and thus healthy"
            };

            // Act
            var result = ingredientsController.CreateIngredient(ingredientForCreation);

            // Assert
            Assert.IsTrue(result.GetType() == typeof(StatusCodeResult));
            Assert.IsTrue(((StatusCodeResult)result).StatusCode == StatusCodes.Status500InternalServerError);
        }

        [Test]
        public void DeleteIngredient_ValidId_ReturnsNoContent()
        {
            // Arrange
            int validRecipeId = 1;
            ingredientsServiceMock = new Mock<IIngredientsService>();
            ingredientsServiceMock
                .Setup(m => m.GetById(validRecipeId))
                .Returns(new IngredientDto());

            ingredientsController = new IngredientsController(ingredientsServiceMock.Object, mapper);

            // Act
            var result = ingredientsController.DeleteIngredient(validRecipeId);

            // Assert
            Assert.IsTrue(result.GetType() == typeof(NoContentResult));
        }

        [Test]
        public void DeleteIngredient_InvalidId_ReturnsNotFound()
        {
            // Arrange
            int invalidRecipeId = 2;
            ingredientsServiceMock = new Mock<IIngredientsService>();
            ingredientsServiceMock
                .Setup(m => m.GetById(invalidRecipeId))
                .Returns(null as IngredientDto);

            ingredientsController = new IngredientsController(ingredientsServiceMock.Object, mapper);

            // Act
            var result = ingredientsController.DeleteIngredient(invalidRecipeId);

            // Assert
            Assert.IsTrue(result.GetType() == typeof(NotFoundResult));
        }

        [Test]
        public void FullyUpdateIngredient_ValidIdAndValidData_ReturnsNoContent()
        {
            // Arrange
            int validIngredientId = 1;
            var ingredientForUpdateDto = new IngredientForUpdateDto();
            ingredientsServiceMock = new Mock<IIngredientsService>();
            ingredientsServiceMock
                .Setup(m => m.GetById(validIngredientId))
                .Returns(new IngredientDto());
            ingredientsController = new IngredientsController(ingredientsServiceMock.Object, mapper);

            // Act
            var result = ingredientsController.FullyUpdateIngredient(validIngredientId, ingredientForUpdateDto);

            // Assert
            Assert.IsTrue(result.GetType() == typeof(NoContentResult));
        }

        [Test]
        public void FullyUpdateIngredient_ValidIdAndInvalidData_ReturnsBadRequest()
        {
            // Arrange
            int validIngredientId = 1;
            var ingredientForUpdateDto = new IngredientForUpdateDto();
            ingredientsServiceMock = new Mock<IIngredientsService>();
            ingredientsServiceMock
                .Setup(m => m.GetById(validIngredientId))
                .Returns(new IngredientDto());
            ingredientsController = new IngredientsController(ingredientsServiceMock.Object, mapper);
            ingredientsController.ModelState.AddModelError("Some Key", "Some Error Message");

            // Act
            var result = ingredientsController.FullyUpdateIngredient(validIngredientId, ingredientForUpdateDto);

            // Assert
            Assert.IsTrue(result.GetType() == typeof(BadRequestObjectResult));
        }

        [Test]
        public void FullyUpdateIngredient_InvalidId_ReturnsNotFound()
        {
            // Arrange
            int invalidIngredientId = 2;
            var ingredientForUpdateDto = new IngredientForUpdateDto();
            ingredientsServiceMock = new Mock<IIngredientsService>();
            ingredientsServiceMock
                .Setup(m => m.GetById(invalidIngredientId))
                .Returns(null as IngredientDto);
            ingredientsController = new IngredientsController(ingredientsServiceMock.Object, mapper);

            // Act
            var result = ingredientsController.FullyUpdateIngredient(invalidIngredientId, ingredientForUpdateDto);

            // Assert
            Assert.IsTrue(result.GetType() == typeof(NotFoundResult));
        }
    }
}