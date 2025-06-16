using System.Collections;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public GameObject Dino;                 // The enemy model to hide (usually a child of the prefab)
    public ParticleSystem smokeParticle;   // Smoke effect to play
    public bool hasBeenZapped = false;
    public GameObject pressZText; // Drag
    private bool hasActivated = false; 
    private void Start()
    {
        if (smokeParticle != null)
            smokeParticle.Stop();
    }

    // Call this when zapped
    public void StartEffect()
    {
        if (!hasActivated && !hasBeenZapped)
        {
            hasActivated = true;
            hasBeenZapped = true;
            StartCoroutine(DoEffect());
           // Hide the text when the enemy is zapped
            EnemyManager manager = FindObjectOfType<EnemyManager>();
            if (manager != null && Dino != null)
            {
                manager.RemoveEnemy(Dino.transform);
            }

        }
    }


    private IEnumerator DoEffect()
    {
        // Optional: small delay before effect
        yield return new WaitForSeconds(0.2f);

        if (smokeParticle != null)
            smokeParticle.Play();

        // Optional delay to let smoke play first
        yield return new WaitForSeconds(0.5f);

        if (Dino != null)
            Dino.SetActive(false); // "Magically" disappear
        smokeParticle.gameObject.SetActive(false);
    }
}
