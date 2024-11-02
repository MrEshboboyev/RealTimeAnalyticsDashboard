using RealTimeAnalyticsDashboard.Application.DTOs;
using RealTimeAnalyticsDashboard.Domain.Enums;

namespace RealTimeAnalyticsDashboard.Application.Services;

public interface IMetricService
{
    Task<ResponseDTO> GetMetricByIdAsync(int metricId);
    Task<ResponseDTO> GetAllMetricsAsync();
    Task<ResponseDTO> CreateMetricAsync(MetricDTO metricDTO);
    Task<ResponseDTO> UpdateMetricAsync(MetricDTO metricDTO);
    Task<ResponseDTO> DeleteMetricAsync(int metricId);
    Task<ResponseDTO> GetLatestMetricByTypeAsync(MetricType metricType);
    Task<ResponseDTO> UpdateMetricValueAsync(MetricType metricType, double value);
}

