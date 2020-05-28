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

	public BoolVariable dogBotao;
    public BoolVariable desativaPower;
    public BoolVariable carroActive;
    public BoolVariable pipaActive;
    public BoolVariable dogAtivo;
    

	[HideInInspector]
    public PhotonView photonView;

    public Player2DAnimations anim;

    public AudioSource tiroSom;

    public static bool dirRight;

    public static bool dirLeft;

    public bool atirou;

    public bool atirando;

    public bool shootAnim;

    public BoolVariable textoAtivo;
    public bool passouTexto;

    private void Awake()
    {
        textoAtivo = Resources.Load<BoolVariable>("TextoAtivo");

        photonView = gameObject.GetComponent<PhotonView>();
        anim = GetComponent<Player2DAnimations>();

        tiroImage = tiroButton.GetComponent<Image>();
        dogBotao.Value = false;

    }


    void Update()
    {
        if (photonView.IsMine == true || !PhotonNetwork.InRoom)
        {
            if (atirou == true && cooldownDelta == 0 && atirando == false && carroActive.Value == false && pipaActive.Value == false)
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

        if (dogBotao.Value == true)
        {
			
            if (carroActive.Value == true || pipaActive.Value == true)
            {
                desativaPower.Value = true;
            }
            else
            {
                atirou = true;
            }
			
        }
        else
        {
            atirou = false;
        }

        if (carroActive.Value == true || pipaActive.Value == true)
        {
            atirando = false;
        }

        if (atirando)
		{
			
			tiroButton.enabled = false;
		}

        else
        {
			Debug.Log("voltou do tiro");
			dogBotao.Value = false;
			cooldownDelta = 0;
            tiroButton.enabled = true;
        }
    }

    public void Atirou()
    {
        if (textoAtivo.Value == false)
        {
            if (atirando == false || Time.timeScale == 0)
            {
                dogBotao.Value = true;
            }
        }

        else
        {
            passouTexto = true;
        }
    }

    public void StopAtirou()
    {
        if (textoAtivo.Value == false)
        {
            dogBotao.Value = false;
        }

        else
        {
            passouTexto = false;
        }
    }

    [PunRPC]
    void Shoot()
    {
		if (PhotonNetwork.IsConnected && !(bool)photonView.Owner.CustomProperties["dogValue"]) return;
        GameObject bullet;
        bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);// as GameObject;
        shootAnim = false;
        anim.DogButtonAnim(shootAnim);

        bullet.GetComponent<ItemThrow>().InitializeBullet(photonView.Owner);
		StartCoroutine("CooldownEffect");
		photonView.Owner.CustomProperties["atirou"] = true;
    }

    IEnumerator StartTiro()
    {
        shootAnim = true;
        anim.DogButtonAnim(shootAnim);
        atirou = false;
        yield return new WaitForSeconds(1f);
        shootAnim = false;
        anim.DogButtonAnim(shootAnim);
		if (PhotonNetwork.IsConnected)
		{
			photonView.RPC("Shoot", RpcTarget.All);
		}
		else
		{
			Shoot();
		}
    }

    void ShootOffline()
    {

    }

	IEnumerator CooldownEffect()
	{
        dogAtivo.Value = false;
        yield return new WaitForSeconds(cooldown);
        dogAtivo.Value = true;
        //gameObject.GetComponent<PhotonView>().RPC("TransformaPet", RpcTarget.All, false);
        atirando = false;
    }

}
