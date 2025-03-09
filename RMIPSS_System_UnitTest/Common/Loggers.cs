using Microsoft.Extensions.Logging;
using RMIPSS_System.Services;

namespace RMIPSS_System_UnitTest.Common;

public static class Loggers
{
    public static ILogger<SE2Service> _se2Logger = new Logger<SE2Service>(new LoggerFactory());
}