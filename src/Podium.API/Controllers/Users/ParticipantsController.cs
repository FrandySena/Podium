using Microsoft.AspNetCore.Mvc;
using Podium.Application;
using Podium.Application.Responses;
using Podium.Application.Services;

namespace Podium.API.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipantsController : ControllerBase
    {
        private readonly UsersServices _usersServices;

        public ParticipantsController(UsersServices usersServices)
        {
            _usersServices = usersServices;
        }

        [HttpGet]
        public async Task<ApiResponse<IEnumerable<ParticipantDto>>> GetAllParticipants() => await _usersServices.GetAllParticipantsAsync();

        [HttpGet("{id}")]
        public async Task<ApiResponse<ParticipantDto>> GetParticipantById(int id) => await _usersServices.GetParticipantByIdAsync(id);

        [HttpPost]
        public async Task<ApiResponse<ParticipantDto>> AddParticipant(ParticipantDto participantRequest) => await _usersServices.AddParticipantAsync(participantRequest);

        [HttpPut("{id}")]
        public async Task<ApiResponse<ParticipantDto>> UpdateParticipant(int id, ParticipantDto participantRequest) => await _usersServices.UpdateParticipantAsync(id, participantRequest);

        [HttpDelete("{id}")]
        public async Task<ApiResponse<ParticipantDto>> DeleteParticipant(int id) => await _usersServices.DeleteParticipantAsync(id);
    }
}
