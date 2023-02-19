using MedicalManager.Core.Application.Core;
using MedicalManager.Core.Application.Interfaces;
using MedicalManager.Infrastructure.Persistence.Context;
using MedicalManager.Infrastructure.Persistence.Core;
using MedicalManager.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalManager.Infrastructure.Persistence {
  public static class ServiceRegistration {
    public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration) {
      #region DbContext
      if (configuration.GetValue<bool>("UseInMemoryDatabase")) {
        services.AddDbContext<MedicalContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
      } else {
        services.AddDbContext<MedicalContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
        m => m.MigrationsAssembly(typeof(MedicalContext).Assembly.FullName)));
      }
      #endregion
      #region Repositories
      services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
      services.AddTransient<ILabTestRepository, LabTestRepository>();
      services.AddTransient<ILabResultRepository, LabResultRepository>();
      services.AddTransient<IPatientRepository, PatientRepository>();
      services.AddTransient<IDoctorRepository, DoctorRepository>();
      services.AddTransient<IUserRepository, UserRepository>();
      services.AddTransient<IDateRepository, DateRepository>();
      #endregion
    }
  }
}
