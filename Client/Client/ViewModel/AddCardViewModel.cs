
using Client.Infrastructure;
using Client.Infrastructure.Commands.Base;
using Client.Infrastructure.Navigation;
using Client.Infrastructure.Navigation.Interfaces;
using Client.ViewModel.Base;
using HTTP_API;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Client.ViewModel
{
    public class AddCardViewModel : ViewModelBase, INavigationAware
    {
        private string _operationName;
        public string OperationName
        {
            get
            {
                return _operationName;
            }
            set
            {
                Set(ref _operationName, value);
            }
        }

        private string _cardName;
        public string CardName
        {
            get
            {
                return _cardName;
            }
            set
            {
                Set(ref _cardName, value);
            }
        }

        private BitmapImage _cardImage;
        public BitmapImage CardImage
        {
            get
            {
                return _cardImage;
            }
            set
            {
                Set(ref _cardImage, value);
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                Set(ref _errorMessage, value);
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

        public ICommand OperationWithCard { get; }

        private void ExecuteOperationWithCard(object obj)
        {
            if (string.IsNullOrEmpty(CardName))
            {
                ErrorMessage = "Введите имя.";
                return;
            }
            if(CardImage is null)
            {
                ErrorMessage = "Введите картинку.";
                return;
            }

            ClientAPI.AddCard(CardName, Converter.ConvertImageToByteArray(CardImage));
            GoToGallery.Execute(null);
        }
        private bool CanOperationWithCard(object obj)
        {
            return true;

        }

        public ICommand ChooseImage { get; }

        private void ExecuteChooseImage(object obj)
        {
            byte[] image = navigationManager.ChooseImage();
            if (image != null)
            {
                CardImage = Converter.ConvertByteArrayToImage(image);
            }
        }
        private bool CanChooseImage(object obj)
        {
            return true;

        }

        public AddCardViewModel(INavigationManager navigationManager) : base(navigationManager)
        {
            GoToGallery = new BaseCommand(ExecuteGoToGallery, CanGoToGallery);
            OperationWithCard = new BaseCommand(ExecuteOperationWithCard, CanOperationWithCard);
            ChooseImage = new BaseCommand(ExecuteChooseImage, CanChooseImage);
        }

        public void WantDoSomethingBeforeClose()
        {

        }

        public void WantDoSomethingBeforeOpen(object obj = null)
        {
            object[] parameterArray = (object[])obj;
            if(parameterArray != null)
            {
                OperationName = parameterArray[0].ToString();
            }
        }
    }
}
