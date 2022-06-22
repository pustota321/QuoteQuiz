using Microsoft.AspNetCore.Mvc;
using QuoteQuizBackend.DataAccess.UnitOfWork;
using QuoteQuizBackend.Dtos;
using QuoteQuizBackend.Entities;

namespace QuoteQuizBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuoteController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public QuoteController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<Quote>> GetAll()
        {
            return await _unitOfWork.QuoteRepository.GetAllAsync(e => e.Author);
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateQuote([FromBody]CreateQuoteDto dto)
        {
            var author = await _unitOfWork.AuthorRepository.FindFirstAsync(e => e.FirstName == dto.AuthorFirstName
                                && e.LastName == dto.AuthorLastName);
            var quote = new Quote
            {
                Content = dto.Content,
            };
            await _unitOfWork.QuoteRepository.SaveAsync(quote);
            quote.SetAuthor(author);
            await _unitOfWork.SaveChangesAsync();
            return Ok(quote);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteQuote(int id)
        {
            await _unitOfWork.QuoteRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return Ok(id);
        }
    }
}
