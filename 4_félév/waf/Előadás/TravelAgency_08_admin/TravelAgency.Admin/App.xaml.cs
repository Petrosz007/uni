using System;
using System.IO;
using System.Windows;
using ELTE.TravelAgency.Admin.Model;
using ELTE.TravelAgency.Admin.Persistence;
using ELTE.TravelAgency.Admin.View;
using ELTE.TravelAgency.Admin.ViewModel;
using Microsoft.Extensions.Configuration;

namespace ELTE.TravelAgency.Admin
{
    public partial class App : Application
    {
        private ITravelAgencyModel _model;
        private MainViewModel _viewModel;
        private MainWindow _view;

        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            IConfigurationRoot configuration = configBuilder.Build();

            _model = new TravelAgencyModel(new TravelAgencyServicePersistence(configuration["BaseAddress"])); // megadjuk a szolgáltatás címét
            _viewModel = new MainViewModel(_model);
            _viewModel.MessageApplication += new EventHandler<MessageEventArgs>(ViewModel_MessageApplication);
            _viewModel.ExitApplication += new EventHandler(ViewModel_ExitApplication);

            _view = new MainWindow();
            _view.DataContext = _viewModel;
            _view.Show();
        }

        private void ViewModel_MessageApplication(object sender, MessageEventArgs e)
        {
            MessageBox.Show(e.Message, "Utazási ügynökség", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private async void ViewModel_ExitApplication(object sender, System.EventArgs e)
        {
            // az adatok opcionális mentése
            if (MessageBox.Show("Elmentsük a változtatásokat?", "Utazási ügynökség", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                await _model.SaveAsync();
            }

            Shutdown();
        }
    }
}