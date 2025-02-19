namespace my_book_shelf_api.Models.Dto
{
    public class BookDtoUpdate
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public DateTime ReleasedDate { get; set; }
        public string Edition { get; set; }
        public string Description { get; set; }
        public string Cover { get; set; }
        public DateTime UpdateDate { get; set; }
        public Guid UpdatedBy { get; set; }

    }
}
