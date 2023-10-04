namespace Backend.Presentation.DataTransferObjects.Products
{
    public class ProductUpdateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CategoryId { get; set; }
    }
}
