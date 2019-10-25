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

    // Update is called once per frame
    void Update()
    {
        if(SwipeDetector.shoot == true)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        SwipeDetector.shoot = false;
    }

    private void SwipeDirection(SwipeData data)
    {
        direction = (data.StartPosition - data.EndPosition).normalized;
    }
}
