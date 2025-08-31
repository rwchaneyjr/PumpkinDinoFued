using UnityEngine;

public class EnemyManager2 : MonoBehaviour
{
    public Transform[] enemies;
    public float detectionDistance = 50f;
    public float stopDistance = 15f;
    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    public LayerMask playerLayer;
    public GameObject pressZText; // Drag your "Press Z" UI object here

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        pressZText.SetActive(false); // Hide by default
    }

    private void Update()
    {
        if (player == null || enemies == null || enemies.Length == 0) return;

        bool shouldShowText = false;

        foreach (Transform enemy in enemies)
        {
            if (enemy == null) continue;
            float distance = Vector3.Distance(enemy.position, player.position);
            Debug.Log(distance);
            Animator animator = enemy.GetComponentInChildren<Animator>();
            if (animator == null) continue;



            if (distance > detectionDistance)
            {
                SetAnimState(animator, "walk");
            }
            else if (distance > stopDistance * 2)
            {
                SetAnimState(animator, "taunt");
                MoveTowardsPlayer(enemy, runSpeed);
            }
            else if (distance > stopDistance)
            {
                SetAnimState(animator, "Bcry");
                MoveTowardsPlayer(enemy, walkSpeed);
            }
            else
            {
                SetAnimState(animator, "taunt");
            }

            // Show text only if one enemy is in range
            if (distance < 50f && distance > 35f)
                shouldShowText = true;
        }

        pressZText.SetActive(shouldShowText);
    }
    public void RemoveEnemy(Transform enemyToRemove)
    {
        var list = new System.Collections.Generic.List<Transform>(enemies);
        list.Remove(enemyToRemove);
        enemies = list.ToArray();
    }


    private void MoveTowardsPlayer(Transform enemy, float speed)
    {
        Vector3 direction = (player.position - enemy.position).normalized;
        direction.y = 0;
        enemy.position += direction * speed * Time.deltaTime;
        enemy.rotation = Quaternion.LookRotation(direction);
    }

    private void SetAnimState(Animator animator, string activeState)
    {
        string[] allStates = { "Bcry", "idle", "taunt", "walk", "punch", "run" };
        foreach (string state in allStates)
        {
            animator.SetBool(state, state == activeState);
        }
    }
}
