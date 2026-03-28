using HierarchicalItemProcessingSystem.Models;

namespace HierarchicalItemProcessingSystem.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task AddUserAsync(User user);
    }
}
