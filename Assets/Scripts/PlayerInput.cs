using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey("w")) GameEvents.current.ThrustInput();

        if (Input.GetKeyUp("w")) GameEvents.current.ThrustIdle();

        if (Input.GetKey("d")) GameEvents.current.Torque_R_Input(); 
    
        if (Input.GetKeyUp("d")) GameEvents.current.Torque_R_InputIdle(); 

        if (Input.GetKey("a")) GameEvents.current.Torque_L_Input();
        
        if (Input.GetKeyUp("a")) GameEvents.current.Torque_L_InputIdle(); 

        //if (Input.GetKeyDown("s")) 

        if (Input.GetKey("space")) GameEvents.current.OpenCargoHatch();
    }
}