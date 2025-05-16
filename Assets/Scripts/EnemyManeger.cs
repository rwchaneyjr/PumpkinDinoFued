using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform[] enemies;
    public float detectionDistance = 30f;
    public float stopDistance = 15f;
    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    public LayerMask playerLayer;
    float closestDistance = 50;
    private Transform player;
    public static GameObject text;
    private void Start()
    {
        text.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

   
        private void Update()
    {
        if (player == null) return;

        float closestDistance =20f; ;


        foreach (Transform enemy in enemies)
        {
            if (enemy == null) continue;

            Animator animator = enemy.GetComponentInChildren<Animator>();
            if (animator == null) continue;

            float distance = Vector3.Distance(enemy.position, player.position);
           
           closestDistance = Mathf.Min(closestDistance, distance);
            if (distance > detectionDistance)
            {
                animator.SetBool("Bcry", true);
                animator.SetBool("idle", false);
                animator.SetBool("taunt", false);
                animator.SetBool("walk", false);
                animator.SetBool("punch", false);
                animator.SetBool("run", false);
            }

            else if (distance > stopDistance * 2)
            {
                animator.SetBool("Bcry", false);
                animator.SetBool("idle", false);
                animator.SetBool("taunt", false);
                animator.SetBool("walk", true);
                animator.SetBool("punch", false);
                animator.SetBool("run", false);

                MoveTowardsPlayer(enemy, runSpeed); // <--- RUN toward player
            }
            else if (distance > stopDistance)
            {
                animator.SetBool("Bcry", false);
                animator.SetBool("idle", false);
                animator.SetBool("taunt", true);
                animator.SetBool("walk", false);
                animator.SetBool("punch", false);
                animator.SetBool("run", false);

                MoveTowardsPlayer(enemy, walkSpeed); // <--- SLOW walk
            }
            else
            {
                animator.SetBool("Bcry", false);
                animator.SetBool("idle", false);
                animator.SetBool("taunt", false);
                animator.SetBool("walk", false);
                animator.SetBool("punch", true);
                animator.SetBool("run", false);
            }
           
          
           

                text.SetActive(false);
            if (distance < 30f && distance > 10f)
            {
                text.SetActive(true);
            }
            else
            {
                text.SetActive(false);
            }
            

        }
    }

    private void MoveTowardsPlayer(Transform enemy, float speed)
    {
        Vector3 direction = (player.position - enemy.position).normalized;
        direction.y = 0; // Prevent vertical movement
        enemy.position += direction * speed * Time.deltaTime;
        enemy.rotation = Quaternion.LookRotation(direction); // Face player
    }

    // Helper to switch animation cleanly
   
}

    