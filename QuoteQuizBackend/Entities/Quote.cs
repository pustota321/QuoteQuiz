namespace QuoteQuizBackend.Entities
{
    public class Quote
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public Author? Author { get; set; }

        public void SetAuthor(Author author)
        {
            Author = author;
        }

    }
}
