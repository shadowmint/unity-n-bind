using UnityEngine;

public class SpawnService : ISpawnService
{
  public GameObject SpawnPrefab(GameObject template)
  {
    return Object.Instantiate(template);
  }
}