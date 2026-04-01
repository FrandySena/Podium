using AutoMapper;
using Podium.Application.Responses;
using Podium.Domain.Entities.Sessions;
using Podium.Infrastructure.Respositories;

namespace Podium.Application.Services
{
    public class SessionServices
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        public SessionServices(IMapper mapper,
            UnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<AttendanceDto>> GetAttendanceByIdAsync(int id)
        {
            var attendance = await _unitOfWork.AttendanceRepository.GetByIdAsync(id);
            if (attendance == null)
            {
                return ApiResponse<AttendanceDto>.FailureResponse("Attendance not found", 404);
            }

            var response = _mapper.Map<AttendanceDto>(attendance);
            return ApiResponse<AttendanceDto>.SuccessResponse(response);
        }

        public async Task<ApiResponse<IEnumerable<AttendanceDto>>> GetAllAttendancesAsync()
        {
            var list = await _unitOfWork.AttendanceRepository.GetAllAsync();
            var response = _mapper.Map<IEnumerable<AttendanceDto>>(list);
            return ApiResponse<IEnumerable<AttendanceDto>>.SuccessResponse(response);
        }

        public async Task<ApiResponse<AttendanceDto>> AddAttendanceAsync(Attendance attendance)
        {
            await _unitOfWork.BeginTransaction();
            await _unitOfWork.AttendanceRepository.AddAsync(attendance);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            var response = _mapper.Map<AttendanceDto>(attendance);
            return ApiResponse<AttendanceDto>.SuccessResponse(response, "Attendance added successfully", 201);
        }

        public async Task<ApiResponse<AttendanceDto>> UpdateAttendanceAsync(Attendance attendance)
        {
            await _unitOfWork.BeginTransaction();
            _unitOfWork.AttendanceRepository.Update(attendance);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            var response = _mapper.Map<AttendanceDto>(attendance);
            return ApiResponse<AttendanceDto>.SuccessResponse(response, "Attendance updated successfully");
        }

        public async Task<ApiResponse<AttendanceDto>> DeleteAttendanceAsync(int id)
        {
            var response = await GetAttendanceByIdAsync(id);
            if (!response.Success) return response;

            await _unitOfWork.BeginTransaction();
            await _unitOfWork.AttendanceRepository.DeleteAsync(id);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();

            return ApiResponse<AttendanceDto>.SuccessResponse(response.Data, "Attendance deleted successfully");
        }

        public async Task<ApiResponse<DebateDto>> GetDebateByIdAsync(int id)
        {
            var debate = await _unitOfWork.DebateRepository.GetByIdAsync(id);
            if (debate == null)
            {
                return ApiResponse<DebateDto>.FailureResponse("Debate not found", 404);
            }

            var response = _mapper.Map<DebateDto>(debate);
            return ApiResponse<DebateDto>.SuccessResponse(response);
        }

        public async Task<IEnumerable<ApiResponse<DebateDto>>> GetAllDebatesAsync()
        {
            var list = await _unitOfWork.DebateRepository.GetAllAsync();
            var response = list.Select(debate => _mapper.Map<DebateDto>(debate)).ToList();
            return response.Select(r => ApiResponse<DebateDto>.SuccessResponse(r));
        }

        public async Task<ApiResponse<DebateDto>> AddDebateAsync(Debate debate)
        {
            await _unitOfWork.BeginTransaction();
            await _unitOfWork.DebateRepository.AddAsync(debate);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            var response = _mapper.Map<DebateDto>(debate);
            return ApiResponse<DebateDto>.SuccessResponse(response, "Debate added successfully", 201);
        }

        public async Task<ApiResponse<DebateDto>> UpdateDebateAsync(Debate debate)
        {
            await _unitOfWork.BeginTransaction();
            _unitOfWork.DebateRepository.Update(debate);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            var response = _mapper.Map<DebateDto>(debate);
            return ApiResponse<DebateDto>.SuccessResponse(response, "Debate updated successfully");
        }

        public async Task<ApiResponse<DebateDto>> DeleteDebateAsync(int id)
        {
            var debate = await GetDebateByIdAsync(id);
            if (debate != null)
            {
                await _unitOfWork.BeginTransaction();
                await _unitOfWork.DebateRepository.DeleteAsync(id);
                await _unitOfWork.Complete();
                await _unitOfWork.CommitTransaction();
            }
            var response = _mapper.Map<DebateDto>(debate.Data);
            return ApiResponse<DebateDto>.SuccessResponse(response, "Debate deleted successfully");
        }

        public async Task<IEnumerable<ApiResponse<DebateDto>>> GetAllDebatesWithDetailsAsync()
        {
            var debates = await _unitOfWork.DebateRepository.GetAllIncludingAsync(
                d => d.Topic,
                d => d.Participants,
                d => d.Juries
            );
            var response = debates.Select(debate => _mapper.Map<DebateDto>(debate)).ToList();
            return response.Select(r => ApiResponse<DebateDto>.SuccessResponse(r));
        }

        public async Task<ApiResponse<DebateDto>> GetDebateWithDetailsByIdAsync(int id)
        {
            var debates = await _unitOfWork.DebateRepository.GetAllIncludingAsync(
                d => d.Topic,
                d => d.Participants,
                d => d.Juries
            );
            var debate = debates.FirstOrDefault(d => d.Id == id);
            var response = _mapper.Map<DebateDto>(debate);
            return ApiResponse<DebateDto>.SuccessResponse(response);
        }

        public async Task<ApiResponse<EvaluationDto>> GetEvaluationByIdAsync(int id)
        {
            var evaluation = await _unitOfWork.EvaluationsRepository.GetByIdAsync(id);
            if (evaluation == null)
                return ApiResponse<EvaluationDto>.FailureResponse("Evaluation not found", 404);

            var response = _mapper.Map<EvaluationDto>(evaluation);
            return ApiResponse<EvaluationDto>.SuccessResponse(response);
        }

        public async Task<IEnumerable<ApiResponse<EvaluationDto>>> GetAllEvaluationsAsync()
        {
            var evaluations = await _unitOfWork.EvaluationsRepository.GetAllAsync();
            var response = evaluations.Select(evaluation => _mapper.Map<EvaluationDto>(evaluation)).ToList();
            return response.Select(r => ApiResponse<EvaluationDto>.SuccessResponse(r));
        }

        public async Task<ApiResponse<EvaluationDto>> AddEvaluationAsync(Evaluation evaluation)
        {
            await _unitOfWork.BeginTransaction();
            await _unitOfWork.EvaluationsRepository.AddAsync(evaluation);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            var response = _mapper.Map<EvaluationDto>(evaluation);
            return ApiResponse<EvaluationDto>.SuccessResponse(response, "Evaluation added successfully", 201);
        }

        public async Task<ApiResponse<EvaluationDto>> UpdateEvaluationAsync(Evaluation evaluation)
        {
            await _unitOfWork.BeginTransaction();
            _unitOfWork.EvaluationsRepository.Update(evaluation);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            var response = _mapper.Map<EvaluationDto>(evaluation);
            return ApiResponse<EvaluationDto>.SuccessResponse(response, "Evaluation updated successfully");
        }

        public async Task<ApiResponse<EvaluationDto>> DeleteEvaluationAsync(int id)
        {
            var evaluation = await GetEvaluationByIdAsync(id);
            if (evaluation != null)
            {
                await _unitOfWork.BeginTransaction();
                await _unitOfWork.EvaluationsRepository.DeleteAsync(id);
                await _unitOfWork.Complete();
                await _unitOfWork.CommitTransaction();
            }
            var response = _mapper.Map<EvaluationDto>(evaluation.Data);
            return ApiResponse<EvaluationDto>.SuccessResponse(response, "Evaluation deleted successfully");
        }

        public async Task<IEnumerable<ApiResponse<TopicDto>>> GetAllTopicsAsync()
        {
            var topics = await _unitOfWork.Topics.GetAllAsync();
            var response = topics.Select(topic => _mapper.Map<TopicDto>(topic)).ToList();
            return response.Select(r => ApiResponse<TopicDto>.SuccessResponse(r));
        }

        public async Task<ApiResponse<TopicDto>> GetTopicByIdAsync(int id)
        {
            var topic = await _unitOfWork.Topics.GetByIdAsync(id);
            if (topic == null)
                return ApiResponse<TopicDto>.FailureResponse("Topic not found", 404);
            var response = _mapper.Map<TopicDto>(topic);
            return ApiResponse<TopicDto>.SuccessResponse(response);
        }

        public async Task<ApiResponse<TopicDto>> AddTopicAsync(Topic topic)
        {
            await _unitOfWork.BeginTransaction();
            await _unitOfWork.Topics.AddAsync(topic);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            var response = _mapper.Map<TopicDto>(topic);
            return ApiResponse<TopicDto>.SuccessResponse(response, "Topic added successfully", 201);
        }

        public async Task<ApiResponse<TopicDto>> UpdateTopicAsync(Topic topic)
        {
            await _unitOfWork.BeginTransaction();
            _unitOfWork.Topics.Update(topic);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            var response = _mapper.Map<TopicDto>(topic);
            return ApiResponse<TopicDto>.SuccessResponse(response, "Topic updated successfully");
        }

        public async Task<ApiResponse<TopicDto>> DeleteTopicAsync(int id)
        {
            var topic = await GetTopicByIdAsync(id);
            if (topic != null)
            {
                await _unitOfWork.BeginTransaction();
                await _unitOfWork.Topics.DeleteAsync(id);
                await _unitOfWork.Complete();
                await _unitOfWork.CommitTransaction();
            }
            var response = _mapper.Map<TopicDto>(topic.Data);
            return ApiResponse<TopicDto>.SuccessResponse(response, "Topic deleted successfully");
        }

    }
}

