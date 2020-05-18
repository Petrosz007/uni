using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Windows.Controls;
using System.Windows.Data;
using TodoList.Desktop.Model;
using TodoList.Persistence.DTO;

namespace TodoList.Desktop.ViewModel
{
    public class SelectedListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ListViewModel)
                return value;
            return null;
        }
    }

    public class MainViewModel : ViewModelBase
    {
        private readonly TodoListAPIService _service;
        private ObservableCollection<ListViewModel> _lists;
        private ObservableCollection<ItemViewModel> _items;
        private ListViewModel _selectedList;
        private ItemViewModel _selectedItem;

        public ObservableCollection<ListViewModel> Lists
        {
            get => _lists;
            set
            {
                _lists = value;
                OnPropertyChanged();
            }
        }

        public List<ListViewModel> ListsForCombo
        {
            get => Lists.ToList();
        }

        public ObservableCollection<ItemViewModel> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        public ListViewModel SelectedList
        {
            get => _selectedList;
            set
            {
                _selectedList = value;
                OnPropertyChanged();
            }
        }

        public ItemViewModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand SelectListCommand { get; private set; }

        public DelegateCommand RefreshListsCommand { get; private set; }

        public DelegateCommand LogoutCommand { get; private set; }

        public DelegateCommand AddingNewListCommand { get; private set; }

        public DelegateCommand AddItemCommand { get; private set; }

        public DelegateCommand EditItemCommand { get; private set; }

        public DelegateCommand DeleteItemCommand { get; private set; }

        public DelegateCommand SaveItemEditCommand { get; private set; }

        public DelegateCommand CancelItemEditCommand { get; private set; }

        public DelegateCommand ChangeImageCommand { get; private set; }

        public event EventHandler LogoutSucceeded;

        public event EventHandler StartingItemEdit;

        public event EventHandler FinishingItemEdit;

        public event EventHandler StartingImageChange;

        public MainViewModel(TodoListAPIService service)
        {
            _service = service;

            LogoutCommand = new DelegateCommand(_ => LogoutAsync());
            RefreshListsCommand = new DelegateCommand(_ => LoadListsAsync());
            SelectListCommand = new DelegateCommand(_ => LoadItemsAsync(SelectedList));
            AddingNewListCommand = new DelegateCommand(param => AddingNewList(param as AddingNewItemEventArgs));

            AddItemCommand = new DelegateCommand(
                _ => !(SelectedList is null) && SelectedList.Id != 0,
                _ => AddItem());
            EditItemCommand = new DelegateCommand(_ => !(SelectedItem is null), _ => StartEditItem());
            DeleteItemCommand = new DelegateCommand(_ => !(SelectedItem is null), _ => DeleteItem(SelectedItem));

            SaveItemEditCommand = new DelegateCommand(
                _ => String.IsNullOrEmpty(SelectedItem?[nameof(ItemViewModel.Name)]),
                _ => SaveItemEdit());
            CancelItemEditCommand = new DelegateCommand(_ => CancelItemEdit());
            ChangeImageCommand = new DelegateCommand(_ => StartingImageChange?.Invoke(this, EventArgs.Empty));
        }

        #region Authentication

        private async void LogoutAsync()
        {
            try
            {
                await _service.LogoutAsync();
                OnLogoutSuccess();
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }

        private void OnLogoutSuccess()
        {
            LogoutSucceeded?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Lists

        private void AddingNewList(AddingNewItemEventArgs e)
        {
            var listVm = new ListViewModel { Name = "New list" };
            listVm.EditEnded += ListViewModel_EditEnded;
            e.NewItem = listVm;
        }

        private async void LoadListsAsync()
        {
            try
            {
                Lists = new ObservableCollection<ListViewModel>((await _service.LoadListsAsync()).Select(list =>
                {
                    var listVm = (ListViewModel)list;
                    listVm.EditEnded += ListViewModel_EditEnded;
                    return listVm;
                }));
                Lists.CollectionChanged += Lists_CollectionChanged;
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }

        private async void ListViewModel_EditEnded(object sender, EventArgs e)
        {
            try
            {
                var listVm = sender as ListViewModel;
                if (listVm.Id == 0)
                {
                    var listDto = (ListDto)listVm;
                    await _service.CreateListAsync(listDto);
                    listVm.Id = listDto.Id;
                }
                else
                {
                    await _service.UpdateListAsync((ListDto)listVm);
                }
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }

        private async void Lists_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                var list = (e.OldItems[0] as ListViewModel);
                if (list.Id != 0)
                {
                    try
                    {
                        await _service.DeleteListAsync(list.Id);
                    }
                    catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
                    {
                        OnMessageApplication($"Unexpected error occured! ({ex.Message})");
                    }
                }
            }
        }

        #endregion

        #region Items

        public async void LoadItemsAsync(ListViewModel list)
        {
            if (list is null || list.Id == 0)
            {
                Items = null;
                return;
            }
            try
            {
                Items = new ObservableCollection<ItemViewModel>((await _service.LoadItemsAsync(list.Id)).Select(item => (ItemViewModel)item));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }

        private void AddItem()
        {
            var newItem = new ItemViewModel
            {
                Name = "New Item",
                Deadline = DateTime.Now,
                ListId = SelectedList.Id
            };
            Items.Add(newItem);
            SelectedItem = newItem;
            StartEditItem();
        }

        private void StartEditItem()
        {
            SelectedItem.BeginEdit();
            StartingItemEdit?.Invoke(this, EventArgs.Empty);
        }

        private async void DeleteItem(ItemViewModel item)
        {
            try
            {
                await _service.DeleteItemAsync(item.Id);
                Items.Remove(SelectedItem);
                SelectedItem = null;
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }

        private void CancelItemEdit()
        {
            if (SelectedItem is null || !SelectedItem.IsDirty)
                return;

            if (SelectedItem.Id == 0)
            {
                Items.Remove(SelectedItem);
                SelectedItem = null;
            }
            else
            {
                SelectedItem.CancelEdit();
            }
            FinishingItemEdit?.Invoke(this, EventArgs.Empty);
        }

        private async void SaveItemEdit()
        {
            try
            {
                if (SelectedItem.Id == 0)
                {
                    var itemDto = (ItemDto)SelectedItem;
                    await _service.CreateItemAsync(itemDto);
                    SelectedItem.Id = itemDto.Id;
                    SelectedItem.EndEdit();
                }
                else
                {
                    await _service.UpdateItemAsync((ItemDto)SelectedItem);
                    SelectedItem.EndEdit();
                    if (SelectedItem.ListId != SelectedList.Id)
                    {
                        Items.Remove(SelectedItem);
                        SelectedItem = null;
                    }
                }
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            FinishingItemEdit?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
