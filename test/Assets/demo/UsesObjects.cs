using N.Package.Bind;
using UnityEngine;

public class UsesObjects : MonoBehaviour
{
  public IBlock Block
  {
    get { return _block; }
    set
    {
      block = value.GameObject;
      _block = value;
    }
  }

  private IBlock _block;

  // So we can see it in the editor
  public GameObject block;
  public GameObject spawned;

  public ISpawnService Spawner { get; set; }

  public void Start()
  {
    Registry.Default.Bind(this);
    spawned = Spawner.SpawnPrefab(block);
  }
}