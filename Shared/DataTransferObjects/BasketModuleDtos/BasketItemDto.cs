namespace Shared.DataTransferObjects.BasketModuleDtos
{
    public class BasketItemDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;

        public decimal Price { get; set; }

        public int Quntity { get; set; }
    }
}