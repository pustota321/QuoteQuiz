using Microsoft.AspNetCore.Mvc;
using QuoteQuizBackend.DataAccess.UnitOfWork;
using QuoteQuizBackend.Dtos;
using QuoteQuizBackend.Entities;

namespace QuoteQuizBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public PlayerController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlayer(string username)
        {
            var player = await _unitOfWork.PlayerRepository.FindFirstAsync(e => e.Username == username);
            return Ok(new PlayerDto
            {
                Username = player.Username,
                RecordScore = player.RecordScore,
            });
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllPlayers()
        {
            var players = (await _unitOfWork.PlayerRepository.GetAllAsync()).OrderByDescending(e => e.RecordScore).ToList();
            return Ok(players);
        }

        [HttpPost]
        public async Task<IActionResult> AddPlayer([FromBody] PlayerDto dto)
        {
            var player = await _unitOfWork.PlayerRepository.FindFirstAsync(e => e.Username == dto.Username);
            if (player is null)
            {
                await _unitOfWork.PlayerRepository.SaveAsync(new Player
                {
                    Username = dto.Username
                });
                await _unitOfWork.SaveChangesAsync();
            }
            return Ok(new PlayerDto
            {
                Username = dto.Username,
                RecordScore = player?.RecordScore,
            });
        }
        [HttpPut]
        [Route("score")]
        public async Task<IActionResult> AddScore([FromBody] PlayerDto dto)
        {
            var player = await _unitOfWork.PlayerRepository.FindFirstAsync(e => e.Username == dto.Username);
            if (player is not null)
            {
                await _unitOfWork.PlayerRepository.UpdateAsync(new Player
                {
                    Id = player.Id,
                    Username = player.Username,
                    RecordScore = dto.RecordScore,
                });
            }
            await _unitOfWork.SaveChangesAsync();
            return Ok(player?.Id);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            await _unitOfWork.PlayerRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return Ok(id);
        }
    }
}
