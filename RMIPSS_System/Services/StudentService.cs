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
/// <summary>
/// method to get the details of student through student id
/// </summary>
/// <param name="id">student id</param>
/// <returns>student details</returns>
    public async Task<StudentViewModel> GetStudentByIdAsync(int id)
    {
        StudentViewModel studentViewModel = new StudentViewModel();
        Dictionary<string, int> studentForms = new Dictionary<string, int>();
        
        Student student = await _studentRepository.GetByStudentIdAsync(id);
        if (student == null)
        {
            _logger.LogError("Student with id: {id} not found", id);
        }
        studentViewModel.Id = student.Id;
        studentViewModel.FirstName = student.FirstName;
        studentViewModel.LastName = student.LastName;
        studentViewModel.SEProcessSteps = student.SEProcessSteps;
        studentViewModel.upcomingSEForms = getNewFormsList(student);
       
       
        
        
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
        else if (student.SEProcessSteps == SEProcessSteps.SE4)
        {
            newForms.Add(SEProcessSteps.SE10);
        }
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
    public async Task<Dictionary<string, int>> getFormsList(Student student)
    {
        Dictionary<string, int> studentForms = new Dictionary<string, int>();
        if (student.SEProcessSteps == SEProcessSteps.SE3)
        {
            int consentId = await _studentRepository.GetEntityIdByStudentId<ConsentForm>(student.Id);
            if (consentId > 0)
            {
                studentForms.Add("Consent Form", consentId);
            }
            int SE2Id = await _studentRepository.GetEntityIdByStudentId<SE2>(student.Id);
            if (SE2Id > 0)
            {
                studentForms.Add("SE-2 Form", SE2Id);
            }
        }
       
        return studentForms;
    }
    
    
    
    
}