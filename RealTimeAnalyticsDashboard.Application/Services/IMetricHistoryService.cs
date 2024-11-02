﻿using RealTimeAnalyticsDashboard.Application.DTOs;
using RealTimeAnalyticsDashboard.Domain.Entities;
using RealTimeAnalyticsDashboard.Domain.Enums;

namespace RealTimeAnalyticsDashboard.Application.Services;

public interface IMetricHistoryService
{
    Task<ResponseDTO> GetMetricHistoryByIdAsync(int historyId);
    Task<ResponseDTO> GetAllMetricHistoriesAsync();
    Task<ResponseDTO> CreateMetricHistoryAsync(MetricHistoryDTO metricHistoryDTO);
    Task<ResponseDTO> DeleteMetricHistoryAsync(int historyId);
    Task<ResponseDTO> GetMetricHistoryByTypeAsync(MetricType metricType);
    Task<ResponseDTO> SaveCurrentMetricsToHistoryAsync(); // Save current metrics as historical data
}
