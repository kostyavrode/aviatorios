using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public GameObject effect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            UIManager.instance.GetExtraScore(1);
            effect.SetActive(true);
        }
    }
}
