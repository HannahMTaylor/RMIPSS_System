using RMIPSS_System.Models.Constants;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.Enums;
using RMIPSS_System.Models.ProcessSteps;
using RMIPSS_System.Models.ViewModel;
using RMIPSS_System.Repository.IRepository;

namespace RMIPSS_System.Services;

public class ProcessStepService(IStudentRepository studentRepository) : IProcessStepService
{

    public async Task<List<DocumentViewModel>> GetNewFormsList(Student student)
    {
        if (student == null) return new List<DocumentViewModel>();

        return student.SEProcessSteps switch
        {
            SEProcessSteps.SE1 => new List<DocumentViewModel>
            {
                new DocumentViewModel { Name = ProcessStepConstants.SE2_NAME, Method = ProcessStepConstants.SE2_METHOD, Controller = ProcessStepConstants.SE2_CONTROLLER },
                new DocumentViewModel { Name = ProcessStepConstants.SE2A_NAME }
            },
            SEProcessSteps.SE2 => new List<DocumentViewModel>
            {
                new DocumentViewModel { Name = ProcessStepConstants.SE2A_NAME }
            },
            SEProcessSteps.SE2A => new List<DocumentViewModel>
            {
                new DocumentViewModel { Name = ProcessStepConstants.SE3_NAME }
            },
            SEProcessSteps.SE3 => new List<DocumentViewModel>
            {
                new DocumentViewModel { Name = ProcessStepConstants.SE3A_NAME }
            },
            SEProcessSteps.SE3A => new List<DocumentViewModel>
            {
                new DocumentViewModel { Name = ProcessStepConstants.SE4_NAME, Method = ProcessStepConstants.SE4_METHOD, Controller = ProcessStepConstants.SE4_CONTROLLER },
                new DocumentViewModel { Name = ProcessStepConstants.SE10_NAME }
            },
            SEProcessSteps.SE4 => new List<DocumentViewModel>
            {
                new DocumentViewModel { Name = ProcessStepConstants.SE5_NAME }
            },
            SEProcessSteps.SE10 => new List<DocumentViewModel>
            {
                new DocumentViewModel { Name = ProcessStepConstants.SE6_NAME }
            },
            _ => new List<DocumentViewModel>
            {
                new DocumentViewModel { Name = ProcessStepConstants.SE6_NAME },
                new DocumentViewModel { Name = ProcessStepConstants.SE5_NAME },
                new DocumentViewModel { Name = ProcessStepConstants.SE9_NAME },
                new DocumentViewModel { Name = ProcessStepConstants.SE12_NAME },
                new DocumentViewModel { Name = ProcessStepConstants.SE13_NAME },
                new DocumentViewModel { Name = ProcessStepConstants.SE14_NAME },
                new DocumentViewModel { Name = ProcessStepConstants.SE15_NAME }
            }
        };
    }

    public async Task<List<DocumentViewModel>> GetFormsList(Student student)
    {
        var studentForms = new List<DocumentViewModel>();
        if (student == null) return studentForms;
        // Fetch all required process step IDs in a single database call
        var processStepIds = await studentRepository.GetProcessStepIdsByStudentId(student.Id, new[]
        {
            typeof(ConsentForm),
            typeof(SE2)
        });

        // Extract IDs from dictionary
        int consentId = processStepIds.GetValueOrDefault(typeof(ConsentForm), 0);
        int se2Id = processStepIds.GetValueOrDefault(typeof(SE2), 0);
        
        // Use ID values to determine which forms to include
        if (student.SEProcessSteps == SEProcessSteps.SE4)
        {
            if (consentId > 0)
                studentForms.Add(CreateDocument(consentId, ProcessStepConstants.SE4_NAME, ProcessStepConstants.SE4_METHOD, ProcessStepConstants.SE4_CONTROLLER));

            if (se2Id > 0)
                studentForms.Add(CreateDocument(se2Id, ProcessStepConstants.SE2_NAME, ProcessStepConstants.SE2_METHOD, ProcessStepConstants.SE2_CONTROLLER));
        }
        else if (student.SEProcessSteps == SEProcessSteps.SE2)
        {
            if (se2Id > 0)
                studentForms.Add(CreateDocument(se2Id, ProcessStepConstants.SE2_NAME, ProcessStepConstants.SE2_METHOD, ProcessStepConstants.SE2_CONTROLLER));
        }

        return studentForms;
    }
    
    // Helper method to create a DocumentViewModel
    private DocumentViewModel CreateDocument(int id, string name, string method = null, string controller = null)
    {
        return new DocumentViewModel
        {
            Id = id,
            Name = name,
            Method = method,
            Controller = controller
        };
    }
}

