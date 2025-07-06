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

            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            GameObject closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            foreach (GameObject enemy in enemies)
            {
                if (!enemy.activeInHierarchy) continue;

                float dist = Vector3.Distance(startPoint.position, enemy.transform.position);
                if (dist < maxDistance && dist < closestDistance)
                {
                    closestDistance = dist;
                    closestEnemy = enemy;
                }
            }

            if (closestEnemy != null)
            {
                GameObject bolt = Instantiate(lightningBoltPrefab);
                LineRenderer lr = bolt.GetComponent<LineRenderer>();

                if (lr != null)
                {
                    int segments = 20;
                    float jitterAmount = 0.7f;

                    lr.positionCount = segments;
                    Vector3 startPos = startPoint != null
                        ? startPoint.position
                        : transform.position + new Vector3(0, 1f, 0); // fallback to chest center

                    Vector3 endPos = closestEnemy.transform.position + new Vector3(0, 1f, 0); // aim at chest

                    for (int i = 0; i < segments; i++)
                    {
                        float t = (float)i / (segments - 1);
                        Vector3 point = Vector3.Lerp(startPos, endPos, t);
                        Vector3 offset = new Vector3(
                            Random.Range(-jitterAmount, jitterAmount),
                            Random.Range(-jitterAmount, jitterAmount),
                            Random.Range(-jitterAmount, jitterAmount)
                        );
                        lr.SetPosition(i, point + offset);
                    }
                }

                Particle particle = closestEnemy.GetComponentInChildren<Particle>();
                if (particle != null)
                {
                    particle.StartEffect();
                }
            }

            // ✅ Reset zap after delay
            StartCoroutine(ResetFire(1f));
        }
    }



    public IEnumerator ResetFire(float delay)
   {
       yield return new WaitForSeconds(delay);
       hasFired = false;

       if (animator != null)
          animator.SetBool("zap", false);
   }

}
