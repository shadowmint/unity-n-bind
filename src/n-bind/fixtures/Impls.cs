#if N_BIND_TESTS
using UnityEngine;

public class ImplA : IDepA { }

public class ImplA2 : IDepA { }

public class ImplAComponent : MonoBehaviour, IDepA { }

public class ImplB : IDepB
{
    public IDepA A { get; set; }
}

public class ImplRecursive : IDepC
{
    public IDepA A { get; set; }
    public IDepB B { get; set; }
    public IDepC C { get; set; }
}
#endif
