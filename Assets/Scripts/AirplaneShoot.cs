using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneShoot : MonoBehaviour
{
    public Bullet bulletPrefab;
    public AudioSource shootSound;
    public static AirplaneShoot instance;
    private void Awake()
    {
        instance = this;
    }
    public void Shoot()
    {
        shootSound.Play();
        Bullet newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = transform.position+transform.forward;
        newBullet.transform.rotation = transform.rotation;
    }
}
