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

        //---------------------------------------------------------------------------------
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

        public async Task<ApiResponse<AttendanceDto>> AddAttendanceAsync(AttendanceDto attendanceDto)
        {
            if (string.IsNullOrEmpty(attendanceDto.ParticipantId.ToString()) || string.IsNullOrEmpty(attendanceDto.DebateId.ToString()))
            {
                return ApiResponse<AttendanceDto>.FailureResponse("Participant ID and Debate ID are required", 400);
            }

            if (attendanceDto.IsPresent)
            {
                var participantExists = await _unitOfWork.ParticipantRepository.GetByIdAsync(attendanceDto.ParticipantId);
                var debateExists = await _unitOfWork.DebateRepository.GetByIdAsync(attendanceDto.DebateId);
                if (participantExists == null || debateExists == null)
                {
                    return ApiResponse<AttendanceDto>.FailureResponse("Valid Participant ID and Debate ID are required", 400);
                }
            }

            await _unitOfWork.BeginTransaction();
            var attendance = _mapper.Map<Attendance>(attendanceDto);
            await _unitOfWork.AttendanceRepository.AddAsync(attendance);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            var response = _mapper.Map<AttendanceDto>(attendance);
            return ApiResponse<AttendanceDto>.SuccessResponse(response, "Attendance added successfully", 201);
        }

        public async Task<ApiResponse<AttendanceDto>> UpdateAttendanceAsync(int id, AttendanceDto attendanceDto)
        {
            if (string.IsNullOrEmpty(attendanceDto.ParticipantId.ToString()) || string.IsNullOrEmpty(attendanceDto.DebateId.ToString()))
            {
                return ApiResponse<AttendanceDto>.FailureResponse("Participant ID and Debate ID are required for update", 400);
            }

            if (attendanceDto.IsPresent)
            {
                var participantExists = await _unitOfWork.ParticipantRepository.GetByIdAsync(attendanceDto.ParticipantId);
                var debateExists = await _unitOfWork.DebateRepository.GetByIdAsync(attendanceDto.DebateId);
                if (participantExists == null || debateExists == null)
                {
                    return ApiResponse<AttendanceDto>.FailureResponse("Valid Participant ID and Debate ID are required for update", 400);
                }
            }

            if (attendanceDto == null)
            {
                return ApiResponse<AttendanceDto>.FailureResponse("Attendance data is required for update", 400);
            }
            if (id != attendanceDto.Id)
            {
                return ApiResponse<AttendanceDto>.FailureResponse("Valid attendance ID is required for update", 400);
            }

            var existingAttendance = await _unitOfWork.AttendanceRepository.GetByIdAsync(id);
            if (existingAttendance == null)
            {
                return ApiResponse<AttendanceDto>.FailureResponse("Attendance not found", 404);
            }

            await _unitOfWork.BeginTransaction();
            _mapper.Map(attendanceDto, existingAttendance);
            _unitOfWork.AttendanceRepository.Update(existingAttendance);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            var response = _mapper.Map<AttendanceDto>(existingAttendance);
            return ApiResponse<AttendanceDto>.SuccessResponse(response, "Attendance updated successfully");
        }

        public async Task<ApiResponse<AttendanceDto>> DeleteAttendanceAsync(int id)
        {
            if (id <= 0)
            {
                return ApiResponse<AttendanceDto>.FailureResponse("Valid attendance ID is required for deletion", 400);
            }

            var existingAttendance = await _unitOfWork.AttendanceRepository.GetByIdAsync(id);
            if (existingAttendance == null)
            {
                return ApiResponse<AttendanceDto>.FailureResponse("Attendance not found", 404);
            }

            var response = await GetAttendanceByIdAsync(id);
            if (!response.Success) return response;

            await _unitOfWork.BeginTransaction();
            await _unitOfWork.AttendanceRepository.DeleteAsync(id);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();

            return ApiResponse<AttendanceDto>.SuccessResponse(response.Data, "Attendance deleted successfully");
        }

        //---------------------------------------------------------------------------------

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

        public async Task<ApiResponse<IEnumerable<DebateDto>>> GetAllDebatesAsync()
        {
            var list = await _unitOfWork.DebateRepository.GetAllAsync();
            var debatesDto = _mapper.Map<IEnumerable<DebateDto>>(list);
            return ApiResponse<IEnumerable<DebateDto>>.SuccessResponse(debatesDto);
        }

        public async Task<ApiResponse<DebateDto>> AddDebateAsync(DebateDto debateDto)
        {
            if (string.IsNullOrEmpty(debateDto.Title) || string.IsNullOrEmpty(debateDto.TopicId.ToString()) || string.IsNullOrEmpty(debateDto.Description))
            {
                return ApiResponse<DebateDto>.FailureResponse("Debate title, Topic ID, and Description are required", 400);
            }

            await _unitOfWork.BeginTransaction();
            var debate = _mapper.Map<Debate>(debateDto);
            await _unitOfWork.DebateRepository.AddAsync(debate);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            var response = _mapper.Map<DebateDto>(debate);
            return ApiResponse<DebateDto>.SuccessResponse(response, "Debate added successfully", 201);
        }

        public async Task<ApiResponse<DebateDto>> UpdateDebateAsync(int id, DebateDto debateDto)
        {
            if (string.IsNullOrEmpty(debateDto.Title) || string.IsNullOrEmpty(debateDto.TopicId.ToString()) || string.IsNullOrEmpty(debateDto.Description))
            {
                return ApiResponse<DebateDto>.FailureResponse("Debate title, Topic ID, and Description are required to update", 400);
            }

            if (debateDto == null)
            {
                return ApiResponse<DebateDto>.FailureResponse("Debate data is required for update", 400);
            }
            if (id != debateDto.Id)
            {
                return ApiResponse<DebateDto>.FailureResponse("Valid debate ID is mismatch", 400);
            }
            var existingDebate = await _unitOfWork.DebateRepository.GetByIdAsync(id);
            if (existingDebate == null)
            {
                return ApiResponse<DebateDto>.FailureResponse("Debate not found", 404);
            }

            await _unitOfWork.BeginTransaction();
            _mapper.Map(debateDto, existingDebate);
            _unitOfWork.DebateRepository.Update(existingDebate);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            var response = _mapper.Map<DebateDto>(existingDebate);
            return ApiResponse<DebateDto>.SuccessResponse(response, "Debate updated successfully");
        }

        public async Task<ApiResponse<DebateDto>> DeleteDebateAsync(int id)
        {
            if (id <= 0)
            {
                return ApiResponse<DebateDto>.FailureResponse("Valid debate ID is required for deletion", 400);
            }
            var existingDebate = await _unitOfWork.DebateRepository.GetByIdAsync(id);
            if (existingDebate == null)
            {
                return ApiResponse<DebateDto>.FailureResponse("Debate not found", 404);
            }

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

        public async Task<ApiResponse<IEnumerable<DebateDto>>> GetAllDebatesWithDetailsAsync()
        {
            var debates = await _unitOfWork.DebateRepository.GetAllIncludingAsync(
                d => d.Attendances,
                d => d.Topic,
                d => d.Participants,
                d => d.Juries,
                d => d.Evaluations
            );
            var response = debates.Select(debate => _mapper.Map<DebateDto>(debate)).ToList();
            return ApiResponse<IEnumerable<DebateDto>>.SuccessResponse(response);
        }

        public async Task<ApiResponse<DebateDto>> GetDebateWithDetailsByIdAsync(int id)
        {
            var debates = await _unitOfWork.DebateRepository.GetAllIncludingAsync(
                d => d.Attendances,
                d => d.Topic,
                d => d.Participants,
                d => d.Juries,
                d => d.Evaluations
            );
            var debate = debates.FirstOrDefault(d => d.Id == id);
            var response = _mapper.Map<DebateDto>(debate);
            return ApiResponse<DebateDto>.SuccessResponse(response);
        }

        public async Task<ApiResponse<IEnumerable<DebateJuriesDto>>> GetJueriesByDebateAsync(int debateId)
        {
            var debates = await _unitOfWork.DebateRepository.GetAllIncludingAsync(d => d.Juries);
            var debate = debates.FirstOrDefault(d => d.Id == debateId);
            if (debate == null)
            {
                return ApiResponse<IEnumerable<DebateJuriesDto>>.FailureResponse("Debate not found", 404);
            }
            var response = _mapper.Map<IEnumerable<DebateJuriesDto>>(debate.Juries);
            return ApiResponse<IEnumerable<DebateJuriesDto>>.SuccessResponse(response);
        }

        public async Task<ApiResponse<IEnumerable<DebateParticipantsDto>>> GetParticipantsByDebateAsync(int debateId)
        {
            var debates = await _unitOfWork.DebateRepository.GetAllIncludingAsync(d => d.Participants);
            var debate = debates.FirstOrDefault(d => d.Id == debateId);
            if (debate == null)
            {
                return ApiResponse<IEnumerable<DebateParticipantsDto>>.FailureResponse("Debate not found", 404);
            }
            var response = _mapper.Map<IEnumerable<DebateParticipantsDto>>(debate.Participants);
            return ApiResponse<IEnumerable<DebateParticipantsDto>>.SuccessResponse(response);
        }

        public async Task<ApiResponse<DebateDto>> AddParticipantToDebateAsync(int debateId, int participantId, string role)
        {
            var debate = await _unitOfWork.DebateRepository.GetByIdAsync(debateId);
            var participant = await _unitOfWork.ParticipantRepository.GetByIdAsync(participantId);

            if (debate == null || participant == null || string.IsNullOrEmpty(role))
            {
                return ApiResponse<DebateDto>.FailureResponse("Valid Debate ID, Participant ID, and Role are required", 400);
            }

            await _unitOfWork.BeginTransaction();
            var debateParticipant = new DebateParticipants
            {
                DebateId = debateId,
                ParticipantId = participantId,
                Role = role
            };

            await _unitOfWork.DebateParticipants.AddAsync(debateParticipant);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            var updatedDebate = await _unitOfWork.DebateRepository.GetAllIncludingAsync(
                d => d.Participants,
                d => d.Juries
             );
            var debateToShow = updatedDebate.FirstOrDefault(d => d.Id == debateId);

            var response = _mapper.Map<DebateDto>(debateToShow);
            return ApiResponse<DebateDto>.SuccessResponse(response, "Participant added successfully");
        }

        public async Task<ApiResponse<DebateDto>> AddJuryToDebateAsync(int debateId, int juryId)
        {
            var debate = await _unitOfWork.DebateRepository.GetByIdAsync(debateId);
            var jury = await _unitOfWork.JuryRepository.GetByIdAsync(juryId);
            if (debate == null || jury == null)
            {
                return ApiResponse<DebateDto>.FailureResponse("Valid Debate ID and Jury ID are required", 400);
            }
            await _unitOfWork.BeginTransaction();
            var debateJury = new DebateJuries
            {
                DebateId = debateId,
                JuryId = juryId
            };
            await _unitOfWork.DebateJuries.AddAsync(debateJury);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            var response = _mapper.Map<DebateDto>(debate);
            return ApiResponse<DebateDto>.SuccessResponse(response, "Jury added to debate successfully");
        }

        //---------------------------------------------------------------------------------

        public async Task<ApiResponse<EvaluationDto>> GetEvaluationByIdAsync(int id)
        {
            var evaluation = await _unitOfWork.EvaluationsRepository.GetByIdAsync(id);
            if (evaluation == null)
                return ApiResponse<EvaluationDto>.FailureResponse("Evaluation not found", 404);

            var response = _mapper.Map<EvaluationDto>(evaluation);
            return ApiResponse<EvaluationDto>.SuccessResponse(response);
        }

        public async Task<ApiResponse<IEnumerable<EvaluationDto>>> GetAllEvaluationsAsync()
        {
            var evaluations = await _unitOfWork.EvaluationsRepository.GetAllAsync();
            var response = evaluations.Select(evaluation => _mapper.Map<EvaluationDto>(evaluation)).ToList();
            return ApiResponse<IEnumerable<EvaluationDto>>.SuccessResponse(response);
        }

        public async Task<ApiResponse<EvaluationDto>> AddEvaluationAsync(EvaluationDto evaluationDto)
        {
            if (string.IsNullOrEmpty(evaluationDto.JuryId.ToString()) || string.IsNullOrEmpty(evaluationDto.DebateId.ToString()) || string.IsNullOrEmpty(evaluationDto.Score.ToString()))
            {
                return ApiResponse<EvaluationDto>.FailureResponse("Jury ID, Debate ID, and Score are required", 400);
            }

            await _unitOfWork.BeginTransaction();
            var evaluation = _mapper.Map<Evaluation>(evaluationDto);
            await _unitOfWork.EvaluationsRepository.AddAsync(evaluation);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            var response = _mapper.Map<EvaluationDto>(evaluation);
            return ApiResponse<EvaluationDto>.SuccessResponse(response, "Evaluation added successfully", 201);
        }

        public async Task<ApiResponse<EvaluationDto>> UpdateEvaluationAsync(int id, EvaluationDto evaluationDto)
        {
            if (string.IsNullOrEmpty(evaluationDto.JuryId.ToString()) || string.IsNullOrEmpty(evaluationDto.DebateId.ToString()) || string.IsNullOrEmpty(evaluationDto.Score.ToString()))
            {
                return ApiResponse<EvaluationDto>.FailureResponse("Jury ID, Debate ID, and Score are required", 400);
            }

            if (evaluationDto == null)
            {
                return ApiResponse<EvaluationDto>.FailureResponse("Evaluation data is required for update", 400);
            }
            if (id != evaluationDto.Id)
            {
                return ApiResponse<EvaluationDto>.FailureResponse("Valid evaluation ID is required for update", 400);
            }
            if (evaluationDto.Score < 0 || evaluationDto.Score > 100)
            {
                return ApiResponse<EvaluationDto>.FailureResponse("Score must be between 0 and 100", 400);
            }
            var existingEvaluation = await _unitOfWork.EvaluationsRepository.GetByIdAsync(id);
            if (existingEvaluation == null)
            {
                return ApiResponse<EvaluationDto>.FailureResponse("Evaluation not found", 404);
            }

            await _unitOfWork.BeginTransaction();
            _mapper.Map(evaluationDto, existingEvaluation);
            _unitOfWork.EvaluationsRepository.Update(existingEvaluation);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            var response = _mapper.Map<EvaluationDto>(existingEvaluation);
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

        //---------------------------------------------------------------------------------

        public async Task<ApiResponse<IEnumerable<TopicDto>>> GetAllTopicsAsync()
        {
            var topics = await _unitOfWork.Topics.GetAllAsync();
            var response = topics.Select(topic => _mapper.Map<TopicDto>(topic)).ToList();
            return ApiResponse<IEnumerable<TopicDto>>.SuccessResponse(response);
        }

        public async Task<ApiResponse<TopicDto>> GetTopicByIdAsync(int id)
        {
            var topic = await _unitOfWork.Topics.GetByIdAsync(id);
            if (topic == null)
                return ApiResponse<TopicDto>.FailureResponse("Topic not found", 404);
            var response = _mapper.Map<TopicDto>(topic);
            return ApiResponse<TopicDto>.SuccessResponse(response);
        }

        public async Task<ApiResponse<TopicDto>> AddTopicAsync(TopicDto topicDto)
        {
            if (string.IsNullOrEmpty(topicDto.Name) || string.IsNullOrEmpty(topicDto.Description))
            {
                return ApiResponse<TopicDto>.FailureResponse("Topic name and description are required", 400);
            }

            await _unitOfWork.BeginTransaction();
            var topic = _mapper.Map<Topic>(topicDto);
            await _unitOfWork.Topics.AddAsync(topic);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            var response = _mapper.Map<TopicDto>(topic);
            return ApiResponse<TopicDto>.SuccessResponse(response, "Topic added successfully", 201);
        }

        public async Task<ApiResponse<TopicDto>> UpdateTopicAsync(int id, TopicDto topicDto)
        {
            if (string.IsNullOrEmpty(topicDto.Name) || string.IsNullOrEmpty(topicDto.Description))
            {
                return ApiResponse<TopicDto>.FailureResponse("Topic name and description are required for update", 400);
            }

            if (id != topicDto.Id)
            {
                return ApiResponse<TopicDto>.FailureResponse("Valid topic ID is required for update", 400);
            }
            if (topicDto == null)
            {
                return ApiResponse<TopicDto>.FailureResponse("Topic data is required for update", 400);
            }
            var existingTopic = await _unitOfWork.Topics.GetByIdAsync(id);
            if (existingTopic == null)
            {
                return ApiResponse<TopicDto>.FailureResponse("Topic not found", 404);
            }

            await _unitOfWork.BeginTransaction();
            _mapper.Map(topicDto, existingTopic);
            _unitOfWork.Topics.Update(existingTopic);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();
            var response = _mapper.Map<TopicDto>(existingTopic);
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

