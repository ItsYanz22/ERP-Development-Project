using HierarchicalItemProcessingSystem.Models;
using HierarchicalItemProcessingSystem.Repositories;

namespace HierarchicalItemProcessingSystem.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            return await _itemRepository.GetAllItemsAsync();
        }

        public async Task<Item?> GetItemByIdAsync(int id)
        {
            return await _itemRepository.GetItemByIdAsync(id);
        }

        public async Task<IEnumerable<Item>> GetItemTreeAsync()
        {
            var allItems = await _itemRepository.GetAllItemsAsync();
            var tree = allItems.Where(i => i.ParentId == null).ToList();
            // EF Core will wire up the Children navigation property automatically
            // because instances are tracked in the same DbContext session.
            return tree;
        }

        public async Task AddItemAsync(Item item)
        {
            await _itemRepository.AddItemAsync(item);
        }

        public async Task UpdateItemAsync(Item item)
        {
            await _itemRepository.UpdateItemAsync(item);
        }

        public async Task DeleteItemAsync(int id)
        {
            var item = await _itemRepository.GetItemByIdAsync(id);
            if (item != null)
            {
                // Delete children recursively
                await DeleteChildrenRecursive(item.Id);
                await _itemRepository.DeleteItemAsync(item.Id);
            }
        }

        private async Task DeleteChildrenRecursive(int parentId)
        {
            var children = await _itemRepository.GetItemsByParentIdAsync(parentId);
            foreach (var child in children)
            {
                await DeleteChildrenRecursive(child.Id);
                await _itemRepository.DeleteItemAsync(child.Id);
            }
        }

        public async Task<bool> ProcessItemAsync(int parentId, IEnumerable<Item> children)
        {
            var parent = await _itemRepository.GetItemByIdAsync(parentId);
            if (parent == null) return false;

            decimal totalChildWeight = children.Sum(c => c.Weight);
            if (totalChildWeight > parent.Weight)
            {
                return false; // Validation failed
            }

            parent.Status = "Processed";
            await _itemRepository.UpdateItemAsync(parent);

            foreach (var child in children)
            {
                child.ParentId = parent.Id;
                child.Status = "Unprocessed";
                await _itemRepository.AddItemAsync(child);
            }

            return true;
        }
    }
}
