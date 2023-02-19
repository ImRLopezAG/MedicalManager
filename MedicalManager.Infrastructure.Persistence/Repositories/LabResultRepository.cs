using MedicalManager.Core.Application.Interfaces;
using MedicalManager.Core.Domain.Entities;
using MedicalManager.Infrastructure.Persistence.Context;
using MedicalManager.Infrastructure.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace MedicalManager.Infrastructure.Persistence.Repositories;

public class LabResultRepository : BaseRepository<LabResult>, ILabResultRepository {
  private readonly MedicalContext _context;
  private readonly IPatientRepository _patientRepository;
  private readonly IDateRepository _dateRepository;

  public LabResultRepository(MedicalContext context, IPatientRepository patientRepository, IDateRepository dateRepository) : base(context) {
    _context = context;
    _patientRepository = patientRepository;
    _dateRepository = dateRepository;
  }

  public async Task<IEnumerable<LabResult>> GetByDNI(string dni) {
    var patient = await _patientRepository.GetByDNI(dni);
    var date = await _dateRepository.GetByPatientId(patient.Id);
    IEnumerable<LabResult> labResults = new List<LabResult>();
    foreach (var item in date)
      labResults = labResults.Concat(await GetByDates(item.Id));

    return labResults;
  }

  public async Task<LabResult> GetByDate(int date)=> await _context.LabResults.FirstOrDefaultAsync(x => x.DateId == date); 
  
  public async Task<IEnumerable<LabResult>> GetByDates(int date) => await _context.LabResults.Where(x => x.DateId == date).ToListAsync();
}
