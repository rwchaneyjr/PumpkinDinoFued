using System.Collections;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public GameObject Dino;                    // The enemy or object to hide (optional)
    public ParticleSystem smokeParticle;       // Smoke effect to play
    //public ParticleSystem explosionParticle;   // Explosion effect to play
    public GameObject player;                  // The player GameObject to hide
    public bool hasBeenZapped = false;

    private bool hasActivated = false;

    private void Start()
    {
        if (smokeParticle != null) smokeParticle.Stop();
      //  if (explosionParticle != null) explosionParticle.Stop();
    }

    // Allow external trigger (Raycast, Button, etc.)
    public void StartEffect()
    {
        if (hasActivated || hasBeenZapped) return;

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

        if (smokeParticle != null) smokeParticle.Play();
       // if (explosionParticle != null) explosionParticle.Play();

        yield return new WaitForSeconds(0.5f);

        if (Dino != null) Dino.SetActive(false);
       // if (player != null) player.SetActive(false);
    }
}
