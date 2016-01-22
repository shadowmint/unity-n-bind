namespace N.Package.Bind
{
    [System.AttributeUsage(System.AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class Bind : System.Attribute
    {
        public Bind()
        {
            N._.Log("Inject invoked");
        }
    }
}
