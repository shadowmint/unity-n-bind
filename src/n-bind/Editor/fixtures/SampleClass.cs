#if N_BIND_TESTS
using N.Package.Bind;

[Bind]
public class SampleClass
{
    public IDepA instance { get; set; }
}
#endif
