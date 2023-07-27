using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelSystem : MonoBehaviour
{
    [SerializeField]
    private float maxFuel = 100f;
    [SerializeField]
    private float currentFuel;
    [SerializeField]
    private float fuelConsumption = .2f;
    private float fuelRefill = 1f;
    private bool isFuelConsumptionStarted;
    private bool isFuelRefillStarted;
    private Rigidbody2D rb_ship;



    void Awake()
    {
        InitializeFuelSystem();
    }
    private void Update()
    {
        if (currentFuel <= 0)
        {
            GameEvents.current.FuelDepleted();
        }
    }
    private void InitializeFuelSystem()
    {
        SubscribeGameEvents();
        PrimeEngine();
        rb_ship = GetComponent<Rigidbody2D>();
    }
    private void SubscribeGameEvents()
    {
        GameEvents.current.onThrustInput += DrainFuel;
        GameEvents.current.onThrustIdle += StopDrainingFuel;
        GameEvents.current.onEnterFuelStation += RefillFuel;
        GameEvents.current.onExitFuelStation += StopRefillingFuel;
        Debug.Log("Fuel System Events Subscribed");
    }
    private void PrimeEngine()
    {
        currentFuel = maxFuel;
    }

    private void DrainFuel()
    {
        if (!isFuelConsumptionStarted)
        {
            isFuelConsumptionStarted = true;
            StartCoroutine(co_DrainFuel());
        }
    }
    private void StopDrainingFuel()
    {
        StopCoroutine(co_DrainFuel());
        isFuelConsumptionStarted = false;
    }
    private IEnumerator co_DrainFuel()
    {
        while (currentFuel > 0 && isFuelConsumptionStarted)
        {
            currentFuel -= fuelConsumption;
            yield return new WaitForSeconds(.1f);
        }
    }

    private void RefillFuel()
    {
        if (!isFuelRefillStarted)
        {
            isFuelRefillStarted = true;
            StartCoroutine(co_RefillFuel());
        }
    }
    public void StopRefillingFuel()
    {
        StopCoroutine(co_RefillFuel());
        isFuelRefillStarted = false;
    }
    private IEnumerator co_RefillFuel()
    {
        while (currentFuel < maxFuel)
        {
            currentFuel += fuelRefill;
            yield return new WaitForSeconds(.1f);
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "FuelStation") GameEvents.current.EnterFuelStation();
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "FuelStation") GameEvents.current.ExitFuelStation();
    }
    private void OnDestroy()
    {
        GameEvents.current.onThrustInput -= DrainFuel;
        GameEvents.current.onThrustIdle -= StopDrainingFuel;
        GameEvents.current.onEnterFuelStation -= RefillFuel;
        GameEvents.current.onExitFuelStation -= StopRefillingFuel;
    }
}
