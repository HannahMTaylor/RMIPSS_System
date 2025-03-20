using Microsoft.Extensions.Logging;
using RMIPSS_System.Services;

namespace RMIPSS_System_UnitTest.Common;

public static class Loggers
{
    public static ILogger<Se2Service>? _se2Logger = new Logger<Se2Service>(new LoggerFactory());
    public static ILogger<ConsentFormService>? _consentFormLogger = new Logger<ConsentFormService>(new LoggerFactory());
    public static ILogger<StudentService>? _studentLogger = new Logger<StudentService>(new LoggerFactory());
}