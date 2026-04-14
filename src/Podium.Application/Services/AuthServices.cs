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
            var userP = participants.FirstOrDefault(p => p.Email == loginDto.Email && p.Password == loginDto.Password);

            if (userP != null)
            {
                return ApiResponse<string>.SuccessResponse("Welcome, Participant.");
            }

            var juries = await _unitOfWork.JuryRepository.GetAllAsync();
            var userJ = juries.FirstOrDefault(j => j.Email == loginDto.Email && j.Password == loginDto.Password);

            if (userJ != null)
            {
                return ApiResponse<string>.SuccessResponse("Welcome, Jury.");
            }

            return ApiResponse<string>.FailureResponse("Wrong email or password");
        }
    }
}