using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public float followSpeed = 0.2f;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    public Transform target;
    public void FixedUpdate()
    {
        //Vector3 targetPosition = Vector3.SmoothDamp(transform.position, target.position, ref cameraFollowVelocity, followSpeed);
        Vector3 targetPosition = Vector3.Lerp(transform.position, target.position, followSpeed*Time.fixedDeltaTime);
        Vector3 finalPosition = new Vector3(targetPosition.x, targetPosition.y+0.2f, targetPosition.z - 0.5f);
        transform.position = finalPosition;
    }
}
