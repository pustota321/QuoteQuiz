using Microsoft.AspNetCore.Mvc;
using QuoteQuizBackend.DataAccess.UnitOfWork;
using QuoteQuizBackend.Dtos;
using QuoteQuizBackend.Exstensions;

namespace QuoteQuizBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public QuestionController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _unitOfWork.QuoteRepository.GetAllAsync(e => e.Author));
        }

        [HttpGet]
        [Route("random")]
        public async Task<IActionResult> GetRandomQuestion()
        {
            var questions = await _unitOfWork.QuoteRepository.GetAllAsync(e => e.Author);
            var authors = await _unitOfWork.AuthorRepository.GetAllAsync();

            var randomQuestion = questions.RandomElement();
            var firstAuthor = authors.RandomElement();
            var secondAuthor = authors.RandomElement();
            QuestionDto questionDto = new()
            {
                Quote = randomQuestion.Content,
                Authors = new List<AuthorDto>
                {
                    new AuthorDto
                    {
                        FirstName = randomQuestion.Author.FirstName,
                        LastName = randomQuestion.Author.LastName,
                        IsAnswer = true
                    },
                    new AuthorDto
                    {
                        FirstName = firstAuthor.FirstName,
                        LastName= firstAuthor.LastName,
                        IsAnswer= false
                    },
                    new AuthorDto
                    {
                        FirstName= secondAuthor.FirstName,
                        LastName = secondAuthor.LastName,
                        IsAnswer = false
                    }
                }
            };
            

            return Ok(questionDto);
            
        }
    }
}
