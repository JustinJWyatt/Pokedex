using Pokedex.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Client
{
    public interface ITrelloAPIClient
    {
        Task<List<TrelloBoard>> GetBoardsAsync();
        Task<TrelloBoard> GetBoardAsync(string boardId);
        Task<List<TrelloCard>> GetCardsAsync(string listId);
        Task<TrelloCard> GetCardAsync(string cardId, bool attachments);
        Task<HttpResponseMessage> PostCardAsync(TrelloCard model);
        Task<HttpResponseMessage> PutBoardMembersAsync(string boardId, string email);
        Task<HttpResponseMessage> PostCardAttachmentAsync(string cardId, byte[] bytes);
    }
}
