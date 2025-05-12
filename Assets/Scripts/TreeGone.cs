using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGone : MonoBehaviour
{
    public GameObject tree;
    bool shrink = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("breaker"))
            shrink = true;
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (shrink)
        {
            Vector3 tmpScale = tree.transform.localScale;
            tmpScale.x = Mathf.Lerp(tree.transform.localScale.x, 0.0f, .5f);
            tmpScale.y = Mathf.Lerp(tree.transform.localScale.y, 0.0f, .5f);
            tmpScale.z = Mathf.Lerp(tree.transform.localScale.z, 0.0f, .5f);

            tree.transform.localScale = tmpScale;
            shrink = false;
        }
        
    }
}
