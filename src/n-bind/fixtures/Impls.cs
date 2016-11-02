#if N_BIND_TESTS
using UnityEngine;

public class ImplA : IDepA
{
}

public class ImplA2 : IDepA
{
}

public class ImplComponent : MonoBehaviour, IDepCmp
{
  public IDepB B { get; set; }
}

public class ImplB : IDepB
{
  public IDepA A { get; set; }
}

public class ImplRecursive : IDepC
{
  public IDepA A { get; set; }
  public IDepB B { get; set; }
  public IDepC C { get; set; }
  public IDepCmp Component { get; set; }
}

public class ImplAutoDep: IAutoDepA, IAutoDepB, IAutoDepC<int>, IAutoDepC<float>
{
}
#endif