using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public GameObject player;
    public GameObject posHolder;
  //  public GameObject pos1;
    // Start is called before the first frame update
    void Start()
    {
    
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("breaker"))

            SceneManager.LoadScene("SceneTwo");
    }
    void Update()
    {
        
    }
}
