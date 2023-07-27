using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }


    public event Action onCameraZoom;
    public event Action onCameraZoomIdle;
    public event Action onThrustInput;
    public event Action onThrustIdle;
    public event Action onTorque_L_Input;
    public event Action onTorque_L_InputIdle;
    public event Action onTorque_R_Input;
    public event Action onTorque_R_InputIdle;
    public event Action onOpenCargoHatch;
    public event Action onDrainFuel;
    public event Action onEnterFuelStation;
    public event Action onExitFuelStation;
    public event Action onFuelDepleted;
    public event Action onRequestCargo;
    public event Action onCrash;
    public event Action onDeliverCargo;
    public event Action onPayOut;


    public event Action onStorm1;
    public event Action onStorm2;
    public event Action onStorm3;


    public void CameraZoom()
    {
        if (onCameraZoom != null)
        {
            onCameraZoom();
        }
    }
    public void CameraZoomIdle()
    {
        if (onCameraZoomIdle != null)
        {
            onCameraZoomIdle();
        }
    }
    public void ThrustInput()
    {
        if (onThrustInput != null)
        {
            onThrustInput();
        }
    }
    public void ThrustIdle()
    {
        if (onThrustIdle != null)
        {
            onThrustIdle();
        }
    }    
    public void Torque_L_Input()
    {
        if (onTorque_L_Input != null)
        {
            onTorque_L_Input();
        }
    }
    public void Torque_L_InputIdle()
    {
        if (onTorque_L_InputIdle != null)
        {
            onTorque_L_InputIdle();
        }
    }
    public void Torque_R_Input()
    {
        if (onTorque_R_Input != null)
        {
            onTorque_R_Input();
        }
    }
    public void Torque_R_InputIdle()
    {
        if (onTorque_R_InputIdle != null)
        {
            onTorque_R_InputIdle();
        }
    }
    public void RequestCargo()
    {
        if (onRequestCargo != null)
        {
            onRequestCargo();
        }
    }
    public void OpenCargoHatch()
    {
        if (onOpenCargoHatch != null)
        {
            onOpenCargoHatch();
        }
    }
    public void DrainFuel()
    {
        if (onDrainFuel != null)
        {
            onDrainFuel();
        }
    }
    public void EnterFuelStation()
    {
        if (onEnterFuelStation != null)
        {
            onEnterFuelStation();
        }
    }
    public void ExitFuelStation()
    {
        if (onExitFuelStation != null)
        {
            onExitFuelStation();
        }
    }
    public void FuelDepleted()
    {
        if (onFuelDepleted != null)
        {
            onFuelDepleted();
        }
    }
    public void DeliverCargo()
    {
        if (onDeliverCargo != null)
        {
            onDeliverCargo();
        }
    }
    public void PayOut()
    {
        if (onPayOut != null)
        {
            onPayOut();
        }
    }
    public void Crash()
    {
        if (onCrash != null)
        {
            onCrash();
        }
    }


    public void Storm1()
    {
        if (onStorm1 != null)
        {
            onStorm1();
        }
    }
    public void Storm2()
    {
        if (onStorm2 != null)
        {
            onStorm2();
        }
    }
    public void Storm3()
    {
        if (onStorm3 != null)
        {
            onStorm3();
        }
    }
}