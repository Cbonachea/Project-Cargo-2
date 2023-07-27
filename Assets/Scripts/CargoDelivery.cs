using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoDelivery : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "deliveryTarget")
        {
            GameEvents.current.DeliverCargo();
            Destroy(gameObject);
        }
    }
}
