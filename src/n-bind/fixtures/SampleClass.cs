#if N_BIND_TESTS
namespace N.Package.Bind.fixtures
{
    public class SampleClass
    {
        public IDepA instance { get; set; }
        public IAutoDepC<int> instance2 { get; set; }
        public IAutoDepA instance3 { get; set; }
    }
}
#endif