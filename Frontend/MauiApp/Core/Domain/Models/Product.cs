
using System.Text.Json.Serialization;

namespace Frontend.MauiApp.Core.Domain.Models
{
    public class Product
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("photo")]
        public byte[] Photo { get; set; }
        [JsonPropertyName("categoryId")]
        public int CategoryId { get; set; }
        [JsonPropertyName("category")]
        public Category Category { get; set; }
    }
}
