namespace N.Package.Bind
{
    /// Provide a default convenience service registry
    public class Registry : ServiceRegistry
    {
        private static Registry _instance;

        public static Registry Default
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = new Registry();
                return _instance;
            }
        }
    }
}