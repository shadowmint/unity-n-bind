#if N_BIND_TESTS
public interface IDepA { }
public interface IDepB
{
    IDepA A { get; set; }
}
public interface IDepC
{
    IDepA A { get; set; }
    IDepB B { get; set; }
    IDepC C { get; set; }
}
#endif
