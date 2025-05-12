using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Explosion : MonoBehaviour
{
    public ParticleSystem explosion;
    public ParticleSystem smallerexp;
    bool _check=false;
    float timer = 2.5f;

    void Start()
    {
        explosion.GetComponent<ParticleSystem>();
        smallerexp.GetComponent<ParticleSystem>();
        if (explosion.isPlaying)
            explosion.Stop();
        if (smallerexp.isPlaying)
            smallerexp.Stop();
    }


    public void OnTriggerEnter(Collider other)
    {
        _check = true;
        
    }
   
    private void FixedUpdate()
    {
        if(_check && timer > 0)
        {
            explosion.Play();
            smallerexp.Play();
            timer -= 1 * Time.deltaTime;
        }
        else
        {
            explosion.Stop();
            smallerexp.Stop();
        }
    }
}