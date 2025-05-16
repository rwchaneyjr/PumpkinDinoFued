using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    public static float moveSpeed = 3;
    public float leftRightSpeed = 4;
   // public Asteroid asteroid;
   
        void Update()
        {

        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);

           if (Input.GetKey(KeyCode.LeftArrow))
           {
               if (this.gameObject.transform.position.x > LevelBoundary.leftSide)
               {
                   transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed);
               }
           }
           if (Input.GetKey(KeyCode.RightArrow))
           {
               if (this.gameObject.transform.position.x < LevelBoundary.rightSide)
               {
                   transform.Translate(Vector3.right * Time.deltaTime * leftRightSpeed);
               }

           }
        
      // anim.speed = 0;
       // anim.SetBool("run", true);
       // anim.enabled = false;
       transform.localScale = new Vector3(1+Asteroid.i/3, 1 + Asteroid.i / 3, 1 + Asteroid.i / 3);
      //  Debug.Log(1 + Asteroid.i / 3);
        transform.position = new Vector3(transform.position.x,(-14.6f) + Asteroid.i / 6f, transform.position.z);
    }
    }
