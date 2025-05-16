using System.Collections;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public GameObject Dino;                 // Assign the Dino model (the thing to disappear)
    public ParticleSystem smokeParticle;   // Assign the smoke ParticleSystem

    private bool hasActivated = false;

    private void Start()
    {
        if (smokeParticle != null)
            smokeParticle.Stop();
    }

    // ✅ This is the method Lightning.cs will call
    public void StartEffect()
    {
        if (!hasActivated)
        {
            hasActivated = true;
            StartCoroutine(DoEffect());
        }
    }

    private IEnumerator DoEffect()
    {
        yield return new WaitForSeconds(2f); // Delay before smoke

        if (smokeParticle != null)
            smokeParticle.Play();

        yield return new WaitForSeconds(1f); // Let smoke play

        if (Dino != null)
            Dino.SetActive(false); // Turn off just this enemy

        gameObject.SetActive(false); // Optional: disable this script object
    }
}
