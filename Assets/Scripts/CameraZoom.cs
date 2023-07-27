using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        GameEvents.current.CameraZoom();
    }    
    private void OnTriggerExit2D(Collider2D collision)
    {
        GameEvents.current.CameraZoomIdle();
    }

}
