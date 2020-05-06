using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<ListDto>> LoadListsAsync()
        {
            HttpResponseMessage response = await _client.GetAsync("api/Lists/");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<ListDto>>();
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

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
    }
}
