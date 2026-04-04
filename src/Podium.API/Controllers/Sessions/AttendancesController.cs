using Microsoft.AspNetCore.Mvc;
using Podium.Application;
using Podium.Application.Responses;
using Podium.Application.Services;

namespace Podium.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendancesController : ControllerBase
    {
        private readonly SessionServices _sessionServices;

        public AttendancesController(SessionServices sessionServices)
        {
            _sessionServices = sessionServices;
        }

        [HttpGet]
        public async Task<ApiResponse<IEnumerable<AttendanceDto>>> GetAllAttendances() => await _sessionServices.GetAllAttendancesAsync();

        [HttpGet("{id}")]
        public async Task<ApiResponse<AttendanceDto>> GetAttendanceById(int id) => await _sessionServices.GetAttendanceByIdAsync(id);

        [HttpPost]
        public async Task<ApiResponse<AttendanceDto>> CreateAttendance(AttendanceDto attendanceRequest) => await _sessionServices.AddAttendanceAsync(attendanceRequest);

        [HttpPut("{id}")]
        public async Task<ApiResponse<AttendanceDto>> UpdateAttendance(int id, AttendanceDto attendanceRequest) => await _sessionServices.UpdateAttendanceAsync(id, attendanceRequest);

        [HttpDelete("{id}")]
        public async Task<ApiResponse<AttendanceDto>> DeleteAttendance(int id) => await _sessionServices.DeleteAttendanceAsync(id);
    }
}
