
using Client.Infrastructure;
using Client.Infrastructure.Commands.Base;
using Client.Infrastructure.Navigation;
using Client.Infrastructure.Navigation.Interfaces;
using Client.Models;
using Client.ViewModel.Base;
using HTTP_API;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Client.ViewModel
{
    public class UpdateCardViewModel : ViewModelBase, INavigationAware
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

        private int _cardId;
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
            ClientAPI.UpdateCard(_cardId, CardName, Converter.ConvertImageToByteArray(CardImage));
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
            if(image != null)
            {
                CardImage = Converter.ConvertByteArrayToImage(image);
            }
        }
        private bool CanChooseImage(object obj)
        {
            return true;

        }

        public UpdateCardViewModel(INavigationManager navigationManager) : base(navigationManager)
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
            if (parameterArray != null)
            {
                OperationName = parameterArray[0].ToString();

                ClientCard card = parameterArray[1] as ClientCard;
                _cardId = card.Id;
                CardName = card.Name;
                CardImage = card.Image;
            }
        }
    }
}
