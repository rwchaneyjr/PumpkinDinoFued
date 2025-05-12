using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Particle : MonoBehaviour
{
    public GameObject Dino;
    
    public ParticleSystem smokeParticles;
    private float timer = 3f;
    bool _check = false;
    private void Start()
    {
        smokeParticles.Stop();  
    }
    public IEnumerator DoEffect()
    {
        // Play electric charge
    

      

        // Show lightning
     
        yield return new WaitForSeconds(2f);

     

        // Play smoke puff
        smokeParticles.Play();

     
      GameObject.FindGameObjectWithTag("enemy1").SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.N))
        {
            StartCoroutine(DoEffect());
            _check = true;
        }
        
      
        
    }
}
