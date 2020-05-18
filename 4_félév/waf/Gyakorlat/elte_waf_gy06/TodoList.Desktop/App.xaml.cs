using Microsoft.Win32;
using System;
using System.Configuration;
using System.IO;
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
        private LoginViewModel _loginViewModel;
        private MainWindow _mainView;
        private LoginWindow _loginView;
        private ItemEditorWindow _editorView;

        public App()
        {
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _service = new TodoListAPIService(ConfigurationManager.AppSettings["baseAddress"]);

            _loginViewModel = new LoginViewModel(_service);

            _loginViewModel.LogintSucceeded += ViewModel_LoginSucceeded;
            _loginViewModel.LoginFailed += ViewModel_LoginFailed;
            _loginViewModel.MessageApplication += ViewModel_MessageApplication;

            _loginView = new LoginWindow
            {
                DataContext = _loginViewModel
            };

            _mainViewModel = new MainViewModel(_service);
            _mainViewModel.LogoutSucceeded += ViewModel_LogoutSucceeded;
            _mainViewModel.MessageApplication += ViewModel_MessageApplication;
            _mainViewModel.StartingItemEdit += ViewModel_StartingItemEdit;
            _mainViewModel.FinishingItemEdit += ViewModel_FinishingItemEdit;
            _mainViewModel.StartingImageChange += ViewModel_StartingImageChange;

            _mainView = new MainWindow
            {
                DataContext = _mainViewModel
            };

            _loginView.Show();
        }

        private void ViewModel_LoginSucceeded(object sender, EventArgs e)
        {
            _loginView.Hide();
            _mainView.Show();
        }

        private void ViewModel_LoginFailed(object sender, EventArgs e)
        {
            MessageBox.Show("Login failed!", "TodoList", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void ViewModel_LogoutSucceeded(object sender, EventArgs e)
        {
            _mainView.Hide();
            _loginView.Show();
        }

        private void ViewModel_MessageApplication(object sender, MessageEventArgs e)
        {
            MessageBox.Show(e.Message, "TodoList", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void ViewModel_StartingItemEdit(object sender, EventArgs e)
        {
            _editorView = new ItemEditorWindow
            {
                DataContext = _mainViewModel
            };
            _editorView.ShowDialog();
        }

        private void ViewModel_FinishingItemEdit(object sender, EventArgs e)
        {
            if (_editorView.IsActive)
            {
                _editorView.Close();
            }
        }

        private async void ViewModel_StartingImageChange(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Filter = "Images|*.jpg;*.jpeg;*.bmp;*.tif;*.gif;*.png;",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            if (dialog.ShowDialog(_editorView).GetValueOrDefault(false))
            {
                _mainViewModel.SelectedItem.Image = await File.ReadAllBytesAsync(dialog.FileName);
            }
        }
    }
}
