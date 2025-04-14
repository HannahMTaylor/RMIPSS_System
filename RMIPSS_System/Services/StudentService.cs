using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.Enums;
using RMIPSS_System.Models.ProcessSteps;
using RMIPSS_System.Models.ViewModel;
using RMIPSS_System.Repository.IRepository;

namespace RMIPSS_System.Services;

public class StudentService(ILogger<StudentService> logger, IStudentRepository studentRepository, IProcessStepService processStepService)
{
    public async Task<(List<Student>, int)> GetPaginatedStudentsAsync(string search, int? schoolId, int pageNo, int pageSize)
    {
        try
        {
            return await studentRepository.GetPaginatedStudentsAsync(search, schoolId, pageNo, pageSize);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching student list.");
            throw;
        }
    }

    /// <summary>
    /// method to get the details of Student through Student id
    /// </summary>
    /// <param name="id">Student id</param>
    /// <param name="schoolId">School id </param>
    /// <returns>Student details</returns>
    public async Task<StudentViewModel?> GetStudentByIdAsync(int id, int? schoolId)
    {
        StudentViewModel studentViewModel = new StudentViewModel();

        Student? student = await studentRepository.GetByStudentIdAsync(id);
        if (student == null)
        {
            logger.LogError("Student with id: {id} not found", id);
            return null;
        }
        
        if (schoolId != null && student.SchoolId != schoolId)
        {
                studentViewModel.hasAccess = false;
                return studentViewModel;
            
        }

        studentViewModel.Id = student.Id;
        studentViewModel.DOB = student.DOB;
        studentViewModel.FirstName = student.FirstName;
        studentViewModel.LastName = student.LastName;
        studentViewModel.Grade = student.Grade;
        studentViewModel.Sex = student.Sex;
        studentViewModel.SEProcessSteps = student.SEProcessSteps;
        studentViewModel.upcomingSEForms = await processStepService.GetNewFormsList(student);
        studentViewModel.documentsList = await processStepService.GetFormsList(student);
        studentViewModel.hasAccess = true;
        return studentViewModel;
    }
    
    
    public async Task<Student?> GetStudent(int studentId)
    {
        try
        {
            return await studentRepository.GetAsync(student => student!.Id == studentId);
        }
        catch (Exception ex) {
            logger.LogError(ex, "Error fetching student.");
            throw;
        }
    }
    
    public async Task UpdateSeProcessSteps(int studentId, SEProcessSteps oldSeProcessSteps, SEProcessSteps newSeProcessSteps)
    {
        try
        {
            Student? student = await GetStudent(studentId);
            if (student != null && student.SEProcessSteps.Equals(oldSeProcessSteps))
            {
                student.SEProcessSteps = newSeProcessSteps;
                student.SEProcessCompletedDate = DateOnly.FromDateTime(DateTime.Now);
                studentRepository.Update(student);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating student.");
            throw;
        }
    }
}