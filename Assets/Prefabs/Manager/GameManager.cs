using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private int duration = 5;
    [SerializeField]
    private int timeRemaining;
    [SerializeField]
    private bool isCountingDown = false;
    private ParticleSystem rain;
    private int stormCategory = 0;


    private void Awake()
    {
        rain = GetComponentInChildren<ParticleSystem>();
        StormStart();
    }

    private void StormStart()
    {
        if (!isCountingDown)
        {
            isCountingDown = true;
            timeRemaining = duration;
            Invoke("StormTick", 1f);
        }
    }
    private void FinalCountDown()
    {

    }
    private void StormTick()
    {
        timeRemaining--;
        if (timeRemaining > 0)
        {
            Invoke("StormTick", 1f);
        }
        else
        {
            isCountingDown = false;
            IncreaseStormIntensity();
        }
    }
    private void FinalCountDownTick()
    {
        timeRemaining--;
        if (timeRemaining > 0)
        {
            Invoke("FinalCountDownTick", 1f);
        }
        else
        {
            isCountingDown = false;
            IncreaseStormIntensity();
        }
    }

    private void IncreaseStormIntensity()
    {
        stormCategory++;

        if (stormCategory == 1)
            GameEvents.current.Storm1();

        if (stormCategory == 2)
            GameEvents.current.Storm2();

        if (stormCategory == 3)
        {
            GameEvents.current.Storm3();
            Debug.Log("Storm Category = " + stormCategory);
            return;
        }

        Debug.Log("Storm Category = "+ stormCategory);
        StormStart();
    }
}