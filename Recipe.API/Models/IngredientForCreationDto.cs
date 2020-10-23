using System.ComponentModel.DataAnnotations;

namespace Recipe.API.Models
{
    public class IngredientForCreationDto
    {
        [Required(AllowEmptyStrings = false), MaxLength(50)]
        public string Name { get; set; }


        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }
    }
}