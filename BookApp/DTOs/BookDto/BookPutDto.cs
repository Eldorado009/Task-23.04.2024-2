namespace BookApp.DTOs.BookDto
{
    public class BookPutDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double SalePrice { get; set; }
        public double CostPrice { get; set; }
        public string ImageUrl { get; set; }
        public bool IsDeleted { get; set; } 
    }
}
