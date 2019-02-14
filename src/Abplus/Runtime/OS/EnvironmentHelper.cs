using System.Runtime.InteropServices;

namespace Abp.Runtime.OS
{
    public static class EnvironmentHelper
    {
        public static EnvDescription GetRuntimeInformation()
        {
            return new EnvDescription
            {
                FrameworkDescription = RuntimeInformation.FrameworkDescription,
                OSArchitecture = RuntimeInformation.OSArchitecture.ToString(),
                OSDescription = RuntimeInformation.OSDescription,
                ProcessArchitecture = RuntimeInformation.ProcessArchitecture.ToString()
            };
        }
    }
}
