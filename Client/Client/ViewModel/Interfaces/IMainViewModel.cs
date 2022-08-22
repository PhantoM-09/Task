using System.Windows.Media;

namespace Client.ViewModel.Interfaces
{
    public interface IMainViewModel
    {
        ImageSource AppIcon { get; set; }
        ImageSource AppLogo { get; set; }
    }
}
