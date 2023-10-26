using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTank : MonoBehaviour
{
    private float liveTime = 80f;
    private float spendedTime;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            other.SendMessage("AddFuel");
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        spendedTime += Time.fixedDeltaTime;
        if (spendedTime>liveTime)
        {
            Destroy(gameObject);
        }
    }
}
