using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneController : MonoBehaviour
{
    public static AirplaneController instance;
    public GameObject deadEffect;
    public GameObject prop;
    public bool isCanFly;
    private bool isAlive;
    public float fuel;
    public Joystick joystick;
    public float forwardSpeed = 25f;
    public float strafeSpeed = 7.5f;
    public float hoverSpeed = 5f;
    private float activeForwardSpeed;
    private float activeStrafeSpeed;
    private float activeHoverSpeed;
    private float forwardAcceleration=10f;
    private float strafeAcceleration=5f;
    private float hoverAcceleration=5f;
    private void Awake()
    {
        instance = this;
        fuel = 100;
        activeForwardSpeed = forwardSpeed;
        isAlive = true;
    }
    private void FixedUpdate()
    {
        if (isCanFly)
        {
            transform.position += transform.forward * activeForwardSpeed * Time.fixedDeltaTime;
            //Debug.Log("Fuel: " + fuel);
        }
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, joystick.Horizontal * strafeSpeed, strafeAcceleration * Time.fixedDeltaTime);
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, joystick.Vertical * hoverSpeed, hoverAcceleration * Time.fixedDeltaTime);

        float rotationY = 1 * activeStrafeSpeed * Time.fixedDeltaTime;
        float rotationX = 1 * activeHoverSpeed * Time.fixedDeltaTime;
        Vector3 finalRotation = new Vector3(rotationX, rotationY);
        transform.Rotate(finalRotation);
        fuel -= 0.03f;
        if (fuel<=0)
        {
            GetComponent<Rigidbody>().useGravity = true;
            prop.GetComponent<Rotate360>().enabled = false;
            isCanFly = false;
            Debug.Log("Fuel not enough");
        }
        if (transform.position.y<-12)
        {
            isCanFly = false;
            deadEffect.SetActive(true);
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;
            isAlive = false;
        }
    }
    private void Dead()
    {

    }
    public void ReceiveDamage()
    {
        fuel -= 5;
    }
    public void AddFuel()
    {
        fuel += 30f;
    }
    public bool GetIsCanFly()
    {
        return isCanFly;
    }
    public bool GetIsAlive()
    {
        return isAlive;
    }
    public float GetFuel()
    {
        return fuel;
    }
    public Transform GetPlayerTransform()
    {
        return transform;
    }
}
