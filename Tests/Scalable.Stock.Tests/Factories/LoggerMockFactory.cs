using Serilog;
using NSubstitute;

namespace Scalable.Stock.Tests.Factories
{
    internal class LoggerMockFactory
    {
        public static ILogger GetLoggerMock()
        {
            return Substitute.For<ILogger>();
        }
    }
}
