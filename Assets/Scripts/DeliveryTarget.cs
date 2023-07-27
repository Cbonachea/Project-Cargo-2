using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryTarget : MonoBehaviour
{
    [SerializeField]
    private GameObject[] residences;
    [SerializeField]
    private GameObject deliveryTarget;
    private int index;

    private void Start()
    {
        GameEvents.current.onRequestCargo += SetTarget;
    }
    private void SetTarget()
    {
        index = Random.Range(0, residences.Length);
        deliveryTarget = residences[index];
        deliveryTarget.tag = "deliveryTarget";
    }
    private void OnDestroy()
    {
        GameEvents.current.onRequestCargo -= SetTarget;
    }
}