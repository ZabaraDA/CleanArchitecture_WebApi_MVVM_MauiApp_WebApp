using System.Text.Json.Serialization;

namespace Backend.Core.Domain.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
