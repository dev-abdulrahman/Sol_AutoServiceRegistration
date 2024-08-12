using Sol_AutoServiceRegistration.Interfaces;

namespace Sol_AutoServiceRegistration.Implementation
{
    public class Demo(ILogger<Demo> logger) : IDemo
    {
        public void Display()
        {
            Console.WriteLine("message from Demo");
            logger.LogInformation("something is done");
            logger.LogCritical("oops something went wrong");
            logger.LogDebug("nothing much");
        }
    }
}
