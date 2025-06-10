using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TapNGoMVC.ViewModels
{
    public class MenuEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [Range(0.01, 1000)]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        [ValidateNever]
        public IFormFile? ImageFile { get; set; }

        [Display(Name = "Kategorija")]
        [Required]
        public int MenuCategoryId { get; set; }
        [ValidateNever]
        public List<SelectListItem>? Categories { get; set; }

        [Required]
        public int UserId { get; set; }
        [ValidateNever]
        public List<SelectListItem>? Users { get; set; }
    }
}
