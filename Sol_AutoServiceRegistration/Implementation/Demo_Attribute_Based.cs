using Sol_AutoServiceRegistration.Attributes;
using Sol_AutoServiceRegistration.Interfaces;

namespace Sol_AutoServiceRegistration.Implementation
{
    [ServiceAttribute(ServiceLifetime.Scoped)]
    public class Demo_Attribute_Based : IDemo_Attribute_Based
    {
        public void Display()
        {
            Console.WriteLine("message from Demo");
        }
    }
}
