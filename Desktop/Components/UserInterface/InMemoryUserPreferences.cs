using k8s;

namespace Desktop.Components.UserInterface
{
    public static class InMemoryUserPreferencesStore 
    {
        public static string? CurrentContextName { get; set; }
    }
}