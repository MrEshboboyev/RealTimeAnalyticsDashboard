using AutoMapper;
using RealTimeAnalyticsDashboard.Application.Common.Interfaces;
using RealTimeAnalyticsDashboard.Application.DTOs;
using RealTimeAnalyticsDashboard.Application.Services;
using RealTimeAnalyticsDashboard.Domain.Entities;

namespace RealTimeAnalyticsDashboard.Infrastructure.Implementations;

public class SessionService(IUnitOfWork unitOfWork, IMapper mapper) :
    BaseService(unitOfWork, mapper), ISessionService
{
    public async Task<ResponseDTO> GetSessionByIdAsync(int sessionId)
    {
        try
        {
            var session = await _unitOfWork.Session.GetAsync(
                filter: a => a.Id == sessionId,
                includeProperties: "User,UserActivities")
                ?? throw new Exception("Session not found!");

            var mappedSession = _mapper.Map<SessionDTO>(session);

            _response.Result = mappedSession;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
    public async Task<ResponseDTO> GetAllSessionsAsync()
    {
        try
        {
            var allSessions = await _unitOfWork.Session.GetAllAsync(
                includeProperties: "User,UserActivities");

            var mappedSessions = _mapper.Map<IEnumerable<SessionDTO>>(allSessions);

            _response.Result = mappedSessions;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
    public async Task<ResponseDTO> GetActiveSessionByUserIdAsync(string userId)
    {
        try
        {
            var session = await _unitOfWork.Session.GetLatestSessionByUserIdAsync(userId)
                ?? throw new Exception("Latest Session not found!");

            var mappedSession = _mapper.Map<SessionDTO>(session);

            _response.Result = mappedSession;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
    public async Task<ResponseDTO> CreateSessionAsync(SessionDTO sessionDTO)
    {
        try
        {
            var sessionForDb = _mapper.Map<Session>(sessionDTO);

            await _unitOfWork.Session.AddAsync(sessionForDb);
            await _unitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
    public async Task<ResponseDTO> EndSessionAsync(int sessionId)
    {
        try
        {
            var sessionFromDb = await _unitOfWork.Session.GetAsync(a => a.Id == sessionId)
                ?? throw new Exception("Session not found!");

            sessionFromDb.EndTime = DateTime.Now;

            await _unitOfWork.Session.UpdateAsync(sessionFromDb);
            await _unitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
    public async Task<ResponseDTO> GetSessionsByUserIdAsync(string userId)
    {
        try
        {
            var userSessions = await _unitOfWork.Session.GetAllAsync(
                filter: a => a.AppUserId == userId,
                includeProperties: "UserDTO,UserActivityDTOs");

            var mappedSessions = _mapper.Map<IEnumerable<SessionDTO>>(userSessions);

            _response.Result = mappedSessions;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
    public async Task<ResponseDTO> DeleteSessionAsync(int sessionId)
    {
        try
        {
            var sessionFromDB = await _unitOfWork.Session.GetAsync(a => a.Id == sessionId)
                ?? throw new Exception("Session not found!");

            await _unitOfWork.Session.RemoveAsync(sessionFromDB);
            await _unitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
}
