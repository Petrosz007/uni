using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ELTE.Windows.TodoList.Model;
using ELTE.Windows.TodoList.View;
using ELTE.Windows.TodoList.ViewModel;

namespace ELTE.Windows.TodoList
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TodoListDbContext model;
        private TodoListViewModel viewModel;
        private MainWindow view;

        public App()
        {
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            model = new TodoListDbContext("name=TodoListModel");
            DbInitializer.Initialize(model);

            viewModel = new TodoListViewModel(model);

            view = new MainWindow
            {
                DataContext = viewModel
            };

            view.Show();
        }
    }
}
