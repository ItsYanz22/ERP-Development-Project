namespace HierarchicalItemProcessingSystem.Services
{
    public interface IAuthService
    {
        Task<bool> ValidateUserAsync(string email, string password);
    }
}
