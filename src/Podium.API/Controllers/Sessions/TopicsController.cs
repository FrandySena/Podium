using Microsoft.AspNetCore.Mvc;
using Podium.Application;
using Podium.Application.Responses;
using Podium.Application.Services;

namespace Podium.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TopicsController : ControllerBase
    {
        private readonly SessionServices _sessionServices;

        public TopicsController(SessionServices sessionServices)
        {
            _sessionServices = sessionServices;
        }

        [HttpGet]
        public async Task<ApiResponse<IEnumerable<TopicDto>>> GetAllTopics() => await _sessionServices.GetAllTopicsAsync();

        [HttpGet("{id}")]
        public async Task<ApiResponse<TopicDto>> GetTopicById(int id) => await _sessionServices.GetTopicByIdAsync(id);

        [HttpPost]
        public async Task<ApiResponse<TopicDto>> CreateTopic(TopicDto topicRequest) => await _sessionServices.AddTopicAsync(topicRequest);

        [HttpPut("{id}")]
        public async Task<ApiResponse<TopicDto>> UpdateTopic(int id, TopicDto topicRequest) => await _sessionServices.UpdateTopicAsync(id, topicRequest);

        [HttpDelete("{id}")]
        public async Task<ApiResponse<TopicDto>> DeleteTopic(int id) => await _sessionServices.DeleteTopicAsync(id);
    }
}
