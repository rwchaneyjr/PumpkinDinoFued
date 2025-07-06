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

    public static bool walk = false;
    void Start()
    {
         animator = GetComponent<Animator>();
       //animator = GetComponentInChildren<Animator>(); //
        animator.SetBool("walk", true);
        Player.moveSpeed = 2f;
    }

   public IEnumerator ResetZap()
    {
        yield return new WaitForSeconds(1f); // wait 1 second (adjust to your animation length)
        animator.SetBool("zap", false);
        animator.SetBool("walk", true); // go back to walking
        Player.moveSpeed = 2f;
    }

    // Update is called once per frame


    void Update()
        {
           
    

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
            animator.SetBool("walk", false);
            animator.SetBool("zombie", false);
            animator.SetBool("jog", false);
            animator.SetBool("gunPlay", false);
            animator.SetBool("swim", false);
            animator.SetBool("run", false);
            animator.SetBool("unArmedW", false);
            animator.SetBool("handsUp", false);
            animator.SetBool("zap", true);

            // 🔁 Stop previous coroutine before starting a new one
            if (ResetZap() != null)
                StopCoroutine(ResetZap() );

           StartCoroutine(ResetZap());
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
