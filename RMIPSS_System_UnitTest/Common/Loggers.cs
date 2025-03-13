using Microsoft.Extensions.Logging;
using RMIPSS_System.Services;

namespace RMIPSS_System_UnitTest.Common;

public static class Loggers
{
    public static ILogger<Se2Service>? _se2Logger = new Logger<Se2Service>(new LoggerFactory());
}