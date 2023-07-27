using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Landing : MonoBehaviour
{

    private bool isLanded;
    private float resetDelay = 3f;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isLanded) return;
        StartCoroutine(LevelResetDelay());
        isLanded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isLanded = false;
        StopCoroutine(LevelResetDelay());
    }

    IEnumerator LevelResetDelay()
    {
        yield return new WaitForSeconds(resetDelay);
        ReLoadScene();
     //   ResetShip();
     //   groundProcGen.ResetGround();
    }
/*
    private void ResetShip()
    {
        shipPosition.position = new Vector3(60, 70, 0);
        isLanded = false;
    }
*/

    private void ReLoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

