using UnityEngine;

public class SpawnService : ISpawnService
{
    public GameObject SpawnPrefab(GameObject template)
    {
        return UnityEngine.Object.Instantiate(template);
    }
}
