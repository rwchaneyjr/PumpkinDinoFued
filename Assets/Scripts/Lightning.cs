using System.Collections;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public GameObject lightningBoltPrefab;
    public Transform startPoint; // Player's hand
    public Animator animator;
    public float maxDistance = 50f;
    public string enemyTag = "Enemy"; // Make sure your enemies are tagged correctly

    private bool hasFired = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !hasFired)
        {
            hasFired = true;

            if (animator != null)
                animator.SetBool("zap", true);

            // Find the closest enemy that is active and not zapped
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            GameObject closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            foreach (GameObject enemy in enemies)
            {
                if (!enemy.activeInHierarchy) continue;

                Particle p = enemy.GetComponentInChildren<Particle>();
                if (p == null || p.hasBeenZapped) continue; // Skip already zapped

                float dist = Vector3.Distance(startPoint.position, enemy.transform.position);
                if (dist < maxDistance && dist < closestDistance)
                {
                    closestDistance = dist;
                    closestEnemy = enemy;
                }
            }

            // If a target was found
            if (closestEnemy != null)
            {
                // Create the lightning bolt
                GameObject bolt = Instantiate(lightningBoltPrefab);
                LineRenderer lr = bolt.GetComponent<LineRenderer>();

                if (lr != null)
                {
                    int segments = 20;
                    float jitterAmount = 0.5f;

                    lr.positionCount = segments;
                    Vector3 startPos = startPoint.position;
                    Vector3 endPos = closestEnemy.transform.position;

                    for (int i = 0; i < segments; i++)
                    {
                        float t = (float)i / (segments - 1);
                        Vector3 point = Vector3.Lerp(startPos, endPos, t);

                        // Add jitter
                        Vector3 offset = new Vector3(
                            Random.Range(-jitterAmount, jitterAmount),
                            Random.Range(-jitterAmount, jitterAmount),
                            Random.Range(-jitterAmount, jitterAmount)
                        );

                        lr.SetPosition(i, point + offset);
                    }
                }

                // Trigger the smoke effect
                Particle particle = closestEnemy.GetComponentInChildren<Particle>();
                if (particle != null)
                {
                    particle.StartEffect(); // Mark as zapped and play smoke
                }
            }

            StartCoroutine(ResetFire(0.5f));
        }
    }

    IEnumerator ResetFire(float delay)
    {
        yield return new WaitForSeconds(delay);
        hasFired = false;

        if (animator != null)
            animator.SetBool("zap", false);
    }
}
