using Client.Infrastructure.Navigation.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Client.ViewModel.Base
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        protected readonly INavigationManager navigationManager;

        public event PropertyChangedEventHandler PropertyChanged;

        protected ViewModelBase(INavigationManager navigationManager)
        {
            this.navigationManager = navigationManager;
        }

        internal virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            else
            {
                field = value;
                OnPropertyChanged(PropertyName);
                return true;
            }
        }
    }
}
