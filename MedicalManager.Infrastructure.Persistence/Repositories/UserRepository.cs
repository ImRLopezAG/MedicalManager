using MedicalManager.Core.Application.Helpers;
using MedicalManager.Core.Application.Interfaces;
using MedicalManager.Core.Application.ViewModels;
using MedicalManager.Core.Domain.Entities;
using MedicalManager.Infrastructure.Persistence.Context;
using MedicalManager.Infrastructure.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace MedicalManager.Infrastructure.Persistence.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository {
  private readonly MedicalContext _context;
  public UserRepository(MedicalContext context) : base(context) => _context = context;

  public async Task<User> Login(LoginVm login) {
    string passwordHash = EncryptPassword.Encrypt(login.Password);
    return await _context.Users.FirstOrDefaultAsync(x => x.UserName == login.UserName && x.Password == passwordHash);
  }
}
