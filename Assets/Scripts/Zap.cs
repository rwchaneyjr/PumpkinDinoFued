using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // Start is called before the first frame update
    public class Zap : MonoBehaviour
    {
       public Animator animator;

        void Start()
        {
            animator = GetComponentInChildren<Animator>(); // or just GetComponent<Animator>() if on same GameObject
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.N)) // Trigger with the Z key
            {
                animator.SetBool("zap", true);
            }
        }
    }
