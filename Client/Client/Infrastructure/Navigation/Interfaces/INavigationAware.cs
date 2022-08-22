

namespace Client.Infrastructure.Navigation.Interfaces
{
    public interface INavigationAware
    {
        void WantDoSomethingBeforeClose();
        void WantDoSomethingBeforeOpen(object obj = null);
    }
}
