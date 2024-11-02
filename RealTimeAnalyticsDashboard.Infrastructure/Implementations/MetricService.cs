using AutoMapper;
using RealTimeAnalyticsDashboard.Application.Common.Interfaces;
using RealTimeAnalyticsDashboard.Application.DTOs;
using RealTimeAnalyticsDashboard.Application.Services;
using RealTimeAnalyticsDashboard.Domain.Entities;
using RealTimeAnalyticsDashboard.Domain.Enums;

namespace RealTimeAnalyticsDashboard.Infrastructure.Implementations;

public class MetricService(IUnitOfWork unitOfWork, IMapper mapper) :
    BaseService(unitOfWork, mapper), IMetricService
{
    public async Task<ResponseDTO> GetMetricByIdAsync(int metricId)
    {
        try
        {
            var metric = await _unitOfWork.Metric.GetAsync(a => a.Id == metricId)
                ?? throw new Exception("Metric not found!");

            var mappedMetric = _mapper.Map<MetricDTO>(metric);

            _response.Result = mappedMetric;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
    public async Task<ResponseDTO> GetAllMetricsAsync()
    {
        try
        {
            var allMetrics = await _unitOfWork.Metric.GetAllAsync();

            var mappedMetrics = _mapper.Map<IEnumerable<MetricDTO>>(allMetrics);

            _response.Result = mappedMetrics;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
    public async Task<ResponseDTO> CreateMetricAsync(MetricDTO metricDTO)
    {
        try
        {
            var metricForDb = _mapper.Map<Metric>(metricDTO);

            await _unitOfWork.Metric.AddAsync(metricForDb);
            await _unitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
    public async Task<ResponseDTO> UpdateMetricAsync(MetricDTO metricDTO)
    {
        try
        {
            var metricFromDb = await _unitOfWork.Metric.GetAsync(a => a.Id == metricDTO.Id)
                ?? throw new Exception("Metric not found!");

            await _unitOfWork.Metric.UpdateAsync(metricFromDb);
            await _unitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
    public async Task<ResponseDTO> DeleteMetricAsync(int metricId)
    {
        try
        {
            var metricFromDb = await _unitOfWork.Metric.GetAsync(a => a.Id == metricId)
                ?? throw new Exception("Metric not found!");

            await _unitOfWork.Metric.RemoveAsync(metricFromDb);
            await _unitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
    public async Task<ResponseDTO> GetLatestMetricByTypeAsync(MetricType metricType)
    {
        try
        {
            var latestMetricByType = await _unitOfWork.Metric.GetLatestMetricByTypeAsync(metricType)
                ?? throw new Exception("Latest metric not found!");

            var mappedMetric = _mapper.Map<MetricDTO>(latestMetricByType);

            _response.Result = mappedMetric;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
    public async Task<ResponseDTO> UpdateMetricValueAsync(MetricType metricType, double value)
    {
        try
        {
            var latestMetricByType = await _unitOfWork.Metric.GetLatestMetricByTypeAsync(metricType)
                ?? throw new Exception("Latest metric not found!");

            latestMetricByType.Value = value;

            await _unitOfWork.Metric.UpdateAsync(latestMetricByType);
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
