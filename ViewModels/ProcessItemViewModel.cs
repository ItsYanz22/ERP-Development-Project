using System.ComponentModel.DataAnnotations;

namespace HierarchicalItemProcessingSystem.ViewModels
{
    public class ProcessItemViewModel
    {
        [Required]
        public int ParentId { get; set; }

        public List<ItemViewModel> Children { get; set; } = new List<ItemViewModel>();
    }
}
