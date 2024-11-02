using AutoMapper;
using RealTimeAnalyticsDashboard.Application.Common.Interfaces;
using RealTimeAnalyticsDashboard.Application.DTOs;

namespace RealTimeAnalyticsDashboard.Application.Services;

public abstract class BaseService(IUnitOfWork unitOfWork, IMapper mapper)
{
    protected readonly IUnitOfWork _unitOfWork = unitOfWork;
    protected readonly IMapper _mapper = mapper;
    protected ResponseDTO _response = new();
}
