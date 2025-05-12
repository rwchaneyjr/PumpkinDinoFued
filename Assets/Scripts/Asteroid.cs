using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;


public class Asteroid : MonoBehaviour
{

    
    public GameObject asteroid;
    public GameObject fractured;
    public GameObject player;
    public static float i = 0;
    bool check1 = false;
    // public ParticleSystem explosion;
    // public ParticleSystem smallerexp;

    float timeElapse= 0;
    float duration = 1.5f;
    float x, y, z;
    // Start d before the first frame update
    
    public void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("breaker"))
        {
            check1 = true;
             
            // asteroid[i].GetComponent<Fracture>().FractureObject();
            Instantiate(fractured, asteroid.transform.position, transform.rotation);
           Destroy(asteroid);
           
            i++;
           Destroy(fractured);

        }
    }
    public void OnTriggerExit(Collider other)
    {
      
        
    }
    // Update is called once per frame
    private void Update()
    {
       // transform.localScale = new Vector3(1 + Asteroid.i / 3, 1 + Asteroid.i / 3, 1 + Asteroid.i / 3);

        if (check1)
        {
            Vector3 tmpScale = fractured.transform.localScale;
            tmpScale.x = Mathf.Lerp(fractured.transform.localScale.x, 1 + Asteroid.i / 3, .5f);
            tmpScale.y = Mathf.Lerp(fractured.transform.localScale.y, 1 + Asteroid.i / 3, .5f);
            tmpScale.z = Mathf.Lerp(fractured.transform.localScale.z, 1 + Asteroid.i / 3, .5f);

         fractured.transform.localScale = tmpScale;
            check1 = false;
        }
        
        
    }
}
