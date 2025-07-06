

using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
public class Rat_Croc : MonoBehaviour
{
    public Transform[] enemies;
    public float detectionDistance = 50f;
    public float stopDistance = 15f;
    public float walkSpeed = 2f;
    //   public float runSpeed = 4f;
    public LayerMask playerLayer;
    private Particle currentTarget;

    public Camera mainCamera; // Assign the main camera in the Inspector
    public LayerMask enemyLayer; // Set this to Enemy layer
    public TextMeshProUGUI zapText; // TMP text that says "Press Z to Zap"

    private Transform player;

    // Optional: use a dictionary to define custom animation sets
    private Dictionary<string, string[]> enemyAnimationSets = new Dictionary<string, string[]>
    {
        { "Rat", new[] { "Bcry","idle", "walk", "taunt" } },
        { "Croc", new[] { "Bcry", "walk", "punch", "idle" } },
        { "Dino", new[] { "idle", "Bcry", "punch", "walk" } },
        { "Stone",new[] { "idle","taunt", "Bcry","walk"} },
         {"Robot", new[] {  "idle","taunt", "Bcry","walk"} }
    };


    public string enemyTag = "Enemy"; // Make sure all enemies are tagged as "Enemy"
    public string idleAnimationParam = "idle"; // Name of the animation parameter (if using triggers/bools)
    void Start()
    {
        zapText.gameObject.SetActive(false);
        // prompt.enabled = false;
        player = GameObject.FindWithTag("Player")?.transform;

        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        enemies = enemyObjects.Select(e => e.transform).ToArray();

        foreach (Transform enemy in enemies)
        {
            // Lift enemy upward by 4 units
            Vector3 pos = enemy.position;
            pos.y += 2f;
            enemy.position = pos;

            // Set idle animation using bools if Animator is available
            Animator animator = enemy.GetComponent<Animator>();
            if (animator == null)
                animator = enemy.GetComponentInChildren<Animator>();

            if (animator != null)
            {
                string[] allStates = { "idle", "walk", "Bcry", "taunt", "punch" };
                foreach (string state in allStates)
                {
                    if (HasParameter(animator, state))
                    {
                        animator.SetBool(state, state == "idle");
                    }
                }
            }
            if (animator != null)
            {
                string[] allStates = { "idle", "walk", "Bcry", "taunt", "punch" };
                foreach (string state in allStates)
                {
                    if (HasParameter(animator, state))
                    {
                        animator.SetBool(state, state == "idle");
                    }
                }
            }
        }
    }


    private void Update()
    {
        if (player == null || enemies == null || enemies.Length == 0) return;

        // 1. Default to hide the zap text and clear target
        zapText.gameObject.SetActive(false);
        currentTarget = null;

        // 2. Enemy AI behavior loop
        foreach (Transform enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.position, player.position);
            Animator animator = enemy.GetComponentInChildren<Animator>();
            if (animator == null) continue;

            string enemyType = enemy.tag;
            if (!enemyAnimationSets.ContainsKey(enemyType)) continue;

            string[] animStates = enemyAnimationSets[enemyType];
            string nextState = "idle";

            if (distance > detectionDistance)
            {
                nextState = "idle";
            }
            else if (distance > stopDistance * 2)
            {
                nextState = "Bcry";
            }
            else if (distance > stopDistance)
            {
                nextState = "walk";
            }
            else
            {
                nextState = "Bcry";
            }

            SetAnimState(animator, nextState, animStates);

            // Optional: Snap Y-position if needed
            if (enemy.tag == "Enemy")
            {
                Vector3 fixedPos = enemy.position;
                fixedPos.y = -14.5f;
                enemy.position = fixedPos;
            }
        }

        // 3. Single raycast to check for zap target
        Ray ray = new Ray(player.position, player.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 50f, enemyLayer))
        {
            float dist = Vector3.Distance(player.position, hit.point);
            if (dist > 35f && dist < 50f)
            {
                Particle p = hit.collider.GetComponentInChildren<Particle>();
                if (p != null && !p.hasBeenZapped)
                {
                    zapText.gameObject.SetActive(true);
                    currentTarget = p;
                }
            }
        }

        // 4. Fire if Z is pressed and a target is locked
        if (Input.GetKeyDown(KeyCode.Z) && currentTarget != null)
        {
            currentTarget.StartEffect();
            zapText.gameObject.SetActive(false);
        }
    }

    public void MoveTowardsPlayer(Transform enemy, float speed)
    {
        Vector3 direction = (player.position - enemy.position).normalized;
        direction.y = 0;
        enemy.position += direction * speed * Time.deltaTime;
        enemy.rotation = Quaternion.LookRotation(direction);
    }


    public void SetAnimState(Animator animator, string activeState, string[] validStates)
    {
        foreach (string state in validStates)
        {
            if (HasParameter(animator, state))
            {
                animator.SetBool(state, state == activeState);
            }
        }
    }


    // This helper checks if the Animator has a parameter
    public bool HasParameter(Animator animator, string paramName)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == paramName)
                return true;
        }
        return false;
    }

    public void RemoveEnemy(Transform enemyToRemove)
    {
        var list = new List<Transform>(enemies);
        list.Remove(enemyToRemove);
        enemies = list.ToArray();
    }
}