using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.Enums;
using RMIPSS_System.Models.ProcessSteps;
using RMIPSS_System.Models.ViewModel;
using RMIPSS_System.Repository.IRepository;

namespace RMIPSS_System.Services;

public class StudentService(ILogger<StudentService> logger, IStudentRepository studentRepository)
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
    /// method to get the details of student through student id
    /// </summary>
    /// <param name="id">student id</param>
    /// <param name="schoolId">School id </param>
    /// <returns>student details</returns>
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
        studentViewModel.upcomingSEForms = GetNewFormsList(student);
        studentViewModel.documentsList = GetFormsList(student);
        studentViewModel.hasAccess = true;
        return studentViewModel;
    }

//this method will deprecated with time
    public List<DocumentViewModel> GetNewFormsList(Student? student)
    {
        List<DocumentViewModel> newForms = new List<DocumentViewModel>();
        //this logic are not fully correct, update will be done with time 
        if (student != null)
            switch (student.SEProcessSteps)
            {
                case SEProcessSteps.SE1:
                    DocumentViewModel document1 = new DocumentViewModel();
                    document1.Name = "SE-2 (Screening Information)";
                    document1.Method = "ScreeningInformationForm";
                    document1.Controller = "SE2";
                    newForms.Add(document1);

                    DocumentViewModel document2 = new DocumentViewModel();
                    document2.Name = "SE-2A (Screening Summary & Disposition)";
                    // document1.method = "ScreeningInformationForm";
                    // document1.controller = "SE2";
                    newForms.Add(document2);
                    break;

                case SEProcessSteps.SE2:
                    DocumentViewModel document3 = new DocumentViewModel();
                    document3.Name = "SE-2A (Screening Summary & Disposition)";
                    // document1.method = "ScreeningInformationForm";
                    // document1.controller = "SE2";
                    newForms.Add(document3);
                    break;

                case SEProcessSteps.SE2A:
                    DocumentViewModel document4 = new DocumentViewModel();
                    document4.Name = "SE-3 (Notice of Action Form)";
                    // document1.method = "ScreeningInformationForm";
                    // document1.controller = "SE2";
                    newForms.Add(document4);
                    break;

                case SEProcessSteps.SE3:
                    DocumentViewModel document5 = new DocumentViewModel();
                    document5.Name = "SE-3A (Request for Special Education Assistance)";
                    // document1.method = "ScreeningInformationForm";
                    // document1.controller = "SE2";
                    newForms.Add(document5);
                    break;

                case SEProcessSteps.SE3A:
                    DocumentViewModel document6 = new DocumentViewModel();
                    document6.Name = "SE-4 (Parent Consent for Evaluation)";
                    document6.Method = "ConsentFormEvaluationReevaluation";
                    document6.Controller = "ConsentForm";
                    newForms.Add(document6);

                    DocumentViewModel document7 = new DocumentViewModel();
                    document7.Name = "SE-10 (Notice of Parent Rights)";
                    //document7.method = "ConsentFormEvaluationReevaluation";
                    // document7.controller = "ConsentForm";
                    newForms.Add(document7);

                    break;

                case SEProcessSteps.SE4:
                    DocumentViewModel document10 = new DocumentViewModel();
                    document10.Name = "SE-5 (Eligibility Determination)";
                    //document7.method = "ConsentFormEvaluationReevaluation";
                    // document7.controller = "ConsentForm";
                    newForms.Add(document10);

                    break;

                case SEProcessSteps.SE10:
                    DocumentViewModel document9 = new DocumentViewModel();
                    document9.Name = "SE-6 (Parent Notice of Meeting)";
                    //document7.method = "ConsentFormEvaluationReevaluation";
                    // document7.controller = "ConsentForm";
                    newForms.Add(document9);
                    break;

                case SEProcessSteps.SE6:
                    // further logic is yet to be implemented

                    DocumentViewModel document11 = new DocumentViewModel();
                    document11.Name = "SE-7A (IEP Form)";
                    //document7.method = "ConsentFormEvaluationReevaluation";
                    // document7.controller = "ConsentForm";
                    newForms.Add(document11);

                    break;

                case SEProcessSteps.SE5:
                    // if not-eligible, SE5 should be implemented for further logic
                    DocumentViewModel document12 = new DocumentViewModel();
                    document12.Name = "SE-3 (Notice of Action Form)";
                    // document1.method = "ScreeningInformationForm";
                    // document1.controller = "SE2";
                    newForms.Add(document12);
                    // if eligible
                    DocumentViewModel document13 = new DocumentViewModel();
                    document13.Name = "SE-8 (Consent for Initial Placement)";
                    // document1.method = "ScreeningInformationForm";
                    // document1.controller = "SE2";
                    newForms.Add(document13);

                    DocumentViewModel document8 = new DocumentViewModel();
                    document8.Name = "SE-10 (Notice of Parent Rights)";
                    //document7.method = "ConsentFormEvaluationReevaluation";
                    // document7.controller = "ConsentForm";
                    newForms.Add(document8);
                    break;

                default:
                    DocumentViewModel document14 = new DocumentViewModel();
                    document14.Name = "SE-6 (Parent Notice of Meeting)";
                    //document7.method = "ConsentFormEvaluationReevaluation";
                    // document7.controller = "ConsentForm";
                    newForms.Add(document14);

                    DocumentViewModel document15 = new DocumentViewModel();
                    document15.Name = "SE-5 (3-Year Reevaluation)";
                    // document1.method = "ScreeningInformationForm";
                    // document1.controller = "SE2";
                    newForms.Add(document15);

                    DocumentViewModel document16 = new DocumentViewModel();
                    document16.Name = "SE-9 (Documentation of Interpretation or Written Translation)";
                    // document1.method = "ScreeningInformationForm";
                    // document1.controller = "SE2";
                    newForms.Add(document16);

                    DocumentViewModel document17 = new DocumentViewModel();
                    document17.Name = "SE-12 (Consent for Records Release)";
                    // document1.method = "ScreeningInformationForm";
                    // document1.controller = "SE2";
                    newForms.Add(document17);

                    DocumentViewModel document18 = new DocumentViewModel();
                    document18.Name = "SE-13 (Due Process Complaint)";
                    // document1.method = "ScreeningInformationForm";
                    // document1.controller = "SE2";
                    newForms.Add(document18);

                    DocumentViewModel document19 = new DocumentViewModel();
                    document19.Name = "SE-14 (Surrogate Parent Request)";
                    // document1.method = "ScreeningInformationForm";
                    // document1.controller = "SE2";
                    newForms.Add(document19);


                    DocumentViewModel document20 = new DocumentViewModel();
                    document19.Name = "SE-15 (Health Insurance)";
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
    public List<DocumentViewModel> GetFormsList(Student? student)
    {
        List<DocumentViewModel> studentForms = new List<DocumentViewModel>();
        if (student != null)
        {
            
            if (student is { SEProcessSteps: SEProcessSteps.SE4 })
            {
                int consentId = studentRepository.GetProcessStepIdByStudentId<ConsentForm>(student.Id).Result;
                int se2Id = studentRepository.GetProcessStepIdByStudentId<SE2>(student.Id).Result;
                if (consentId > 0)
                {
                    DocumentViewModel document = new DocumentViewModel();
                    document.Id = consentId;
                    document.Name = "SE-4 (Parent Consent for Evaluation)";
                    document.Method = "ConsentFormEvaluationReevaluation";
                    document.Controller = "ConsentForm";
                    studentForms.Add(document);
                }

                if (se2Id > 0)
                {
                    DocumentViewModel documentViewModel = new DocumentViewModel();
                    documentViewModel.Id = consentId;
                    documentViewModel.Name = "SE-2 (Screening Information)";
                    documentViewModel.Method = "ScreeningInformationForm";
                    documentViewModel.Controller = "SE2";
                    studentForms.Add(documentViewModel);
                }
            }
            
            else if (student is { SEProcessSteps: SEProcessSteps.SE2 })
            {
                int se2Id = studentRepository.GetProcessStepIdByStudentId<SE2>(student.Id).Result;
                if (se2Id > 0)
                {
                    DocumentViewModel documentViewModel = new DocumentViewModel();
                    documentViewModel.Id = se2Id;
                    documentViewModel.Name = "SE-2 (Screening Information)";
                    documentViewModel.Method = "ScreeningInformationForm";
                    documentViewModel.Controller = "SE2";
                    studentForms.Add(documentViewModel);
                }
            }
            
        }

        return studentForms;
    }
    
    public async Task<Student?> GetStudent(int studentId)
    {
        try
        {
            return await studentRepository.GetAsync(student => student != null && student.Id == studentId);
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
                await studentRepository.SaveAsync();
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating student.");
            throw;
        }
    }
}