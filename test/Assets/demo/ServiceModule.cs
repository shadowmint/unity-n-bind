using N.Package.Bind;
using UnityEngine;

public class ServiceModule : MonoBehaviour, IServiceModule
{
  public BlockBase BlockType;

  public void Start()
  {
    Registry.Default.Register(this);
  }

  public void Register(ServiceRegistry registry)
  {
    registry.Register<IBlock, BlockBase>(BlockType.GetComponent<BlockBase>());
    registry.Register<ISpawnService, SpawnService>();
  }
}