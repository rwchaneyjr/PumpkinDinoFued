using UnityEngine;

public class TextScript : MonoBehaviour
{
    public Transform player;  // Assign your player Transform in Inspector
    public TMPro.TextMeshProUGUI zapText;  // Assign the TMP UI Text in Inspector
    public string enemyTag = "Enemy";      // Make sure enemies are tagged
    public float zapMinDistance = 35f;
    public float zapMaxDistance = 50f;

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(player.position, enemy.transform.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null && closestDistance > zapMinDistance && closestDistance < zapMaxDistance)
        {
            zapText.gameObject.SetActive(true);
        }
        else
        {
            zapText.gameObject.SetActive(false);
        }
    }
}
