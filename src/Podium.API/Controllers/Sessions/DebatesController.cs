using Microsoft.AspNetCore.Mvc;
using Podium.Application;
using Podium.Application.Responses;
using Podium.Application.Services;

namespace Podium.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DebatesController : ControllerBase
    {
        private readonly SessionServices _sessionServices;

        public DebatesController(SessionServices sessionServices)
        {
            _sessionServices = sessionServices;
        }

        [HttpGet]
        public async Task<ApiResponse<IEnumerable<DebateDto>>> GetAllDebates() => await _sessionServices.GetAllDebatesAsync();

        [HttpGet("{id}")]
        public async Task<ApiResponse<DebateDto>> GetDebateById(int id) => await _sessionServices.GetDebateByIdAsync(id);

        [HttpGet("AllDebatesWithDetail")]
        public async Task<ApiResponse<IEnumerable<DebateDto>>> GetAllDebatesWithDetail() => await _sessionServices.GetAllDebatesWithDetailsAsync();

        [HttpPost]
        public async Task<ApiResponse<DebateDto>> AddDebate(DebateDto debateRequest) => await _sessionServices.AddDebateAsync(debateRequest);

        [HttpGet("GetDebateWithDetailsByID/{id}")]
        public async Task<ApiResponse<DebateDto>> GetDebateWithDetails(int id) => await _sessionServices.GetDebateWithDetailsByIdAsync(id);

        [HttpGet("GetJuriesByDebateId/{id}")]
        public async Task<ApiResponse<IEnumerable<DebateJuriesDto>>> GetJuriesByDebateId(int id) => await _sessionServices.GetJueriesByDebateAsync(id);

        [HttpGet("GetParticipantsByDebateId/{id}")]
        public async Task<ApiResponse<IEnumerable<DebateParticipantsDto>>> GetParticipantsByDebateId(int id) => await _sessionServices.GetParticipantsByDebateAsync(id);

        [HttpPut("{id}")]
        public async Task<ApiResponse<DebateDto>> UpdateDebate(int id, DebateDto debateRequest) => await _sessionServices.UpdateDebateAsync(id, debateRequest);

        [HttpDelete("{id}")]
        public async Task<ApiResponse<DebateDto>> DeleteDebate(int id) => await _sessionServices.DeleteDebateAsync(id);

        [HttpPost("AddParticipantToDebate/{debateId}/{participantId}/{role}")]
        public async Task<ApiResponse<DebateDto>> AddParticipantToDebate(int debateId, int participantId, string role) => await _sessionServices.AddParticipantToDebateAsync(debateId, participantId, role);

        [HttpPost("AddJuryToDebate/{debateId}/{juryId}")]
        public async Task<ApiResponse<DebateDto>> AddJuryToDebate(int debateId, int juryId) => await _sessionServices.AddJuryToDebateAsync(debateId, juryId);
    }
}
