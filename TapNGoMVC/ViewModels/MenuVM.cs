using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TapNGoMVC.ViewModels
{
    public class MenuVM
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        [ValidateNever]
        public IFormFile? ImageFile { get; set; }

        public string CategoryName { get; set; } = null!;

        public int Quantity { get; set; } = 0;
    }
}
