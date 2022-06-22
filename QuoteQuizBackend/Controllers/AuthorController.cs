using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuoteQuizBackend.DataAccess.UnitOfWork;
using QuoteQuizBackend.Dtos;
using QuoteQuizBackend.Entities;

namespace QuoteQuizBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public AuthorController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _unitOfWork.AuthorRepository.GetAllAsync());
        }
        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorDto dto)
        {
            await _unitOfWork.AuthorRepository.SaveAsync(new Author
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
            });
            return Ok(dto);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            await _unitOfWork.QuoteRepository.FindAllAsync(e => e.Author.Id == id);
            await _unitOfWork.AuthorRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}
