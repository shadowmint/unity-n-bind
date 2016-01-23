using UnityEngine;

public class BlockBase : MonoBehaviour, IBlock
{
    public GameObject GameObject
    {
        get
        {
            return gameObject;
        }
    }
}
