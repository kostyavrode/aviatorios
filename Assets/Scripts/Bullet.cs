using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float liveTime = 20f;
    private float spendedTime;
    public bool isEnemyBullet;
    private void FixedUpdate()
    {
        transform.position += transform.forward * bulletSpeed * Time.fixedDeltaTime;
        spendedTime += Time.fixedDeltaTime;
        if (spendedTime>liveTime)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if ((other.tag=="Player" && isEnemyBullet) || (other.tag == "Enemy" && !isEnemyBullet))
        {
            other.SendMessage("ReceiveDamage");
        }
    }
}
