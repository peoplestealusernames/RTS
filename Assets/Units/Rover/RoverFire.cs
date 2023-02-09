using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverFire : MonoBehaviour
{
    public TurretController Controller;
    public GameObject BulletRef;

    public float BulletSpeed = 100;

    public void Start()
    {
        Controller.Fire.AddListener(Fire);
    }

    void Fire()
    {
        GameObject Bullet = Instantiate(BulletRef, BulletRef.transform.position, BulletRef.transform.rotation);
        Bullet.SetActive(true);
        Bullet.GetComponent<Rigidbody>().velocity = Vector3.Normalize(Bullet.transform.forward) * BulletSpeed;
    }
}
