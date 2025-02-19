namespace my_book_shelf_api.Models.Dto
{
    public class BookDtoCreate
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public DateTime ReleasedDate { get; set; }
        public string Edition { get; set; }
        public string Description { get; set; }
        public string Cover { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public Guid CreatedBy { get; set; }

    }
}
