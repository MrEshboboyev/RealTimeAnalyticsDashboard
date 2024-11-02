﻿using AutoMapper;
using RealTimeAnalyticsDashboard.Application.Common.Interfaces;
using RealTimeAnalyticsDashboard.Application.DTOs;
using RealTimeAnalyticsDashboard.Application.Services;
using RealTimeAnalyticsDashboard.Domain.Entities;

namespace RealTimeAnalyticsDashboard.Infrastructure.Implementations;

public class UserActivityService(IUnitOfWork unitOfWork, IMapper mapper) :
    BaseService(unitOfWork, mapper), IUserActivityService
{
    public async Task<ResponseDTO> GetActivityByIdAsync(int activityId)
    {
        try
        {
            var activity = await _unitOfWork.UserActivity.GetAsync(
                filter: a => a.Id == activityId,
                includeProperties: "Session")
                ?? throw new Exception("Activity not found!");

            var mappedActivity = _mapper.Map<UserActivityDTO>(activity);

            _response.Result = mappedActivity;
        }
        catch (Exception ex) 
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
    public async Task<ResponseDTO> GetAllActivitiesAsync()
    {
        try
        {
            var allActivities = await _unitOfWork.UserActivity.GetAllAsync(
                includeProperties: "Session");

            var mappedActivities = _mapper.Map<IEnumerable<UserActivityDTO>>(allActivities);

            _response.Result = mappedActivities;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
    public async Task<ResponseDTO> CreateActivityAsync(UserActivityDTO userActivityDTO)
    {
        try
        {
            var activityForDb = _mapper.Map<UserActivity>(userActivityDTO);

            await _unitOfWork.UserActivity.AddAsync(activityForDb);
            await _unitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
    public async Task<ResponseDTO> GetActivitiesBySessionIdAsync(int sessionId)
    {
        try
        {
            var sessionActivities = await _unitOfWork.UserActivity.GetAllAsync(
                filter: a => a.SessionId == sessionId,
                includeProperties: "Session");

            var mappedActivities = _mapper.Map<IEnumerable<UserActivityDTO>>(sessionActivities);

            _response.Result = mappedActivities;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
    public async Task<ResponseDTO> GetActivitiesByUserIdAsync(string userId)
    {
        try
        {
            var userActivities = await _unitOfWork.UserActivity.GetAllAsync(
                filter: a => a.Session.AppUserId == userId,
                includeProperties: "Session");

            var mappedActivities = _mapper.Map<IEnumerable<UserActivityDTO>>(userActivities);

            _response.Result = mappedActivities;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
    public async Task<ResponseDTO> DeleteActivityAsync(int activityId)
    {
        try
        {
            var activityFromDb = await _unitOfWork.UserActivity.GetAsync(a => a.Id == activityId)
                ?? throw new Exception("Activity not found!");
            
            await _unitOfWork.UserActivity.RemoveAsync(activityFromDb);
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
