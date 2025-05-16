using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public GameObject lightningBoltPrefab;
    public Transform startPoint;
    public int maxTargets = 5;
    public float maxDistance = 50f;
    public float minSeparationBetweenTargets = 30f; // prevent double zaps on overlapping enemies
    public Animator animator;

    private bool hasFired = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !hasFired)
        {
            animator.SetBool("zap", true);
            FireLightning();
            hasFired = true;
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            animator.SetBool("zap", false);
            hasFired = false;
        }


    
            if (Input.GetKeyDown(KeyCode.Z) && !hasFired)
            {
                hasFired = true;
                animator.SetBool("zap", true);
                FireLightning();
                StartCoroutine(ResetFire(0.5f));
          
            }
        }

        IEnumerator ResetFire(float delay)
        {
            yield return new WaitForSeconds(delay);
            hasFired = false;
            animator.SetBool("zap", false);
    
        }

        void FireLightning()
    {
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

        var sortedEnemies = enemyObjects
            .Where(e => e.activeInHierarchy)
            .OrderBy(e => Vector3.Distance(startPoint.position, e.transform.position))
            .ToList();

        List<Vector3> zapPositions = new List<Vector3>();
        int targetsHit = 0;

        foreach (var enemy in sortedEnemies)
        {
            if (targetsHit >= maxTargets) break;

            float distance = Vector3.Distance(startPoint.position, enemy.transform.position);
            if (distance > maxDistance) continue;

            // Skip enemies too close to ones already zapped
            bool tooCloseToZapped = zapPositions.Any(pos =>
                Vector3.Distance(pos, enemy.transform.position) < minSeparationBetweenTargets);

            if (tooCloseToZapped) continue;

            // Spawn lightning
            GameObject bolt = Instantiate(lightningBoltPrefab);
            LightningBolt boltScript = bolt.GetComponent<LightningBolt>();
            if (boltScript != null)
            {
                boltScript.Initialize(startPoint, enemy.transform);
            }

            enemy.SetActive(false);
            zapPositions.Add(enemy.transform.position);
            targetsHit++;
        }
    }
}
