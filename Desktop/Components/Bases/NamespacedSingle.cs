namespace Desktop.Components.Bases
{
    public abstract class NamespacedSingle : Namespaced
    {
        protected string CurrentUri => NavigationManager.Uri;
    }
}