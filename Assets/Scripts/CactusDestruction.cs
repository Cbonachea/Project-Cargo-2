using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusDestruction : MonoBehaviour
{
    private Rigidbody2D cactusSegment;
    private Vector2 stormDirection = new Vector2(-1.0f, 1.0f);
    private Vector2 lateralMovement = new Vector2(-1.0f, 0.5f);
    private int stormStrength = 15;
    private bool isAirborne = false;

    void Start()
    {
        cactusSegment = GetComponent<Rigidbody2D>();
        GameEvents.current.onStorm1 += StormBreakCactus1;
        GameEvents.current.onStorm2 += StormBreakCactus2;
        GameEvents.current.onStorm3 += StormBreakCactus3;
    }

    void FixedUpdate()
    {
        if (isAirborne)
        {
            cactusSegment.AddForce(lateralMovement * 10, ForceMode2D.Force);
        }
    }

    private void StormBreakCactus1()
    {
        if (gameObject.tag == "breakingfirst")
        {
            cactusSegment.AddForce(stormDirection * stormStrength, ForceMode2D.Impulse);
            isAirborne = true;
        }
    }    
    
    private void StormBreakCactus2()
    {
        if (gameObject.tag == "breakingsecond")
        {
            cactusSegment.AddForce(stormDirection * stormStrength, ForceMode2D.Impulse);
            isAirborne = true;
        }
    }    
   
    private void StormBreakCactus3()
    {
        if (gameObject.tag == "breakinglast")
        {
            cactusSegment.constraints = 0;
            cactusSegment.AddForce(stormDirection * stormStrength, ForceMode2D.Impulse);
            isAirborne = true;
        }
    }
}
