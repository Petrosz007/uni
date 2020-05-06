using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ELTE.Windows.TodoList.Model;

namespace ELTE.Windows.TodoList.ViewModel
{
    public class TodoListViewModel : ViewModelBase
    {
        private TodoListDbContext context;

        private ListViewModel currentList;
        private string currentListName = String.Empty;

        public ObservableCollection<ListViewModel> Lists { get; set; }

        public ObservableCollection<ItemViewModel> Items { get; set; }

        public string CurrentListName
        {
            get { return currentListName; }
            set { currentListName = value; OnPropertyChanged(); }
        }

        public DelegateCommand SelectCommand { get; private set; }

        public DelegateCommand RenameListCommand { get; private set; }
        public DelegateCommand NewListCommand { get; private set; }
        public DelegateCommand DeleteListCommand { get; private set; }

        public TodoListViewModel(TodoListDbContext context)
        {
            this.context = context;

            Lists = new ObservableCollection<ListViewModel>();
            Items = new ObservableCollection<ItemViewModel>();

            LoadListsAsync();

            SelectCommand = new DelegateCommand(param =>
            {
                if (param != null)
                    LoadItems(param as ListViewModel);
            });

            RenameListCommand = new DelegateCommand(
                param => CurrentListName.Length > 0, 
                async param => await RenameList());
            NewListCommand = new DelegateCommand(
                param => CurrentListName.Length > 0, 
                async param => await AddList());
            DeleteListCommand = new DelegateCommand(
                param => CurrentListName.Length > 0,
                async param => await DeleteList());
        }

        public async Task LoadListsAsync()
        {
            var loadedLists = await context.Lists.ToListAsync();
            Lists.Clear();
            foreach (var list in loadedLists.Select(l => new ListViewModel(l)))
            {
                Lists.Add(list);
            }

            currentList = null;
            CurrentListName = String.Empty;
        }

        public void LoadItems(ListViewModel list)
        {
            Items.CollectionChanged -= OnItemsChanged;
            Items.Clear();
            foreach (var item in list.Items)
            {
                Items.Add(item);
                item.PropertyChanged += (o, args) => { context.SaveChanges(); };
            }
            Items.CollectionChanged += OnItemsChanged;

            currentList = list;
            CurrentListName = currentList.Name;
        }

        private void OnItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.NewItems != null)
                foreach (ItemViewModel item in e.NewItems)
                {
                    currentList.Entity.Items.Add(item.Entity);
                    item.PropertyChanged += (o, args) => { context.SaveChanges(); };
                }

            if(e.OldItems != null)
                foreach (ItemViewModel item in e.OldItems)
                {
                    context.Items.Remove(item.Entity);
                }

            context.SaveChanges();
        }

        public async Task RenameList()
        {
            currentList.Name = currentListName;
            await context.SaveChangesAsync();
        }

        public async Task AddList()
        {
            context.Lists.Add(new List
            {
                Name = CurrentListName,
                Items = new List<Item>()
            });
            await context.SaveChangesAsync();
            await LoadListsAsync();
        }

        public async Task DeleteList()
        {
            context.Lists.Remove(currentList.Entity);
            await context.SaveChangesAsync();
            await LoadListsAsync();
        }
    }
}