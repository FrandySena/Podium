using AutoMapper;
using Podium.Application.Responses;
using Podium.Domain.Entities.Users;
using Podium.Infrastructure.Respositories;

namespace Podium.Application.Services
{
    public class UsersServices
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        public UsersServices(IMapper mapper, UnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<UserDto>> GetUserByIdAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user == null)
            {
                return ApiResponse<UserDto>.FailureResponse("User not found", 404);
            }

            var respose = _mapper.Map<UserDto>(user);
            return ApiResponse<UserDto>.SuccessResponse(respose);
        }
        public async Task<ApiResponse<IEnumerable<UserDto>>> GetAllUsersAsync()
        {
            var list = await _unitOfWork.UserRepository.GetAllAsync();
            var response = _mapper.Map<IEnumerable<UserDto>>(list);
            return ApiResponse<IEnumerable<UserDto>>.SuccessResponse(response);
        }

        public async Task<ApiResponse<UserDto>> AddUserAsync(User user)
        {
            await _unitOfWork.BeginTransaction();
            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            var respose = _mapper.Map<UserDto>(user);
            return ApiResponse<UserDto>.SuccessResponse(respose, "User added successfully", 201);
        }
        public async Task<ApiResponse<UserDto>> UpdateUserAsync(User user)
        {
            await _unitOfWork.BeginTransaction();
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            var respose = _mapper.Map<UserDto>(user);
            return ApiResponse<UserDto>.SuccessResponse(respose, "User updated successfully");
        }

        public async Task<ApiResponse<UserDto>> DeleteUserAsync(int id)
        {
            var response = await GetUserByIdAsync(id);
            if (response.Data == null)
            {
                return ApiResponse<UserDto>.FailureResponse("User not found", 404);
            }
            await _unitOfWork.BeginTransaction();
            await _unitOfWork.UserRepository.DeleteAsync(id);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            return ApiResponse<UserDto>.SuccessResponse(response.Data, "User deleted successfully");
        }

        public async Task<ApiResponse<JuryDto>> GetJuryByIdAsync(int id)
        {
            var jury = await _unitOfWork.JuryRepository.GetByIdAsync(id);
            if (jury == null)
            {
                return ApiResponse<JuryDto>.FailureResponse("Jury not found", 404);
            }
            var respose = _mapper.Map<JuryDto>(jury);
            return ApiResponse<JuryDto>.SuccessResponse(respose);
        }

        public async Task<ApiResponse<IEnumerable<JuryDto>>> GetAllJuriesAsync()
        {
            var list = await _unitOfWork.JuryRepository.GetAllAsync();
            var response = _mapper.Map<IEnumerable<JuryDto>>(list);
            return ApiResponse<IEnumerable<JuryDto>>.SuccessResponse(response);
        }

        public async Task<ApiResponse<JuryDto>> AddJuryAsync(Jury jury)
        {
            await _unitOfWork.BeginTransaction();
            await _unitOfWork.JuryRepository.AddAsync(jury);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            var respose = _mapper.Map<JuryDto>(jury);
            return ApiResponse<JuryDto>.SuccessResponse(respose, "Jury added successfully", 201);
        }
        public async Task<ApiResponse<JuryDto>> UpdateJuryAsync(Jury jury)
        {
            await _unitOfWork.BeginTransaction();
            _unitOfWork.JuryRepository.Update(jury);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            var respose = _mapper.Map<JuryDto>(jury);
            return ApiResponse<JuryDto>.SuccessResponse(respose, "Jury updated successfully");
        }

        public async Task<ApiResponse<JuryDto>> DeleteJuryAsync(int id)
        {
            var response = await GetJuryByIdAsync(id);
            if (response.Data == null)
            {
                return ApiResponse<JuryDto>.FailureResponse("Jury not found", 404);
            }
            await _unitOfWork.BeginTransaction();
            await _unitOfWork.JuryRepository.DeleteAsync(id);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            return ApiResponse<JuryDto>.SuccessResponse(response.Data, "Jury deleted successfully");
        }

        public async Task<ApiResponse<ParticipantDto>> GetParticipantByIdAsync(int id)
        {
            var participant = await _unitOfWork.ParticipantRepository.GetByIdAsync(id);
            if (participant == null)
            {
                return ApiResponse<ParticipantDto>.FailureResponse("Participant not found", 404);
            }
            var respose = _mapper.Map<ParticipantDto>(participant);
            return ApiResponse<ParticipantDto>.SuccessResponse(respose);
        }

        public async Task<ApiResponse<IEnumerable<ParticipantDto>>> GetAllParticipantsAsync()
        {
            var list = await _unitOfWork.ParticipantRepository.GetAllAsync();
            var response = _mapper.Map<IEnumerable<ParticipantDto>>(list);
            return ApiResponse<IEnumerable<ParticipantDto>>.SuccessResponse(response);
        }

        public async Task<ApiResponse<ParticipantDto>> AddParticipantAsync(Participant participant)
        {
            await _unitOfWork.BeginTransaction();
            await _unitOfWork.ParticipantRepository.AddAsync(participant);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            var respose = _mapper.Map<ParticipantDto>(participant);
            return ApiResponse<ParticipantDto>.SuccessResponse(respose, "Participant added successfully", 201);
        }
        public async Task<ApiResponse<ParticipantDto>> UpdateParticipantAsync(Participant participant)
        {
            await _unitOfWork.BeginTransaction();
            _unitOfWork.ParticipantRepository.Update(participant);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            var respose = _mapper.Map<ParticipantDto>(participant);
            return ApiResponse<ParticipantDto>.SuccessResponse(respose, "Participant updated successfully");
        }

        public async Task<ApiResponse<ParticipantDto>> DeleteParticipantAsync(int id)
        {
            var response = await GetParticipantByIdAsync(id);
            if (response.Data == null)
            {
                return ApiResponse<ParticipantDto>.FailureResponse("Participant not found", 404);
            }
            await _unitOfWork.BeginTransaction();
            await _unitOfWork.ParticipantRepository.DeleteAsync(id);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            return ApiResponse<ParticipantDto>.SuccessResponse(response.Data, "Participant deleted successfully");
        }
    }
}

