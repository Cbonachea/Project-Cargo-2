using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{

    private CinemachineVirtualCamera vcam;
    [SerializeField] private Transform shipTransform;
    private float height;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        height = shipTransform.position.y;
        if (height >= 60)
        {
            height = 60;
        }
        if (height <= 10)
        {
            height = 10;
        }
    }

    void FixedUpdate()
    {
        { vcam.m_Lens.OrthographicSize = height; }
    }
}