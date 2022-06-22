using QuoteQuizBackend.Entities;

namespace QuoteQuizBackend.Dtos
{
    public class QuestionDto
    {
        public string Quote { get; set; }
        public List<AuthorDto> Authors { get; set; }
    }
}
