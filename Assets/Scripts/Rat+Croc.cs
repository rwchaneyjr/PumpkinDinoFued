using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Rat_Croc : MonoBehaviour
{
    private Dictionary<Transform, float> animTimers = new();
    private float animationCycleTime = 2.5f;
    private Dictionary<Transform, string> currentAnimState = new(); // track what each enemy is playing

    public Transform[] enemies;
    public float detectionDistance = 50f;
    public float stopDistance = 15f;
    public float walkSpeed = 2f;
    private Particle currentTarget;

    public Camera mainCamera;
    public LayerMask enemyLayer;
    public TextMeshProUGUI zapText;

    private Transform player;

    public string enemyTag = "Enemy";
    // public TextMeshProUGUI pressZText;  // drag TMP component here
    //   zapText.text = "Press Z to Zap";

    private string[] animChoices = { "idle", "Bcry", "walk" };

    void Start()
    {
        // zapText.gameObject.SetActive(false);
        player = GameObject.FindWithTag("Player")?.transform;
        zapText.text = "Press Z to Zap";
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        enemies = enemyObjects.Select(e => e.transform).ToArray();

        foreach (Transform enemy in enemies)
        {
            Vector3 pos = enemy.position;
            if (pos.y > -15.6)
                pos.y += .01f;
            enemy.position = pos;

            animTimers[enemy] = Random.Range(0f, animationCycleTime); // desync animations
            currentAnimState[enemy] = "idle";

            Animator animator = enemy.GetComponent<Animator>() ?? enemy.GetComponentInChildren<Animator>();

            if (animator != null)
            {
                foreach (string state in animChoices)
                {
                    if (HasParameter(animator, state))
                    {
                        animator.SetBool(state, state == "idle"); // start with idle
                    }
                }
            }
        }
    }

    void Update()
    {
        if (player == null || enemies == null || enemies.Length == 0) return;

        zapText.gameObject.SetActive(false);
        currentTarget = null;

        foreach (Transform enemy in enemies)
        {
            Animator animator = enemy.GetComponentInChildren<Animator>();
            if (animator == null) continue;

            animTimers[enemy] += Time.deltaTime;
            if (animTimers[enemy] >= animationCycleTime)
            {
                animTimers[enemy] = 0f;

                // Pick a random state for this enemy
                string newState = animChoices[Random.Range(0, animChoices.Length)];
                currentAnimState[enemy] = newState;

                SetAnimState(animator, newState, animChoices);
            }

            // Optional: Snap to ground
            if (enemy.tag == "Enemy")
            {
                Vector3 fixedPos = enemy.position;
                  fixedPos.y = -14.7f;
                enemy.position = fixedPos;
            }
        }

        // Zap targeting logic
        Vector3 angledDirection = Quaternion.Euler(-10f, 0, 0) * player.forward;
        Ray ray = new Ray(player.position + Vector3.up * 1.5f, angledDirection);

        if (Physics.Raycast(ray, out RaycastHit hit, 50f, enemyLayer))
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
