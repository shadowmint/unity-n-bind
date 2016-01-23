using System.Reflection;

namespace N.Package.Bind
{
    /// Provide a default convenience service registry
    public class Registry : ServiceRegistry
    {
        private static Registry instance = null;
        public static Registry Default
        {
            get
            {
                if (instance == null)
                {
                    instance = new Registry();
                }
                return instance;
            }
        }
    }
}
