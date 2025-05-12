using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoundary : MonoBehaviour
{
  
public static  float leftSide= -20.0f;
public static float rightSide= 60.0f;
public float internalLeft;
public float internalRight;

void Update()
{

        internalLeft = leftSide;
        internalRight = rightSide;


    }

}