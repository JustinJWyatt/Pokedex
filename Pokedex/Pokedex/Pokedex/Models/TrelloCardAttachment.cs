using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Models
{
    public class Preview
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("scaled")]
        public bool Scaled { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("bytes")]
        public int Bytes { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }
    }

    public class TrelloCardAttachment
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("bytes")]
        public int Bytes { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("edgeColor")]
        public string EdgeColor { get; set; }

        [JsonProperty("idMember")]
        public string IdMember { get; set; }

        [JsonProperty("isUpload")]
        public bool IsUpload { get; set; }

        [JsonProperty("mimeType")]
        public string MimeType { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("previews")]
        public List<Preview> Previews { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("pos")]
        public int Pos { get; set; }

        [JsonProperty("fileName")]
        public string FileName { get; set; }
    }
}
