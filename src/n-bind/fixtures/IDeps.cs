#if N_BIND_TESTS
public interface IDepA
{
}

public interface IDepB
{
  IDepA A { get; set; }
}

public interface IDepCmp
{
  IDepB B { get; set; }
}

public interface IDepC
{
  IDepA A { get; set; }
  IDepB B { get; set; }
  IDepC C { get; set; }
  IDepCmp Component { get; set; }
}

public interface IAutoDepA
{
}

public interface IAutoDepB
{
}

public interface IAutoDepC<T>
{
}
#endif