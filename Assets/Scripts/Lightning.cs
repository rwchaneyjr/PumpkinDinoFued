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
        if (Input.GetKeyDown(KeyCode.Z) )//&& !hasFired)
        {
            hasFired = true;

            if (animator != null)
                animator.SetBool("zap", true);

            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            GameObject closestEnemy = null;
            float closestDistance = Mathf.Infinity;

         
            foreach (GameObject enemy in enemies)
            {
                if (!enemy.activeInHierarchy)
                {
                    Debug.Log(enemy.name + " is inactive.");
                    continue;
                }

                Particle p = enemy.GetComponentInChildren<Particle>();
                if (p == null)
                {
                    Debug.Log(enemy.name + " has no Particle script.");
                    continue;
                }

                if (p.hasBeenZapped)
                {
                    Debug.Log(enemy.name + " has already been zapped.");
                    continue;
                }

                float dist = Vector3.Distance(startPoint.position, enemy.transform.position);
                Debug.Log(enemy.name + " distance: " + dist);

                if (dist < maxDistance && dist < closestDistance)
                {
                    closestDistance = dist;
                    closestEnemy = enemy;
                    Debug.Log("New closest: " + closestEnemy.name);
                }
            }

            if (closestEnemy == null)
            {
                Debug.LogWarning("⚠️ No enemy found to zap. Check tags, zapped status, and distance.");
            }
            else
            {
                Debug.Log("✅ Zapping " + closestEnemy.name);
                Particle particle = closestEnemy.GetComponentInChildren<Particle>();
                if (particle != null) particle.StartEffect();
            }


            if (closestEnemy != null)
            {
                Vector3 enemyChestPos = closestEnemy.transform.position + Vector3.up * 1.5f;
                Vector3 direction = (enemyChestPos - startPoint.position).normalized;
                float distance = Vector3.Distance(startPoint.position, enemyChestPos);
                Debug.DrawLine(startPoint.position, enemyChestPos, Color.red, 2f);



                if (Physics.Raycast(startPoint.position, direction, out RaycastHit hit, distance))
                {
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        GameObject bolt = Instantiate(lightningBoltPrefab);
                        LineRenderer lr = bolt.GetComponent<LineRenderer>();

                        if (lr != null)
                        {
                            int segments = 20;
                            float jitterAmount = 0.7f;

                            lr.positionCount = segments;
                            Vector3 startPos = startPoint.position;
                            Vector3 endPos = enemyChestPos;

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

                        Particle particle = hit.collider.GetComponentInParent<Particle>();
                        Debug.DrawRay(startPoint.position, direction * distance, Color.red, 1f);

                        Debug.Log("Hit: " + hit.collider.name);
                        if (particle != null)
                        {
                            particle.StartEffect();
                        }

                    }
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

        Debug.Log("Zap is ready again.");
    }
}
