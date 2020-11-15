using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Kooboos.API.Base;
using Kooboos.API.Recipes.Entities;

namespace Kooboos.API.IngredientsLists.Entities
{
    public class IngredientsList : IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [ForeignKey("RecipeId")]
        public Recipe Recipe { get; set; }

        [Required]
        public int RecipeId { get; set; }

        public ICollection<IngredientsListItem> IngredientsListItems { get; set; } = new List<IngredientsListItem>();
    }
}