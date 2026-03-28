using HierarchicalItemProcessingSystem.Models;

namespace HierarchicalItemProcessingSystem.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<Item> Tree { get; set; } = new List<Item>();
        public ProcessItemViewModel ProcessModel { get; set; } = new ProcessItemViewModel();
        public List<Item> ParentItems { get; set; } = new List<Item>();
    }
}
