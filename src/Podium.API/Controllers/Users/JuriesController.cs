using Microsoft.AspNetCore.Mvc;
using Podium.Application;
using Podium.Application.Responses;
using Podium.Application.Services;

namespace Podium.API.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public class JuriesController : ControllerBase
    {
        private readonly UsersServices _usersServices;

        public JuriesController(UsersServices usersServices)
        {
            _usersServices = usersServices;
        }

        [HttpGet]
        public async Task<ApiResponse<IEnumerable<JuryDto>>> GetAllJuries() => await _usersServices.GetAllJuriesAsync();

        [HttpGet("{id}")]
        public async Task<ApiResponse<JuryDto>> GetJuryById(int id) => await _usersServices.GetJuryByIdAsync(id);

        [HttpPost]
        public async Task<ApiResponse<JuryDto>> AddJury(JuryDto juryRequest) => await _usersServices.AddJuryAsync(juryRequest);

        [HttpPut("{id}")]
        public async Task<ApiResponse<JuryDto>> UpdateJury(int id, JuryDto juryRequest) => await _usersServices.UpdateJuryAsync(id, juryRequest);

        [HttpDelete("{id}")]
        public async Task<ApiResponse<JuryDto>> DeleteJury(int id) => await _usersServices.DeleteJuryAsync(id);
    }
}
