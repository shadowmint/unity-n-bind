using N.Package.Bind;
using UnityEngine;

public class UsesObjects : MonoBehaviour
{
    public IBlock Block
    {
        get { return block_; }
        set
        {
            block = value.GameObject;
            block_ = value;
        }
    }
    private IBlock block_;

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
