using RMIPSS_System.Data;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.Enums;
using RMIPSS_System.Models.ProcessSteps;
using RMIPSS_System.Models.ViewModel;
using RMIPSS_System.Repository.IRepository;

namespace RMIPSS_System.Services;

public class StudentService
{
    private readonly ILogger<StudentService> _logger;
    private readonly IStudentRepository _studentRepository;

    public StudentService(ILogger<StudentService> logger, IStudentRepository studentRepository)
    {
        _logger = logger; 
        _studentRepository = studentRepository;
    }
    
    public async Task<(List<Student>, int)> GetPaginatedStudentsAsync(string search, int? schoolId, int pageNo, int pageSize)
    {
        try
        {
            return await _studentRepository.GetPaginatedStudentsAsync(search, schoolId, pageNo, pageSize);
        }
        catch (Exception ex) {
            _logger.LogError(ex, "Error fetching student list.");
            throw;
        }
    }
    
/// <summary>
/// method to get the details of student through student id
/// </summary>
/// <param name="id">student id</param>
/// <returns>student details</returns>
    public async Task<StudentViewModel> GetStudentByIdAsync(int id)
    {
        StudentViewModel studentViewModel = new StudentViewModel();
        
        Student student = await _studentRepository.GetByStudentIdAsync(id);
        if (student == null)
        {
            _logger.LogError("Student with id: {id} not found", id);
        }
        studentViewModel.Id = student.Id;
        studentViewModel.FirstName = student.FirstName;
        studentViewModel.LastName = student.LastName;
        studentViewModel.Grade = student.Grade;
        studentViewModel.Sex = student.Sex;
        studentViewModel.SEProcessSteps = student.SEProcessSteps;
        studentViewModel.upcomingSEForms = getNewFormsList(student);
        studentViewModel.documentsList =  GetFormsList(student);
        return studentViewModel;
    }

//this method will deprecated with time
    public List<SEProcessSteps> getNewFormsList(Student student)
    {
        List<SEProcessSteps> newForms = new List<SEProcessSteps>();
        //this logic are not fully correct, update will be done with time 
        if (student.SEProcessSteps == SEProcessSteps.SE1)
        {
            newForms.Add(SEProcessSteps.SE2);
            newForms.Add(SEProcessSteps.SE2A);
        }
        else if (student.SEProcessSteps == SEProcessSteps.SE2)
        {
            newForms.Add(SEProcessSteps.SE2A);
        }
        else if (student.SEProcessSteps == SEProcessSteps.SE2A)
        {
            newForms.Add(SEProcessSteps.SE3);
        }
        else if (student.SEProcessSteps == SEProcessSteps.SE3)
        {
            newForms.Add(SEProcessSteps.SE3A);
        }
        else if (student.SEProcessSteps == SEProcessSteps.SE3A)
        {
            newForms.Add(SEProcessSteps.SE4);
            newForms.Add(SEProcessSteps.SE10);
        }
       // else if (student.SEProcessSteps == SEProcessSteps.SE4)
        //{
        //    newForms.Add(SEProcessSteps.SE10);
       // }
        else if (student.SEProcessSteps == SEProcessSteps.SE10)
        {
            newForms.Add(SEProcessSteps.SE6);
        }
        else if (student.SEProcessSteps == SEProcessSteps.SE6)
        {
            //further logic is yet to be implemented 
            newForms.Add(SEProcessSteps.SE5);
            newForms.Add(SEProcessSteps.SE7A);
        }
        else if (student.SEProcessSteps == SEProcessSteps.SE5)
        {
            // if not-eligible, SE5 should be implemented for further logic
            newForms.Add(SEProcessSteps.SE3);
            //if eligible
            newForms.Add(SEProcessSteps.SE8);
        }
        else if (student.SEProcessSteps == SEProcessSteps.SE8)
        {
            newForms.Add(SEProcessSteps.SE6);
           
        }
        else
        {
            newForms.Add(SEProcessSteps.SE5);
            newForms.Add(SEProcessSteps.SE9);
            newForms.Add(SEProcessSteps.SE12);
            newForms.Add(SEProcessSteps.SE13);
            newForms.Add(SEProcessSteps.SE14);
            newForms.Add(SEProcessSteps.SE15);
        }

        return newForms;
    }

    /// <summary>
    /// method to retrieve stored submitted forms of the student
    /// Needs to be updated as other forms will be implemented in the system
    /// </summary>
    /// <param name="student"></param>
    /// <returns> list of forms id and their name</returns>
    public  List<DocumentViewModel> GetFormsList(Student student)
    {
        List<DocumentViewModel> studentForms = new List<DocumentViewModel>();
        if (student.SEProcessSteps == SEProcessSteps.SE4)
        {
            int consentId =  _studentRepository.GetEntityIdByStudentId<ConsentForm>(student.Id).Result;
            if (consentId > 0)
            {
                DocumentViewModel document = new DocumentViewModel();
                document.id = consentId;
                document.name = "SE-4 Process Step";
                document.method = "ConsentFormEvaluationReevaluation";
                document.controller = "ConsentForm";
                studentForms.Add(document);
            }
            int SE2Id =  _studentRepository.GetEntityIdByStudentId<SE2>(student.Id).Result;
            if (SE2Id > 0)
            {
                DocumentViewModel documentViewModel = new DocumentViewModel();
                documentViewModel.id = consentId;
                documentViewModel.name = "SE-2 Process Step";
                documentViewModel.method = "ScreeningInformationForm";
                documentViewModel.controller = "SE2";
                studentForms.Add(documentViewModel);
            }
        }
       
        return studentForms;
    }
    
    public async Task<Student> GetStudent(int studentId)
    {
        try
        {
            return await _studentRepository.GetAsync(student => student.Id == studentId);
        }
        catch (Exception ex) {
            _logger.LogError(ex, "Error fetching student.");
            throw;
        }
    }
    
    public async Task updateSEProcessSteps(int studentId, SEProcessSteps se1, SEProcessSteps se2)
    {
        try
        {
            Student student = await GetStudent(studentId);
            if (student.SEProcessSteps.Equals(se1))
            {
                student.SEProcessSteps = se2;
                student.SEProcessCompletedDate = DateOnly.FromDateTime(DateTime.Now);
                _studentRepository.Update(student);
                await _studentRepository.SaveAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating student.");
            throw;
        }
    }
}