using RMIPSS_System.Models.ProcessSteps;
using RMIPSS_System.Services;

namespace RMIPSS_System_UnitTest.Common;

public class Services
{
    public static IProcessStepService ProcessStepService = new ProcessStepService(Repositories._studentRepository);
    public static StudentService StudentService = new StudentService(Loggers._studentLogger, Repositories._studentRepository, ProcessStepService);

    public static ConsentFormService ConsentFormService =
        new ConsentFormService(Loggers._consentFormLogger, Repositories._consentFormRepo, StudentService);
    public static UserService UserService = new UserService(Loggers._userLogger, Repositories._appUserRepo,Repositories._roleManager,Repositories._emailSender);
}