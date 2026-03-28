using HierarchicalItemProcessingSystem.Models;

namespace HierarchicalItemProcessingSystem.Services
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<Item?> GetItemByIdAsync(int id);
        Task<IEnumerable<Item>> GetItemTreeAsync();
        Task AddItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAsync(int id);
        Task<bool> ProcessItemAsync(int parentId, IEnumerable<Item> children);
    }
}
