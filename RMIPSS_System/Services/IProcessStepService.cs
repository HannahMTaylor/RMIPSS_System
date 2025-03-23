using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.ViewModel;

namespace RMIPSS_System.Services;

public interface IProcessStepService
{
    Task<List<DocumentViewModel>> GetNewFormsList(Student student);
    Task<List<DocumentViewModel>> GetFormsList(Student student);
}