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

	[Header("Cooldown em segundos")]
	public float cooldown;
	private float cooldownDelta;

	public GameObject EfeitoDeCooldown;

	[HideInInspector]
    public PhotonView photonView;

    public Animator playerAC;

    public AudioSource tiroSom;

    public static bool dirRight;

    public static bool dirLeft;

    public bool atirou;

    private void Awake()
    {
        photonView = gameObject.GetComponent<PhotonView>();
       // SwipeDetector.OnSwipe += SwipeDirection;
		cooldownDelta = 0;
		EfeitoDeCooldown.SetActive(false);

	}


    void Update()
    {
        if (photonView.IsMine == true)
        {
            if (/*SwipeDetector.shoot == true*/ atirou == true && cooldownDelta <= 0)
            {
                StartCoroutine("StartTiro");
                atirou = false;
                //SwipeDetector.shoot = false;
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                direction = new Vector2(0.5f, 0);
                photonView.RPC("Shoot", RpcTarget.All);
            }

			if (cooldown > 0)
				cooldownDelta -= Time.deltaTime;
        }
    }

    [PunRPC]
    void Shoot()
    {
		if (!(bool)photonView.Owner.CustomProperties["dogValue"] || cooldownDelta > 0) return;
        GameObject bullet;
        bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);// as GameObject;
        bullet.GetComponent<ItemThrow>().InitializeBullet(photonView.Owner);
        tiroSom.Play();
        cooldownDelta = cooldown;
		StartCoroutine("CooldownEffect");
		photonView.Owner.CustomProperties["atirou"] = true;
		SwipeDetector.shoot = false;
    }

    IEnumerator StartTiro()
    {
        playerAC.SetTrigger("Atirou");
        yield return new WaitForSeconds(1f);
        photonView.RPC("Shoot", RpcTarget.All);
    }

	IEnumerator CooldownEffect()
	{
		EfeitoDeCooldown.SetActive(true);
		yield return new WaitForSeconds(cooldownDelta);
		EfeitoDeCooldown.SetActive(false);
	}

    //Funciona somente na build, para conseguir atirar usando a tecla "Z"

    private void SwipeDirection(SwipeData data)
    {
        direction = (data.StartPosition - data.EndPosition).normalized;

        if (data.Direction.Equals("Right"))
        {
            dirRight = true;
        }

        else if (data.Direction.Equals("Left"))
        {
            dirLeft = true;
        }
    }
}
