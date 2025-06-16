using UnityEngine;

public class LightningBolt : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 0.75f); // Just for visual effect
    }
}
