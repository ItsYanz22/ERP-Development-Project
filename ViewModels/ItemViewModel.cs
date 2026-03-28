using System.ComponentModel.DataAnnotations;

namespace HierarchicalItemProcessingSystem.ViewModels
{
    public class ItemViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Item Name is required")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Weight is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Weight must be greater than 0")]
        public decimal Weight { get; set; }

        public int? ParentId { get; set; }

        public string Status { get; set; } = "Unprocessed";
    }
}
