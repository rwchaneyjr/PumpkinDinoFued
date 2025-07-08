using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Particle : MonoBehaviour
{
    public GameObject Dino;                    // The enemy or object to hide (optional)
    public ParticleSystem smokeParticle;       // Smoke effect to play
   public ParticleSystem explosionParticle;   // Explosion effect to play
    public GameObject player;                  // The player GameObject to hide
    public bool hasBeenZapped = false;
   public int num = 0;
    private bool hasActivated = false;
    public static int zapCount = 0;


    private void Start()
    {
        if (smokeParticle != null) smokeParticle.Stop();
        if (explosionParticle != null) explosionParticle.Stop();
        //  if (explosionParticle != null) explosionParticle.Stop();
    }

    // Allow external trigger (Raycast, Button, etc.)
    public void StartEffect()
    {
        if (hasActivated || hasBeenZapped) return;
       num= 0;
        hasActivated = true;
        hasBeenZapped = true;
        StartCoroutine(DoEffect());
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartEffect();
        }
    }

    public IEnumerator DoEffect()
    {

        yield return new WaitForSeconds(0.1f);

        if (explosionParticle != null) explosionParticle.Play();
        yield return new WaitForSeconds(0.2f);

        if (smokeParticle != null) smokeParticle.Play();
      
       // if (explosionParticle != null) explosionParticle.Play();

        yield return new WaitForSeconds(0.5f);

        if (Dino != null)
        {
            Dino.SetActive(false);
            zapCount++;
        }

        if (zapCount == 5)
        {
            int current = SceneManager.GetActiveScene().buildIndex;
            if (current == 0)
                SceneManager.LoadScene(1); // Go to scene 1
            else
                SceneManager.LoadScene(0); // Go back to scene 0
        }

    }
}
