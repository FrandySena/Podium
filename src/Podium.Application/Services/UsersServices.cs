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

        public async Task<ApiResponse<JuryDto>> AddJuryAsync(JuryDto juryRequest)
        {
            if (string.IsNullOrEmpty(juryRequest.Name) || string.IsNullOrEmpty(juryRequest.LastName) || string.IsNullOrEmpty(juryRequest.Email) || string.IsNullOrEmpty(juryRequest.Password))
            {
                return ApiResponse<JuryDto>.FailureResponse("Name, LastName, Email, and Password are required fields", 400);
            }

            if (juryRequest.Age <= 0)
            {
                return ApiResponse<JuryDto>.FailureResponse("Age must be a positive integer", 400);
            }

            await _unitOfWork.BeginTransaction();
            var jury = _mapper.Map<Jury>(juryRequest);
            await _unitOfWork.JuryRepository.AddAsync(jury);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            var respose = _mapper.Map<JuryDto>(jury);
            return ApiResponse<JuryDto>.SuccessResponse(respose, "Jury added successfully", 201);
        }
        public async Task<ApiResponse<JuryDto>> UpdateJuryAsync(int id, JuryDto juryRequest)
        {
            if (string.IsNullOrEmpty(juryRequest.Name) || string.IsNullOrEmpty(juryRequest.LastName) || string.IsNullOrEmpty(juryRequest.Email) || string.IsNullOrEmpty(juryRequest.Password))
            {
                return ApiResponse<JuryDto>.FailureResponse("Name, LastName, Email, and Password are required fields", 400);
            }

            if (id != juryRequest.Id)
            {
                return ApiResponse<JuryDto>.FailureResponse("Jury ID mismatch for update", 400);
            }

            if (juryRequest == null)
            {
                return ApiResponse<JuryDto>.FailureResponse("Jury data is required for update", 400);
            }

            if (juryRequest.Age <= 0)
            {
                return ApiResponse<JuryDto>.FailureResponse("Age must be a positive integer", 400);
            }

            var existingJury = await _unitOfWork.JuryRepository.GetByIdAsync(id);
            if (existingJury == null)
            {
                return ApiResponse<JuryDto>.FailureResponse("Jury not found", 404);
            }

            await _unitOfWork.BeginTransaction();
            _mapper.Map(juryRequest, existingJury);

            _unitOfWork.JuryRepository.Update(existingJury);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            var respose = _mapper.Map<JuryDto>(existingJury);
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

        public async Task<ApiResponse<ParticipantDto>> AddParticipantAsync(ParticipantDto participantRequest)
        {
            if (string.IsNullOrEmpty(participantRequest.Name) || string.IsNullOrEmpty(participantRequest.LastName) || string.IsNullOrEmpty(participantRequest.Email) || string.IsNullOrEmpty(participantRequest.Password))
            {
                return ApiResponse<ParticipantDto>.FailureResponse("Name, LastName, Email, and Password are required fields", 400);
            }

            if (participantRequest.Age <= 0)
            {
                return ApiResponse<ParticipantDto>.FailureResponse("Age must be a positive integer", 400);
            }

            await _unitOfWork.BeginTransaction();
            var participant = _mapper.Map<Participant>(participantRequest);
            await _unitOfWork.ParticipantRepository.AddAsync(participant);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            var respose = _mapper.Map<ParticipantDto>(participant);
            return ApiResponse<ParticipantDto>.SuccessResponse(respose, "Participant added successfully", 201);
        }
        public async Task<ApiResponse<ParticipantDto>> UpdateParticipantAsync(int id, ParticipantDto participantDto)
        {
            if (string.IsNullOrEmpty(participantDto.Name) || string.IsNullOrEmpty(participantDto.LastName) || string.IsNullOrEmpty(participantDto.Email) || string.IsNullOrEmpty(participantDto.Password))
            {
                return ApiResponse<ParticipantDto>.FailureResponse("Name, LastName, Email, and Password are required fields", 400);
            }

            if (id != participantDto.Id)
            {
                return ApiResponse<ParticipantDto>.FailureResponse("Participant ID mismatch for update", 400);
            }

            if (participantDto == null)
            {
                return ApiResponse<ParticipantDto>.FailureResponse("Participant data is required for update", 400); 
            }

            if (participantDto.Age <= 0)
            {
                return ApiResponse<ParticipantDto>.FailureResponse("Age must be a positive integer", 400);
            }

            var existingParticipant = await _unitOfWork.ParticipantRepository.GetByIdAsync(id);
            if (existingParticipant == null)
            {
                return ApiResponse<ParticipantDto>.FailureResponse("Participant not found", 404);
            }

            await _unitOfWork.BeginTransaction();

            _mapper.Map(participantDto, existingParticipant);

            _unitOfWork.ParticipantRepository.Update(existingParticipant);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();

            var respose = _mapper.Map<ParticipantDto>(existingParticipant);
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

