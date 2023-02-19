using MedicalManager.Core.Application.Contracts;
using MedicalManager.Core.Application.Core;
using Microsoft.Extensions.Logging;

namespace MedicalManager.Core.Application.Services;

public class LoggerService<TService> : ILoggerService<TService> where TService : IBaseService {
  private readonly ILogger<TService> logger;
  public LoggerService(ILogger<TService> logger) => this.logger = logger;
  public void LogDebug(string message, params object[] args) => this.logger.LogDebug(message, args);
  public void LogError(string message, params object[] args) => this.logger.LogError(message, args);
  public void LogInformation(string message, params object[] args) => this.logger.LogInformation(message, args);
  public void LogWarning(string message, params object[] args) => this.logger.LogWarning(message, args);
}

