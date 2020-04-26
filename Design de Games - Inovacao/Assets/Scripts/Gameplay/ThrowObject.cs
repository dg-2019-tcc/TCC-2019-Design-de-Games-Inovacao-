using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;


public class ThrowObject : MonoBehaviour
{
	[Header("Alvo e Prefab")]
	public Transform firePoint;
    public GameObject bulletPrefab;

    public static Vector2 direction;

    public GameObject tiroButton;
    private Image tiroImage;
    public Image dogImage;

	[Header("Cooldown em segundos")]
	public float cooldown;
	private float cooldownDelta;

	public GameObject EfeitoDeCooldown;

	public BoolVariable buttonPressed;
    

	[HideInInspector]
    public PhotonView photonView;

    public Player2DAnimations anim;

    public AudioSource tiroSom;

    public static bool dirRight;

    public static bool dirLeft;

    public bool atirou;

    public static bool shootAnim;

    private void Awake()
    {
        photonView = gameObject.GetComponent<PhotonView>();
        anim = GetComponent<Player2DAnimations>();
       // SwipeDetector.OnSwipe += SwipeDirection;
		cooldownDelta = 0;
		EfeitoDeCooldown.SetActive(false);

        tiroImage = tiroButton.GetComponent<Image>();
        buttonPressed.Value = false;

    }


    void Update()
    {
        if (photonView.IsMine == true)
        {
            if ( atirou == true && cooldownDelta <= 0)
            {
                StartCoroutine("StartTiro");
                atirou = false;
                buttonPressed.Value = false;

            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                direction = new Vector2(0.5f, 0);
                photonView.RPC("Shoot", RpcTarget.All);
            }

			if (cooldown > 0)
				cooldownDelta -= Time.deltaTime;
        }

		if (buttonPressed.Value)
		{
            shootAnim = true;
            atirou = true;
			DogController.poderEstaAtivo = false;
			
			gameObject.GetComponent<PhotonView>().RPC("TransformaPet", RpcTarget.All, true);
			
		}
    }

    public void Atirou()
    {
		buttonPressed.Value = true;
    }

    [PunRPC]
    void Shoot()
    {
		if (!(bool)photonView.Owner.CustomProperties["dogValue"] || cooldownDelta > 0) return;
        Debug.Log("Shoot");
        GameObject bullet;
        bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);// as GameObject;
        bullet.GetComponent<ItemThrow>().InitializeBullet(photonView.Owner);
        cooldownDelta = cooldown;
		StartCoroutine("CooldownEffect");
		photonView.Owner.CustomProperties["atirou"] = true;
    }

    IEnumerator StartTiro()
    {
        //playerAC.SetTrigger("Atirou");
        yield return new WaitForSeconds(1f);
        photonView.RPC("Shoot", RpcTarget.All);
    }

	IEnumerator CooldownEffect()
	{
        var tempColor = tiroImage.color;
        tempColor.a = 0.1f;
        tiroImage.color = tempColor;
        dogImage.color = tempColor;

        //EfeitoDeCooldown.SetActive(true);
		yield return new WaitForSeconds(cooldownDelta);
        //EfeitoDeCooldown.SetActive(false);

        gameObject.GetComponent<PhotonView>().RPC("TransformaPet", RpcTarget.All, false);
        shootAnim = false;
        tempColor.a = 1f;
        tiroImage.color = tempColor;
        dogImage.color = tempColor;
    }

}
