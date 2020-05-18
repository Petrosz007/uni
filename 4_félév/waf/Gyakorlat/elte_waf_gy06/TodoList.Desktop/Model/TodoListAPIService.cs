using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TodoList.Persistence.DTO;

namespace TodoList.Desktop.Model
{
    public class TodoListAPIService
    {
        private readonly HttpClient _client;

        public TodoListAPIService(string baseAddress)
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(baseAddress)
            };
        }

        #region Authentication

        public async Task<bool> LoginAsync(string name, string password)
        {
            LoginDto user = new LoginDto
            {
                UserName = name,
                Password = password
            };

            HttpResponseMessage response = await _client.PostAsJsonAsync("api/Account/Login", user);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return false;
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        public async Task LogoutAsync()
        {
            HttpResponseMessage response = await _client.PostAsync("api/Account/Logout", null);

            if (response.IsSuccessStatusCode)
            {
                return;
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        #endregion

        #region List

        public async Task<IEnumerable<ListDto>> LoadListsAsync()
        {
            HttpResponseMessage response = await _client.GetAsync("api/Lists/");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<ListDto>>();
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        public async Task CreateListAsync(ListDto list)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/lists/", list);
            list.Id = (await response.Content.ReadAsAsync<ListDto>()).Id;

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        public async Task UpdateListAsync(ListDto list)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"api/lists/{list.Id}", list);

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        public async Task DeleteListAsync(Int32 listId)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"api/lists/{listId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        #endregion

        #region Item

        public async Task<IEnumerable<ItemDto>> LoadItemsAsync(int listId)
        {
            HttpResponseMessage response = await _client.GetAsync(
                QueryHelpers.AddQueryString("api/Items/", "listId", listId.ToString()));

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<ItemDto>>();
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        public async Task CreateItemAsync(ItemDto item)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/items/", item);
            item.Id = (await response.Content.ReadAsAsync<ItemDto>()).Id;

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        public async Task UpdateItemAsync(ItemDto item)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"api/items/{item.Id}", item);

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        public async Task DeleteItemAsync(Int32 itemId)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"api/items/{itemId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        #endregion
    }
}
