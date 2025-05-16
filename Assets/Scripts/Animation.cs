using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class Animation : MonoBehaviour
{
    public GameObject player;
 //   public GameObject BigBird;
    public Animator animator;
    public static int _count = 0;
    // Start is called before the first frame update
    void Start()
    {
         animator = GetComponent<Animator>();
       //animator = GetComponentInChildren<Animator>(); //
        animator.SetBool("walk", true);
    }
 

    // Update is called once per frame
   
   
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z)) // Trigger with the Z key
            {
                animator.SetBool("zap", true);
            }
        
    

      //  animator.SetBool("flying", true);
      //  BigBird.transform.Translate(Vector3.forward * .02f);
       // BigBird.transform.localScale = new Vector3(50 + Asteroid.i*3 , 50 + Asteroid.i*3 , 50 + Asteroid.i*3 );
      //  transform.position = new Vector3(transform.position.x, (-14.5f) + Asteroid.i / 5f, transform.position.z);


        if(Input.GetKey(KeyCode.UpArrow))
        {
            Player.moveSpeed = 2f;
            animator.SetBool("walk", true);
            animator.SetBool("zombie", false);
            animator.SetBool("jog", false);
            animator.SetBool("gunPlay", false);
            animator.SetBool("handsUp", false);
            animator.SetBool("swim", false);
            animator.SetBool("run", false);
            animator.SetBool("unArmedW", false);
            animator.SetBool("zap", false);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Player.moveSpeed = 3f;
            animator.SetBool("walk", false);
            animator.SetBool("zombie", false);
            animator.SetBool("jog", true);
            animator.SetBool("gunPlay", false);
            animator.SetBool("handsUp", false);
            animator.SetBool("swim", false);
            animator.SetBool("run", false);
            animator.SetBool("unArmedW", false);
            animator.SetBool("zap", false);
        }
        if (Input.GetKey(KeyCode.M))
        {
            Player.moveSpeed = 4f;
            animator.SetBool("walk", false);
            animator.SetBool("zombie", false);
            animator.SetBool("jog", false);
            animator.SetBool("gunPlay", false);
            animator.SetBool("handsUp", false);
            animator.SetBool("swim", false);
            animator.SetBool("run", true);
            animator.SetBool("unArmedW", false);
            animator.SetBool("zap", false);
        }
       
        if (Input.GetKey(KeyCode.Z))
        {
            
            Player.moveSpeed = 0f;
             animator.SetBool("spell", false);
            animator.SetBool("walk",false);
            animator.SetBool("zombie", false);
            animator.SetBool("jog", false);
            animator.SetBool("gunPlay", false);
            animator.SetBool("swim", false);
            animator.SetBool("run", false);
            animator.SetBool("unArmedW", false);
            animator.SetBool("handsUp", false);
            animator.SetBool("zap", true);

            // animator.Play("Hands Forward Gesture");
        }
        if (Input.GetKey(KeyCode.B))
        {
            Player.moveSpeed = 2f;
            animator.SetBool("walk", false);
            animator.SetBool("zombie", false);
            animator.SetBool("jog", false);
            animator.SetBool("gunPlay", false);
            animator.SetBool("handsUp", false);
            animator.SetBool("swim", true);
            animator.SetBool("run", false);
            animator.SetBool("unArmedW", false);
            animator.SetBool("zap", false);
        }
       // animator.SetBool("spell", false);
    }
}
