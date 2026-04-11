using Microsoft.AspNetCore.Mvc;
using Podium.Application;
using Podium.Application.Responses;
using Podium.Application.Services;

namespace Podium.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvaluationsController : ControllerBase
    {
        private readonly SessionServices _sessionServices;

        public EvaluationsController(SessionServices sessionServices)
        {
            _sessionServices = sessionServices;
        }

        [HttpGet]
        public async Task<ApiResponse<IEnumerable<EvaluationDto>>> GetAllevaluations() => await _sessionServices.GetAllEvaluationsAsync();

        [HttpGet("{id}")]
        public async Task<ApiResponse<EvaluationDto>> GetEvaluationById(int id) => await _sessionServices.GetEvaluationByIdAsync(id);

        [HttpPost]
        public async Task<ApiResponse<EvaluationDto>> CreateEvaluation(EvaluationDto evaluationRequest) => await _sessionServices.AddEvaluationAsync(evaluationRequest);

        [HttpPut("{id}")]
        public async Task<ApiResponse<EvaluationDto>> UpdateEvaluation(int id, EvaluationDto evaluationRequest) => await _sessionServices.UpdateEvaluationAsync(id, evaluationRequest);

        [HttpDelete("{id}")]
        public async Task<ApiResponse<EvaluationDto>> DeleteEvaluation(int id) => await _sessionServices.DeleteEvaluationAsync(id);
    }
}
