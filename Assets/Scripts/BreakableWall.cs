using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public void Break()
    {
        Debug.Log("Wall broken!");
        Destroy(gameObject); 
    }
}
