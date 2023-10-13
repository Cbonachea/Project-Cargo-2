using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{

    [SerializeField] private bool isControlling;
    [SerializeField] private Rigidbody2D rb_ship;
    [SerializeField] private int hullIntegrity = 0;
    private SpriteRenderer shipSprite;
    private ParticleSystem flames;
    //private Rigidbody2D rb_cargo;
    //[SerializeField] private Transform cargoDropLocation;
    //[SerializeField] private bool hasCargo;
    //public GameObject cargo;
    private AudioManager audioManager;
    private ParticleSystem explosion;
    private enum EngineStatus { on, off };
    private EngineStatus engineStatus = EngineStatus.off;
    
    private bool torque_L;
    private bool torque_R;
    
    [SerializeField]
    [Range(0.0f, 150f)]
    private float thrust;
    
    [SerializeField]
    [Range(0.0f, 70.0f)]
    private float torque;

    [SerializeField]
    [Range(0.0f, 100.0f)]
    private float crashTolerance = 3f;
    private bool crashwait = false;
    private int crashwaitvalue = 80;

    void Start()
    {
        InitializeShip();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void FixedUpdate()
    {
        if (!isControlling) return;
        if (engineStatus == EngineStatus.on) Thrust();
        if (torque_L == true) Torque_L();
        if (torque_R == true) Torque_R();
        if (crashwait == true)
        {
            crashwaitvalue--;
            //Debug.Log("waiting");
        } 
        if(crashwaitvalue == 0)
        {
            crashwait = false;
            crashwaitvalue = 80;
            //Debug.Log("Done waiting");
        }
    }


    private void InitializeShip()
    {
        rb_ship = GetComponent<Rigidbody2D>();
        flames = transform.GetChild(1).GetComponentInChildren<ParticleSystem>();
        explosion = transform.GetChild(2).GetComponentInChildren<ParticleSystem>();
        shipSprite = GetComponent<SpriteRenderer>();
        isControlling = true;
        SubscribeGameEvents();
        Debug.Log("Ship Initialized - Systems Nominal");
    }
    private void SubscribeGameEvents()
    {
        GameEvents.current.onThrustInput += EngineOn;
        GameEvents.current.onThrustIdle += EngineOff;
        GameEvents.current.onTorque_L_Input += OnTorque_L;
        GameEvents.current.onTorque_L_InputIdle += Torque_L_Idle;
        GameEvents.current.onTorque_R_Input += OnTorque_R;
        GameEvents.current.onTorque_R_InputIdle += Torque_R_Idle;
        //GameEvents.current.onRequestCargo += LoadCargo;
        //GameEvents.current.onOpenCargoHatch += DropCargo;
        GameEvents.current.onCrash += CheckHull;
        GameEvents.current.onFuelDepleted += NoFuel;
        Debug.Log("Game Events Subscribed");
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude >= crashTolerance) GameEvents.current.Crash();
        else if (collision.gameObject.tag == "Restaurant") GameEvents.current.RequestCargo();
    }
    private void CheckHull()
    {
        if(hullIntegrity >= 1 && !crashwait)
        {
            crashwait = true;
            hullIntegrity--;
            Debug.Log("Hull Integrity = " + hullIntegrity);
        }
        if(hullIntegrity < 1 && !crashwait)
        {
            Explode();
        }
    }

    private void Explode()
    {
        isControlling = false;
        rb_ship.angularDrag = 0f;
        rb_ship.gravityScale = 0.4f;
        shipSprite.enabled = false;
        audioManager.Play("Explosion");
        rb_ship.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        explosion.Play();
        EngineOff();
        Debug.Log("Ship Busted");
    }

    /*
    private void LoadCargo()
    {
        if(hasCargo == false)
        {
            Debug.Log("Populate cargo UI Element here");
            hasCargo = true;
        }
    }
    private void DropCargo()
    {
        if(hasCargo == true)
        {
            var cargoInstance = Instantiate(cargo, cargoDropLocation.position, cargoDropLocation.rotation) as GameObject;
            rb_cargo = cargoInstance.GetComponent<Rigidbody2D>();
            rb_cargo.velocity = rb_ship.velocity;
            rb_cargo.angularVelocity = rb_ship.angularVelocity;
            hasCargo = false;
        }
    }
    */
    private void EngineOn()
    {
        if (!isControlling) return;
        if (engineStatus == EngineStatus.on) return;
        engineStatus = EngineStatus.on;
        flames.Play();
        audioManager.Play("EngineSound");
    }
    private void EngineOff()
    {
        if (engineStatus == EngineStatus.off) return;
        engineStatus = EngineStatus.off;
        flames.Stop();
        audioManager.Stop("EngineSound");

    }
    private void OnTorque_L()
    {
        if (torque_L == true) return;
        torque_L = true;
    }
    private void Torque_L_Idle() 
    {
        if (torque_L == false) return;
        torque_L = false; 
    }
    private void OnTorque_R()
    {
        if (torque_R == true) return;
        torque_R = true;
    }
    private void Torque_R_Idle() 
    {
        if (torque_R == false) return;
        torque_R = false; 
    }
    private void NoFuel()
    {
        EngineOff();
        isControlling = false;
    }


    //Adding forces to the rigid body [fixed update]
    private void Thrust() { rb_ship.AddForce(transform.up * thrust); }
    private void Torque_L() { rb_ship.AddTorque(torque * Mathf.Deg2Rad, ForceMode2D.Impulse); }
    private void Torque_R() { rb_ship.AddTorque(-torque * Mathf.Deg2Rad, ForceMode2D.Impulse); }


    private void OnDestroy()
    {
        GameEvents.current.onThrustInput -= EngineOn;
        GameEvents.current.onThrustIdle -= EngineOff;
        GameEvents.current.onTorque_L_Input -= OnTorque_L;
        GameEvents.current.onTorque_L_InputIdle -= Torque_L_Idle;
        GameEvents.current.onTorque_R_Input -= OnTorque_R;
        GameEvents.current.onTorque_R_InputIdle -= Torque_R_Idle;
        //GameEvents.current.onRequestCargo += LoadCargo;
        //GameEvents.current.onOpenCargoHatch -= DropCargo;
    }

}
