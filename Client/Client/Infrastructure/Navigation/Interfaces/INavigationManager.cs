
namespace Client.Infrastructure.Navigation.Interfaces
{
    public interface INavigationManager
    {
        void Navigate(string navigationKey, object obj = null);
        void Register(string navigationKey, INavigationManager navigationManager = null);
        byte[] ChooseImage();
    }
}
