using Moq;
using Serilog;

namespace Scalable.Stock.Tests.Factories
{
    internal class LoggerMockFactory
    {
        public static IMock<ILogger> GetLoggerMock()
        {
            var logger = new Mock<ILogger>();
            logger.Setup(l => l.Information(It.IsAny<string>()));
            logger.Setup(l => l.Information(It.IsAny<string>(), It.IsAny<object>()));
            logger.Setup(l => l.Debug(It.IsAny<string>()));
            logger.Setup(l => l.Debug(It.IsAny<string>(), It.IsAny<object>()));
            logger.Setup(l => l.Warning(It.IsAny<string>()));
            logger.Setup(l => l.Warning(It.IsAny<string>(), It.IsAny<object>()));
            logger.Setup(l => l.Error(It.IsAny<string>()));
            logger.Setup(l => l.Error(It.IsAny<string>(), It.IsAny<object>()));
            logger.Setup(l => l.Verbose(It.IsAny<string>()));
            logger.Setup(l => l.Verbose(It.IsAny<string>(), It.IsAny<object>()));

            return logger;
        }
    }
}
