using Newtonsoft.Json;
using Pokedex.Constants;
using Pokedex.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Client
{
    public class TrelloAPIClient : ITrelloAPIClient
    {
        HttpClient _client;

        public TrelloAPIClient()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(TrelloAPI.BaseUrl)
            };
        }

        public async Task<List<TrelloBoard>> GetBoardsAsync()
        {
            var boards = await _client.GetAsync($"members/me/boards?fields=name,url&key={TrelloAPI.API_KEY}&token={TrelloAPI.TOKEN}");

            var content = await boards.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<TrelloBoard>>(content);
        }

        public async Task<TrelloBoard> GetBoardAsync(string boardId)
        {
            var lists = await _client.GetAsync($"boards/{boardId}?lists=all&cards=all&key={TrelloAPI.API_KEY}&token={TrelloAPI.TOKEN}");

            var content = await lists.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TrelloBoard>(content);
        }

        public async Task<List<TrelloCard>> GetCardsAsync(string listId)
        {
            var cards = await _client.GetAsync($"lists/{listId}/cards?key={TrelloAPI.API_KEY}&token={TrelloAPI.TOKEN}");

            var content = await cards.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<TrelloCard>>(content);
        }

        public async Task<TrelloCard> GetCardAsync(string cardId, bool attachments)
        {
            var cards = await _client.GetAsync($"cards/{cardId}?{(attachments ? "attachments=true&" : string.Empty)}key={TrelloAPI.API_KEY}&token={TrelloAPI.TOKEN}");

            var content = await cards.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TrelloCard>(content);
        }

        public async Task<HttpResponseMessage> PostCardAsync(TrelloCard model)
        {
            var content = JsonConvert.SerializeObject(model);
            var buffer = Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return await _client.PostAsync($"cards?key={TrelloAPI.API_KEY}&token={TrelloAPI.TOKEN}", byteContent);
        }

        public async Task<HttpResponseMessage> PutBoardMembersAsync(string boardId, string email)
        {
            return await _client.PutAsync($"boards/{boardId}/members?email={email}&key={TrelloAPI.API_KEY}&token={TrelloAPI.TOKEN}", null);
        }

        public async Task<HttpResponseMessage> PostCardAttachmentAsync(string cardId, byte[] bytes)
        {
            MultipartFormDataContent form = new MultipartFormDataContent();
            ByteArrayContent lFileContent = new ByteArrayContent(bytes);

            form.Add(new StringContent(TrelloAPI.TOKEN), "token");
            form.Add(new StringContent(TrelloAPI.API_KEY), "key");
            form.Add(new StringContent("image/jpg"), "mimeType");
            form.Add(new StringContent("Attachment.jpg"), "name");
            form.Add(lFileContent, "file");

            return await _client.PostAsync($"cards/{cardId}/attachments", form);
        }
    }
}
