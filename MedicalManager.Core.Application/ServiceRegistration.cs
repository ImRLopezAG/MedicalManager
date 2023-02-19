using MedicalManager.Core.Application.Contracts;
using MedicalManager.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalManager.Core.Application;

public static class ServiceRegistration {
  public static void AddApplicationServices(this IServiceCollection services) {
    #region Services
    services.AddTransient(typeof(ILoggerService<>), typeof(LoggerService<>));
    services.AddTransient<IPatientService, PatientService>();
    services.AddTransient<IDoctorService, DoctorService>();
    services.AddTransient<IUserService, UserService>();
    services.AddTransient<ILabTestService, LabTestService>();
    services.AddTransient<ILabResultService, LabResultService>();
    services.AddTransient<IDateService, DateService>();
    #endregion
  }
}
