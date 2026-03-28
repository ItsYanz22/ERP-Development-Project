using HierarchicalItemProcessingSystem.Models;

namespace HierarchicalItemProcessingSystem.Repositories
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<Item?> GetItemByIdAsync(int id);
        Task<IEnumerable<Item>> GetItemsByParentIdAsync(int? parentId);
        Task AddItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAsync(int id);
        Task SaveChangesAsync();
    }
}
