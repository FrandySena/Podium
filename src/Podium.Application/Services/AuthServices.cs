using Podium.Application.Dtos.Auth;
using Podium.Application.Responses;
using Podium.Infrastructure.Respositories;

namespace Podium.Application.Services
{
    public class AuthServices
    {
        private readonly UnitOfWork _unitOfWork;

        public AuthServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<string>> LoginAsync(LoginDto loginDto)
        {
            var participants = await _unitOfWork.ParticipantRepository.GetAllAsync();
            var user = participants.FirstOrDefault(p => p.Email == loginDto.Email && p.Password == loginDto.Password);

            if (user == null) return ApiResponse<string>.FailureResponse("Wrong email or password");

            return ApiResponse<string>.SuccessResponse("Welcome.");
        }
    }
}
