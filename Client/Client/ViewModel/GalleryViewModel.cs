using Client.Infrastructure;
using Client.Infrastructure.Commands.Base;
using Client.Infrastructure.Navigation;
using Client.Infrastructure.Navigation.Interfaces;
using Client.Models;
using Client.ViewModel.Base;
using HTTP_API;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Client.ViewModel
{
    public class GalleryViewModel : ViewModelBase, INavigationAware
    {
        private ObservableCollection<ClientCard> _cards;
        public ObservableCollection<ClientCard> Cards
        {
            get
            {
                return _cards;
            }
            set
            {
                Set(ref _cards, value);
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
        public ICommand GetCards { get; }

        private void ExecuteGetCards(object obj)
        {
            try
            {
                ErrorMessage = null;
                Cards = new ObservableCollection<ClientCard>(Converter.ConvertServerDataToClientCard(ClientAPI.GetCards()));
            }
            catch(System.AggregateException aggException)
            {
                ErrorMessage = "Сервер не ответил. Попробуйте обновить позже.";
            }
        }
        private bool CanGetCards(object obj)
        {
            return true;

        }

        public ICommand GoToAddCard { get; }

        private void ExecuteGoToAddCard(object obj)
        {
            navigationManager.Register(NavigationKeys.AddCardViewKey);
            navigationManager.Navigate(NavigationKeys.AddCardViewKey, new object[] { "Добавить" });
        }
        private bool CanGoToAddCard(object obj)
        {
            return true;

        }

        public ICommand GoToUpdateCard { get; }

        private void ExecuteGoToUpdateCard(object obj)
        {
            navigationManager.Register(NavigationKeys.UpdateCardViewKey);
            navigationManager.Navigate(NavigationKeys.UpdateCardViewKey, new object[] { "Редактировать", obj });
        }
        private bool CanGoToUpdateCard(object obj)
        {
            return true;

        }

        public ICommand DeleteCard { get; }

        private void ExecuteDeleteCard(object obj)
        {
            ClientAPI.DeleteCard(Converter.ConvertClientCardToServerData((obj as ClientCard)).Id);
            GetCards.Execute(null);
        }
        private bool CanDeleteCard(object obj)
        {
            return true;

        }

        public ICommand DeleteChoised { get; }

        private void ExecuteDeleteChoised(object obj)
        {
            IList choisedItems = (IList)obj;
            var deletedItems = choisedItems.Cast<ClientCard>().ToList();
            deletedItems.ForEach(i => ClientAPI.DeleteCard(i.Id));

            GetCards.Execute(null);
        }
        private bool CanDeleteChoised(object obj)
        {
            return true;

        }

        private bool _isSortAscending = false;
        public ICommand SortCollection { get; }

        private void ExecuteSortCollection(object obj)
        {
            if (ErrorMessage != null)
                return;

            if(_isSortAscending)
            {
                Cards = new ObservableCollection<ClientCard>(Cards.OrderByDescending(i => i.Name));
                _isSortAscending = false;
            }   
            else
            {
                Cards = new ObservableCollection<ClientCard>(Cards.OrderBy(i => i.Name));
                _isSortAscending = true;
            }
        }
        private bool CanSortCollection(object obj)
        {
            return true;

        }

        public GalleryViewModel(INavigationManager navigationManager) : base(navigationManager)
        {
            GetCards = new BaseCommand(ExecuteGetCards, CanGetCards);
            GoToAddCard = new BaseCommand(ExecuteGoToAddCard, CanGoToAddCard);
            GoToUpdateCard = new BaseCommand(ExecuteGoToUpdateCard, CanGoToUpdateCard);
            DeleteCard = new BaseCommand(ExecuteDeleteCard, CanDeleteCard);
            DeleteChoised = new BaseCommand(ExecuteDeleteChoised, CanDeleteChoised);
            SortCollection = new BaseCommand(ExecuteSortCollection, CanSortCollection);
        }

        public void WantDoSomethingBeforeClose()
        {
            
        }

        public void WantDoSomethingBeforeOpen(object obj = null)
        {
            GetCards.Execute(null);
        }
    }
}
