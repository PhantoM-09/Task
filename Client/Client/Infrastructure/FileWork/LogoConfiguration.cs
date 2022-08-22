using System;

namespace Client.Infrastructure.FileWork
{
    [Serializable]
    class LogoConfiguration
    {
        private string appIcon;
        public string AppIcon
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

        private string appLogo;
        public string AppLogo
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
    }
}
