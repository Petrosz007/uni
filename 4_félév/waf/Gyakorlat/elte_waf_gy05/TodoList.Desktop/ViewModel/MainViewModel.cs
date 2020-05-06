using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using TodoList.Desktop.Model;
using TodoList.Persistence.DTO;

namespace TodoList.Desktop.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<ListDto> _lists;
        private ObservableCollection<ItemDto> _items;
        private readonly TodoListAPIService _service;

        public ObservableCollection<ListDto> Lists
        {
            get => _lists;
            set
            {
                _lists = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ItemDto> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand SelectCommand { get; private set; }

        public DelegateCommand RefreshListsCommand { get; private set; }

        public MainViewModel(TodoListAPIService service)
        {
            _service = service;

            RefreshListsCommand = new DelegateCommand(_ => LoadListsAsync());
            SelectCommand = new DelegateCommand(param => LoadItemsAsync((param as ListDto)));
        }

        public async void LoadListsAsync()
        {
            try
            {
                Lists = new ObservableCollection<ListDto>(await _service.LoadListsAsync());
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }

        public async void LoadItemsAsync(ListDto list)
        {
            if (list is null)
                return;
            try
            {
                Items = new ObservableCollection<ItemDto>(await _service.LoadItemsAsync(list.Id));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }
    }
}
