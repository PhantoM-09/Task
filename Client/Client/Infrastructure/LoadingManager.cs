using Client.Infrastructure.FileWork;
using Client.ViewModel.Interfaces;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace Client.Infrastructure
{
    //Класс менеджера загрузок, содержит методя для считывания конфигурационного файла и применения параметров из него
    class LoadingManager
    {
        public static void LoadingStartCompanents(IMainViewModel mainViewModel)
        {
            LogoConfiguration startParameters = FileManager.ReadFile<LogoConfiguration>(ConfigFilesPath.FirstConfigFile);

            string pathAppLogo = Path.GetFullPath(startParameters.AppLogo);
            mainViewModel.AppLogo = new BitmapImage(new Uri(pathAppLogo));

            string pathAppIcon = Path.GetFullPath(startParameters.AppIcon);
            mainViewModel.AppIcon = new BitmapImage(new Uri(pathAppIcon));
        }
    }
}
