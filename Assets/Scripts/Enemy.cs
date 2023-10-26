using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject deadEffect;
    public Transform target;
    public Bullet bulletPrefab;
    public float shootReduce = 5f;
    private float spendedTime;
    private void FixedUpdate()
    {
        transform.LookAt(target);
        spendedTime += Time.fixedDeltaTime;
        if (spendedTime>shootReduce)
        {
            Shoot();
            spendedTime = 0;
        }
    }
    private void Shoot()
    {
        Bullet newBullet = Instantiate(bulletPrefab);
        newBullet.isEnemyBullet = true;
        newBullet.transform.position = transform.position + transform.forward*2;
        newBullet.transform.LookAt(target);
    }
    public void ReceiveDamage()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        deadEffect.SetActive(true);
        UIManager.instance.GetExtraScore(3);
        StartCoroutine(DeadCoroutine());
    }
    private IEnumerator DeadCoroutine()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}
