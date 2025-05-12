using System.Collections;
using UnityEngine;

public class ScaleLerper : MonoBehaviour
{
    public GameObject player;
    
    public float duration =70f;
    Vector3 minScale;
   
    void FixedUpdate()
    {
        player.transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(5.5f, 5.5f, 5.5f), .01f * Time.deltaTime);

    }
}