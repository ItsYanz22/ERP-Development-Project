namespace HierarchicalItemProcessingSystem.ViewModels
{
    public class ProfileViewModel
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public string AccountStatus { get; set; } = "Active Assessor";
    }
}
