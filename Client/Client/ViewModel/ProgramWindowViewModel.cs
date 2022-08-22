using Client.Infrastructure.Commands.Base;
using Client.Infrastructure.Navigation;
using Client.Infrastructure.Navigation.Interfaces;
using Client.ViewModel.Base;
using Client.ViewModel.Interfaces;
using System.Windows.Input;
using System.Windows.Media;

namespace Client.ViewModel
{
    public class ProgramWindowViewModel : ViewModelBase, IMainViewModel
    {
        private ImageSource appLogo;
        public ImageSource AppLogo
        {
            get
            {
                return appLogo;
            }
            set
            {
                appLogo = value;
            }
        }

        private ImageSource appIcon;
        public ImageSource AppIcon
        {
            get
            {
                return appIcon;
            }
            set
            {
                appIcon = value;
            }
        }

        public ICommand GoToGallery { get; }

        private void ExecuteGoToGallery(object obj)
        {
            navigationManager.Register(NavigationKeys.GalleryViewKey);
            navigationManager.Navigate(NavigationKeys.GalleryViewKey);
        }
        private bool CanGoToGallery(object obj)
        {
            return true;

        }

        public ProgramWindowViewModel(INavigationManager navigationManager) : base(navigationManager)
        {
            navigationManager.Register(NavigationKeys.GalleryViewKey);
            navigationManager.Navigate(NavigationKeys.GalleryViewKey);

            GoToGallery = new BaseCommand(ExecuteGoToGallery, CanGoToGallery);
        }
    }
}
