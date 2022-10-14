using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Attachments
    {
        [JsonProperty("perBoard")]
        public PerBoard PerBoard { get; set; }
    }

    public class AttachmentsByType
    {
        [JsonProperty("trello")]
        public Trello Trello { get; set; }
    }

    public class Badges
    {
        [JsonProperty("attachmentsByType")]
        public AttachmentsByType AttachmentsByType { get; set; }

        [JsonProperty("location")]
        public bool Location { get; set; }

        [JsonProperty("votes")]
        public int Votes { get; set; }

        [JsonProperty("viewingMemberVoted")]
        public bool ViewingMemberVoted { get; set; }

        [JsonProperty("subscribed")]
        public bool Subscribed { get; set; }

        [JsonProperty("fogbugz")]
        public string Fogbugz { get; set; }

        [JsonProperty("checkItems")]
        public int CheckItems { get; set; }

        [JsonProperty("checkItemsChecked")]
        public int CheckItemsChecked { get; set; }

        [JsonProperty("comments")]
        public int Comments { get; set; }

        [JsonProperty("attachments")]
        public int Attachments { get; set; }

        [JsonProperty("description")]
        public bool Description { get; set; }

        [JsonProperty("due")]
        public string Due { get; set; }

        [JsonProperty("start")]
        public string Start { get; set; }

        [JsonProperty("dueComplete")]
        public bool DueComplete { get; set; }
    }

    public class Cover
    {
        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("idUploadedBackground")]
        public bool? IdUploadedBackground { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("brightness")]
        public string Brightness { get; set; }

        [JsonProperty("isTemplate")]
        public bool IsTemplate { get; set; }
    }

    public class DescData
    {
        [JsonProperty("emoji")]
        public Emoji Emoji { get; set; }
    }

    public class Emoji
    {
    }

    public class IdChecklist
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class IdLabel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("idBoard")]
        public string IdBoard { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }
    }

    public class Limits
    {
        [JsonProperty("attachments")]
        public Attachments Attachments { get; set; }
    }

    public class PerBoard
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("disableAt")]
        public int DisableAt { get; set; }

        [JsonProperty("warnAt")]
        public int WarnAt { get; set; }
    }

    public class TrelloCard
    {
        public TrelloCard() { }

        public TrelloCard(string listId)
        {
            IdList = listId;
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("idBoard")]
        public string IdBoard { get; set; }

        [JsonProperty("idList")]
        public string IdList { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("attachments")]
        public List<TrelloCardAttachment> Attachments { get; set; }
    }

    public class Trello
    {
        [JsonProperty("board")]
        public int Board { get; set; }

        [JsonProperty("card")]
        public int Card { get; set; }
    }
}
