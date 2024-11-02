using AutoMapper;
using RealTimeAnalyticsDashboard.Application.Common.Interfaces;
using RealTimeAnalyticsDashboard.Application.DTOs;
using RealTimeAnalyticsDashboard.Application.Services;
using RealTimeAnalyticsDashboard.Domain.Entities;
using RealTimeAnalyticsDashboard.Domain.Enums;

namespace RealTimeAnalyticsDashboard.Infrastructure.Implementations;

public class MetricHistoryService(IUnitOfWork unitOfWork, IMapper mapper) :
    BaseService(unitOfWork, mapper), IMetricHistoryService
{
    public async Task<ResponseDTO> GetMetricHistoryByIdAsync(int historyId)
    {
        try
        {
            var activity = await _unitOfWork.MetricHistory.GetAsync(a => a.Id == historyId)
                ?? throw new Exception("Metric History not found!");

            var mappedMetricHistory = _mapper.Map<MetricHistoryDTO>(activity);

            _response.Result = mappedMetricHistory;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
    public async Task<ResponseDTO> GetAllMetricHistoriesAsync()
    {
        try
        {
            var allMetricHistories = await _unitOfWork.MetricHistory.GetAllAsync();

            var mappedMetricHistories = _mapper.Map<IEnumerable<MetricHistoryDTO>>(allMetricHistories);

            _response.Result = mappedMetricHistories;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
    public async Task<ResponseDTO> CreateMetricHistoryAsync(MetricHistoryDTO metricHistoryDTO)
    {
        try
        {
            var activityForDb = _mapper.Map<MetricHistory>(metricHistoryDTO);

            await _unitOfWork.MetricHistory.AddAsync(activityForDb);
            await _unitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
    public async Task<ResponseDTO> DeleteMetricHistoryAsync(int historyId)
    {
        try
        {
            var activityFromDb = await _unitOfWork.MetricHistory.GetAsync(a => a.Id == historyId)
                ?? throw new Exception("MetricHistory not found!");

            await _unitOfWork.MetricHistory.RemoveAsync(activityFromDb);
            await _unitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
    public async Task<ResponseDTO> GetMetricHistoriesByTypeAsync(MetricType metricType)
    {
        try
        {
            var typeMetricHistories = await _unitOfWork.MetricHistory.GetAllAsync(
                a => a.MetricType == metricType);

            var mappedMetricHistories = _mapper.Map<IEnumerable<MetricHistoryDTO>>(typeMetricHistories);

            _response.Result = mappedMetricHistories;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
    public async Task<ResponseDTO> SaveCurrentMetricsToHistoryAsync()
    {
        try
        {
            // Retrieve all current metrics
            var allMetrics = await _unitOfWork.Metric.GetAllAsync();

            // Convert each Metric to a MetricHistory entry
            var metricHistories = allMetrics.Select(m => new MetricHistory
            {
                MetricType = m.MetricType,
                Value = m.Value,
                Timestamp = DateTime.UtcNow
            }).ToList();

            // Save all MetricHistory entries to the repository
            await _unitOfWork.MetricHistory.AddListAsync(metricHistories);

            _response.Result = metricHistories;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
}
