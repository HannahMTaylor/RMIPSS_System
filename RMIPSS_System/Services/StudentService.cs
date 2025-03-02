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

    public async Task<(List<Student>, int)> GetPaginatedStudentsAsync(string search, int pageNo, int pageSize)
    {
        return await _studentRepository.GetPaginatedStudentsAsync(search, pageNo, pageSize);
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
        studentViewModel.DOB = student.DOB;
        studentViewModel.FirstName = student.FirstName;
        studentViewModel.LastName = student.LastName;
        studentViewModel.Grade = student.Grade;
        studentViewModel.Sex = student.Sex;
        studentViewModel.SEProcessSteps = student.SEProcessSteps;
        studentViewModel.upcomingSEForms = getNewFormsList(student);
        studentViewModel.documentsList = GetFormsList(student);
        return studentViewModel;
    }

//this method will deprecated with time
    public List<DocumentViewModel> getNewFormsList(Student student)
    {
        List<DocumentViewModel> newForms = new List<DocumentViewModel>();
        //this logic are not fully correct, update will be done with time 
        switch (student.SEProcessSteps)
        {
            case SEProcessSteps.SE1:
                DocumentViewModel document1 = new DocumentViewModel();
                document1.name = "SE-2 (Screening Information)";
                document1.method = "ScreeningInformationForm";
                document1.controller = "SE2";
                newForms.Add(document1);
                
                DocumentViewModel document2 = new DocumentViewModel();
                document2.name = "SE-2A (Screening Summary & Disposition)";
               // document1.method = "ScreeningInformationForm";
               // document1.controller = "SE2";
                newForms.Add(document2);
                break;

            case SEProcessSteps.SE2:
                DocumentViewModel document3 = new DocumentViewModel();
                document3.name = "SE-2A (Screening Summary & Disposition)";
                // document1.method = "ScreeningInformationForm";
                // document1.controller = "SE2";
                newForms.Add(document3);
                break;

            case SEProcessSteps.SE2A:
                DocumentViewModel document4 = new DocumentViewModel();
                document4.name = "SE-3 (Notice of Action Form)";
                // document1.method = "ScreeningInformationForm";
                // document1.controller = "SE2";
                newForms.Add(document4);
                break;

            case SEProcessSteps.SE3:
                DocumentViewModel document5 = new DocumentViewModel();
                document5.name = "SE-3A (Request for Special Education Assistance)";
                // document1.method = "ScreeningInformationForm";
                // document1.controller = "SE2";
                newForms.Add(document5);
                break;

            case SEProcessSteps.SE3A:
                DocumentViewModel document6 = new DocumentViewModel();
                document6.name = "SE-4 (Parent Consent for Evaluation)";
                document6.method = "ConsentFormEvaluationReevaluation";
                document6.controller = "ConsentForm";
                newForms.Add(document6);
                
                DocumentViewModel document7 = new DocumentViewModel();
                document7.name = "SE-10 (Notice of Parent Rights)";
                //document7.method = "ConsentFormEvaluationReevaluation";
               // document7.controller = "ConsentForm";
                newForms.Add(document7);
                
                break;

            case SEProcessSteps.SE4:
                DocumentViewModel document10 = new DocumentViewModel();
                document10.name = "SE-5 (Eligibility Determination)";
                //document7.method = "ConsentFormEvaluationReevaluation";
                // document7.controller = "ConsentForm";
                newForms.Add(document10);
              
                break;

            case SEProcessSteps.SE10:
                DocumentViewModel document9 = new DocumentViewModel();
                document9.name = "SE-6 (Parent Notice of Meeting)";
                //document7.method = "ConsentFormEvaluationReevaluation";
                // document7.controller = "ConsentForm";
                newForms.Add(document9);
                break;

            case SEProcessSteps.SE6:
                // further logic is yet to be implemented
                
                DocumentViewModel document11 = new DocumentViewModel();
                document11.name = "SE-7A (IEP Form)";
                //document7.method = "ConsentFormEvaluationReevaluation";
                // document7.controller = "ConsentForm";
                newForms.Add(document11);
              
                break;

            case SEProcessSteps.SE5:
                // if not-eligible, SE5 should be implemented for further logic
                DocumentViewModel document12 = new DocumentViewModel();
                document12.name = "SE-3 (Notice of Action Form)";
                // document1.method = "ScreeningInformationForm";
                // document1.controller = "SE2";
                newForms.Add(document12);
                // if eligible
                DocumentViewModel document13 = new DocumentViewModel();
                document13.name = "SE-8 (Consent for Initial Placement)";
                // document1.method = "ScreeningInformationForm";
                // document1.controller = "SE2";
                newForms.Add(document13);
                
                DocumentViewModel document8 = new DocumentViewModel();
                document8.name = "SE-10 (Notice of Parent Rights)";
                //document7.method = "ConsentFormEvaluationReevaluation";
                // document7.controller = "ConsentForm";
                newForms.Add(document8);
                break;
            
            default:
                DocumentViewModel document14 = new DocumentViewModel();
                document14.name = "SE-6 (Parent Notice of Meeting)";
                //document7.method = "ConsentFormEvaluationReevaluation";
                // document7.controller = "ConsentForm";
                newForms.Add(document14);
                
                DocumentViewModel document15 = new DocumentViewModel();
                document15.name = "SE-5 (3-Year Reevaluation)";
                // document1.method = "ScreeningInformationForm";
                // document1.controller = "SE2";
                newForms.Add(document15);
                
                DocumentViewModel document16 = new DocumentViewModel();
                document16.name = "SE-9 (Documentation of Interpretation or Written Translation)";
                // document1.method = "ScreeningInformationForm";
                // document1.controller = "SE2";
                newForms.Add(document16);
                
                DocumentViewModel document17 = new DocumentViewModel();
                document17.name = "SE-12 (Consent for Records Release)";
                // document1.method = "ScreeningInformationForm";
                // document1.controller = "SE2";
                newForms.Add(document17);
                
                DocumentViewModel document18 = new DocumentViewModel();
                document18.name = "SE-13 (Due Process Complaint)";
                // document1.method = "ScreeningInformationForm";
                // document1.controller = "SE2";
                newForms.Add(document18);
                
                DocumentViewModel document19 = new DocumentViewModel();
                document19.name = "SE-14 (Surrogate Parent Request)";
                // document1.method = "ScreeningInformationForm";
                // document1.controller = "SE2";
                newForms.Add(document19);
                
                
                DocumentViewModel document20 = new DocumentViewModel();
                document19.name = "SE-15 (Health Insurance)";
                // document1.method = "ScreeningInformationForm";
                // document1.controller = "SE2";
                newForms.Add(document19);
                break;
        }

        return newForms;
    }

    /// <summary>
    /// method to retrieve stored submitted forms of the student
    /// Needs to be updated as other forms will be implemented in the system
    /// </summary>
    /// <param name="student"></param>
    /// <returns> list of forms id and their name</returns>
    public List<DocumentViewModel> GetFormsList(Student student)
    {
        List<DocumentViewModel> studentForms = new List<DocumentViewModel>();
        if (student.SEProcessSteps == SEProcessSteps.SE4)
        {
            int consentId = _studentRepository.GetEntityIdByStudentId<ConsentForm>(student.Id).Result;
            if (consentId > 0)
            {
                DocumentViewModel document = new DocumentViewModel();
                document.id = consentId;
                document.name = "SE-4 (Parent Consent for Evaluation)";
                document.method = "ConsentFormEvaluationReevaluation";
                document.controller = "ConsentForm";
                studentForms.Add(document);
            }

            int SE2Id = _studentRepository.GetEntityIdByStudentId<SE2>(student.Id).Result;
            if (SE2Id > 0)
            {
                DocumentViewModel documentViewModel = new DocumentViewModel();
                documentViewModel.id = consentId;
                documentViewModel.name = "SE-2 (Screening Information)";
                documentViewModel.method = "ScreeningInformationForm";
                documentViewModel.controller = "SE2";
                studentForms.Add(documentViewModel);
            }
        }

        return studentForms;
    }
}