using N.Package.Bind;
using UnityEngine;

public class ServiceModule : MonoBehaviour, IServiceModule
{
    public BlockBase blockType;

    public void Start()
    {
        Registry.Default.Register(this);
    }

    public void Register(ServiceRegistry registry)
    {
        registry.Register<IBlock, BlockBase>(blockType.GetComponent<BlockBase>());
        registry.Register<ISpawnService, SpawnService>();
    }
}
