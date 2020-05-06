using System.Configuration;
using System.Windows;
using TodoList.Desktop.Model;
using TodoList.Desktop.View;
using TodoList.Desktop.ViewModel;

namespace TodoList.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TodoListAPIService _service;
        private MainViewModel _mainViewModel;
        private MainWindow _view;

        public App()
        {
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _service = new TodoListAPIService(ConfigurationManager.AppSettings["baseAddress"]);

            _mainViewModel = new MainViewModel(_service);
            _mainViewModel.MessageApplication += ViewModel_MessageApplication;

            _view = new MainWindow
            {
                DataContext = _mainViewModel
            };

            _view.Show();
        }

        private void ViewModel_MessageApplication(object sender, MessageEventArgs e)
        {
            MessageBox.Show(e.Message, "TodoList", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
    }
}
