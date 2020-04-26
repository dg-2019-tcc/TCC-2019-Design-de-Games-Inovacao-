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



    public Button tiroButton;
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

    public bool atirando;

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
            if ( atirou == true && cooldownDelta == 0 && atirando == false)
            {
                atirando = true;
                cooldownDelta += 1;
                StartCoroutine("StartTiro");
                atirou = false;

            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                direction = new Vector2(0.5f, 0);
                photonView.RPC("Shoot", RpcTarget.All);
            }

        }

		if (buttonPressed.Value)
		{
            shootAnim = true;
            atirou = true;
			DogController.poderEstaAtivo = false;

            gameObject.GetComponent<PhotonView>().RPC("TransformaPet", RpcTarget.All, true);
        }

        if (atirando)
        {
            buttonPressed.Value = false;

            var tempColor = tiroImage.color;
            tempColor.a = 0.1f;
            tiroImage.color = tempColor;
            dogImage.color = tempColor;

            tiroButton.enabled = false;

        }

        else
        {
            cooldownDelta = 0;
            var tempColor = tiroImage.color;
            tempColor.a = 1f;
            tiroImage.color = tempColor;
            dogImage.color = tempColor;

            tiroButton.enabled = true;
        }
    }

    public void Atirou()
    {
        if (atirando == false)
        {
            buttonPressed.Value = true;
        }
    }

    [PunRPC]
    void Shoot()
    {
		if (!(bool)photonView.Owner.CustomProperties["dogValue"]) return;
        GameObject bullet;
        bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);// as GameObject;
        bullet.GetComponent<ItemThrow>().InitializeBullet(photonView.Owner);
		StartCoroutine("CooldownEffect");
		photonView.Owner.CustomProperties["atirou"] = true;
    }

    IEnumerator StartTiro()
    {
        
        yield return new WaitForSeconds(1f);

        photonView.RPC("Shoot", RpcTarget.All);
    }

	IEnumerator CooldownEffect()
	{
        shootAnim = false;
        atirando = false;
        yield return new WaitForSeconds(cooldown);

        gameObject.GetComponent<PhotonView>().RPC("TransformaPet", RpcTarget.All, false);
    }

}
