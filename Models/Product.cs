﻿using System.Text.Json.Serialization;

namespace Backend.Core.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
