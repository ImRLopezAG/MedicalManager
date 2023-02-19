namespace MedicalManager.Core.Application.Core;

public interface IGenericService<TVm, SaveVm> : IBaseService where TVm : class where SaveVm : class {
  Task<SaveVm> Save(SaveVm vm, string createdBy);
  Task Edit(SaveVm vm, string createdBy);
  Task Delete(int id);
  Task<SaveVm> GetEntity(int id);

}
