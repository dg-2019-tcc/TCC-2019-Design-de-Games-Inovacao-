using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class ThrowObject : MonoBehaviour
{
	[Header("Alvo e Prefab")]
	public Transform firePoint;
    public GameObject bulletPrefab;

    public static Vector2 direction;

	[HideInInspector]
    public PhotonView photonView;

    public Animator playerAC;

    private void Awake()
    {
        photonView = gameObject.GetComponent<PhotonView>();
        SwipeDetector.OnSwipe += SwipeDirection;
    }


    void Update()
    {
        if (photonView.IsMine == true)
        {
            if (SwipeDetector.shoot == true) Shoot();
            
            if (Input.GetKeyDown(KeyCode.Z))
            {
                direction = new Vector2(0.5f, 0);
                photonView.RPC("Shoot", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    void Shoot()
    {
		if (!(bool)photonView.Owner.CustomProperties["dogValue"]) return;
        GameObject bullet;
        playerAC.SetTrigger("Atirou");
        bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);// as GameObject;
        bullet.GetComponent<ItemThrow>().InitializeBullet(photonView.Owner);

        SwipeDetector.shoot = false;
    }

    //Funciona somente na build, para conseguir atirar usando a tecla "Z"

    private void SwipeDirection(SwipeData data)
    {
        direction = (data.StartPosition - data.EndPosition).normalized;
    }
}
