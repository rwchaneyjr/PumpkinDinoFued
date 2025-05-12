using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEnemy : MonoBehaviour
{
    public GameObject dino;
    
    public Animator animator2;
    // Start is called before the first frame update
    void Start()
    {

        animator2.SetBool("idle", true);
    }


    // Update is called once per frame
    void Update()
    {

      

        if (Input.GetKey(KeyCode.Y))
        {
         
            animator2.SetBool("walk", true);
            animator2.SetBool("run", false);
            animator2.SetBool("taunt", false);
            animator2.SetBool("idle", false);
            animator2.SetBool("punch", false);
            animator2.SetBool("Bcry", false);
        }
        else if (Input.GetKey(KeyCode.T))
        {

            animator2.SetBool("walk", false);
            animator2.SetBool("run", true);
            animator2.SetBool("taunt", false);
            animator2.SetBool("idle", false);
            animator2.SetBool("punch", false);
            animator2.SetBool("Bcry", false);
        }
        else if (Input.GetKey(KeyCode.R))
        {
        
            animator2.SetBool("walk", false);
            animator2.SetBool("run", false);
            animator2.SetBool("taunt", true);
            animator2.SetBool("idle", false);
            animator2.SetBool("punch", false);
            animator2.SetBool("Bcry", false);
        }
        else if (Input.GetKey(KeyCode.E))
        {

            animator2.SetBool("walk", false);
            animator2.SetBool("run", false);
            animator2.SetBool("taunt", false);
            animator2.SetBool("idle", false);
            animator2.SetBool("punch", false);
            animator2.SetBool("Bcry", true);
        }
        else if (Input.GetKey(KeyCode.U))
        {

            animator2.SetBool("walk", false);
            animator2.SetBool("run", false);
            animator2.SetBool("taunt", false);
            animator2.SetBool("idle", false);
            animator2.SetBool("punch", true);
            animator2.SetBool("Bcry", false);
        }
        else if(Input.GetKey(KeyCode.G))
        {

            animator2.SetBool("walk", false);
            animator2.SetBool("run", false);
            animator2.SetBool("taunt", false);
            animator2.SetBool("idle", true);
            animator2.SetBool("punch", false);
            animator2.SetBool("Bcry", false);
        }
    }
}