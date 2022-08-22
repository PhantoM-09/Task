using Client.Infrastructure.Navigation.Interfaces;
using Client.View;
using Client.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Client.Infrastructure.Navigation
{
    class NavigationManager : INavigationManager
    {
        private Dispatcher _dispatcher;
        private ContentControl _currentContentControl;
        private Dictionary<string, object> _viewModelsByKeys = new Dictionary<string, object>();
        private Dictionary<Type, object> _viewObjectByViewModelType = new Dictionary<Type, object>();

        public NavigationManager(Dispatcher dispatcher, ContentControl currentContentControl)
        {
            this._dispatcher = dispatcher;
            this._currentContentControl = currentContentControl;
        }

        public void Register(string navigationKey, INavigationManager navigationManager = null)
        {
            if (navigationKey == null)
                throw new ArgumentNullException("navigationKey");

            switch (navigationKey)
            {
                case NavigationKeys.GalleryViewKey:
                    AddElementToDictionary<GalleryViewModel, GalleryView>(new GalleryViewModel(this), new GalleryView(), navigationKey);
                    break;
                case NavigationKeys.AddCardViewKey:
                    AddElementToDictionary<AddCardViewModel, OperationView>(new AddCardViewModel(this), new OperationView(), navigationKey);
                    break;
                case NavigationKeys.UpdateCardViewKey:
                    AddElementToDictionary<UpdateCardViewModel, OperationView>(new UpdateCardViewModel(this), new OperationView(), navigationKey);
                    break;
            }
        }

        public void Navigate(string navigationKey, object obj = null)
        {
            if (navigationKey == null)
                throw new ArgumentNullException("navigationKey");

            ExecutionActionInMainThread(() =>
            {
                DoSomethingBeforeClose();
                var viewModel = GetViewModelFromDictionary(navigationKey);
                DoSomethingBeforeOpen(viewModel, obj);

                var view = CreateViewByViewModel(viewModel);
                _currentContentControl.Content = view;
            });
        }

        private void ExecutionActionInMainThread(Action action)
        {
            _dispatcher.Invoke(action);
        }

        private void AddElementToDictionary<TViewModel, TView>(TViewModel viewModel, TView view, string navigationKey)
            where TViewModel : class
            where TView : FrameworkElement
        {
            if (viewModel == null)
                throw new ArgumentNullException("viewModel");
            if (view == null)
                throw new ArgumentNullException("view");
            if (navigationKey == null)
                throw new ArgumentNullException("navigationKey");

            _viewModelsByKeys[navigationKey] = viewModel;
            _viewObjectByViewModelType[typeof(TViewModel)] = view;
        }

        private object GetViewModelFromDictionary(string navigationKey)
        {
            if (navigationKey == null)
                throw new ArgumentNullException("navigationKey");
            return _viewModelsByKeys[navigationKey];
        }

        private FrameworkElement CreateViewByViewModel(object viewModel)
        {
            var view = (FrameworkElement)_viewObjectByViewModelType[viewModel.GetType()];
            view.DataContext = viewModel;
            return view;
        }

        private void DoSomethingBeforeClose(object obj = null)
        {
            var oldView = _currentContentControl.Content as FrameworkElement;
            if (oldView == null)
                return;

            var viewModelOldView = oldView.DataContext as INavigationAware;

            if (viewModelOldView == null)
                return;

            viewModelOldView.WantDoSomethingBeforeClose();
        }

        private void DoSomethingBeforeOpen(object viewModel, object obj = null)
        {
            var newViewModel = viewModel as INavigationAware;

            if (newViewModel == null)
                return;

            newViewModel.WantDoSomethingBeforeOpen(obj);
        }

        public byte[] ChooseImage()
        {
            OpenFileDialog choiceWindow = new OpenFileDialog();

            choiceWindow.Filter = "Файлы рисунков (*.png)|*.png";
            choiceWindow.FilterIndex = 1;
            choiceWindow.Multiselect = false;
            if (choiceWindow.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(choiceWindow.FileName, FileMode.Open, FileAccess.Read);
                byte[] array = new byte[fileStream.Length];
                fileStream.Read(array, 0, (int)fileStream.Length);
                fileStream.Close();

                return array;
            }
            return null;
        }
    }
}
