using Client.Infrastructure;
using Client.Infrastructure.Navigation;
using Client.View;
using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        NavigationManager navigationManager;

        protected override void OnStartup(StartupEventArgs e)
        {
            var window = new ProgramWindowView();

            navigationManager = new NavigationManager(Dispatcher, window.MainContentPlace);

            var viewModel = new ProgramWindowViewModel(navigationManager);
            LoadingManager.LoadingStartCompanents(viewModel);

            window.DataContext = viewModel;

            window.Show();
        }
    }
}
