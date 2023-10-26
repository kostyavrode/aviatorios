using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate360 : MonoBehaviour
{
    public float rotateX;
    public float rotateY;
    public float rotateZ;
    private void Update()
    {
        transform.Rotate(rotateX, rotateY, rotateZ);
    }
}
