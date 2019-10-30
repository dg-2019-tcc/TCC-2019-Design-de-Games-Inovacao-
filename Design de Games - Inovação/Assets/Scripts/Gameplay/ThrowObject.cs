using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public Transform firePoint;

    public GameObject bulletPrefab;

    public static Vector2 direction;

    private void Awake()
    {
        SwipeDetector.OnSwipe += SwipeDirection;
    }


    void Update()
    {
        if(SwipeDetector.shoot == true)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            direction = new Vector2(0.1f, 0);
            Shoot();
        }

    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        SwipeDetector.shoot = false;
    }

    //Funciona somente na build, para conseguir atirar usando a tecla "Z"
    void ShootDebug()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        SwipeDetector.shoot = false;
    }

    private void SwipeDirection(SwipeData data)
    {
        direction = (data.StartPosition - data.EndPosition).normalized;
    }
}
