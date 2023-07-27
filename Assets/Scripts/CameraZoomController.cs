using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoomController : MonoBehaviour
{

    private CinemachineVirtualCamera vcam;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        GameEvents.current.onCameraZoom += ZoomCamera;
        GameEvents.current.onCameraZoomIdle += UnZoomCamera;
    }

    void ZoomCamera()
    {
        vcam.m_Lens.OrthographicSize = Mathf.Lerp(60, 10, 3);
    }
    void UnZoomCamera()
    {
        vcam.m_Lens.OrthographicSize = Mathf.Lerp(10, 60, 3);
    }


    private void OnDestroy()
    {
        GameEvents.current.onCameraZoom -= ZoomCamera;
        GameEvents.current.onCameraZoomIdle -= UnZoomCamera;
    }
}
