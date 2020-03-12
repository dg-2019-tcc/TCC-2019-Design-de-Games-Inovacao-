using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    public GameObject player;
    public GameObject playerModel;
    private Transform target;
    public float speedToTotem = 10f;

    private bool dogTransformed;

    [Header("Pet")]

    public GameObject Pet;
	public BoolVariable dogAtivo;
    public GameObject dogSpawn;
    public static bool poderEstaAtivo;
    public static bool desativaPoder;

    [Header("Pet pipa")]

    public GameObject pipaObj;

    [Header("Pet carrinho")]

    public GameObject carrinhoObj;


    [Header("Som")]

    public AudioSource tokenSom;
    public AudioSource carroSom;
    public AudioSource pipaSom;
    public SimpleAudioEvent tokenAudioEvent;


    [HideInInspector]
    private PhotonView PV;

    [Header("Skills")]
    public PipaEffect efeitoPipa;
    public CarroEffect efeitoCarro;
    public BoolVariable hitTotemCarro;
    public BoolVariable hitTotemPipa;
    public BoolVariable carroActive;
    public BoolVariable pipaActive;

    public Animator playerAC;




    void Start()
    {
        PV = gameObject.GetComponent<PhotonView>();
        efeitoCarro.ativa.Value = false;
        efeitoPipa.ativa.Value = false;
		// PV.Controller.CustomProperties["dogValue"] = true;
		dogAtivo.Value = true;
    }


    void Update()
    {
		if (PV == null || PV.IsMine)
		{


			if (PlayerMovement.acabou == true)
			{
				carrinhoObj.SetActive(false);
				pipaObj.SetActive(false);
				playerModel.SetActive(true);
			}


			if (poderEstaAtivo == false)
			{
				efeitoPipa.effectVar = 0;
				efeitoCarro.effectTime = 0;
				pipaSom.Stop();
				carroSom.Stop();
			//	dogAtivo.Value = true;
				gameObject.GetComponent<PhotonView>().RPC("DesativaPowerUps", RpcTarget.All);
			}


			if (hitTotemCarro.Value == true || hitTotemPipa.Value == true)
			{
				target = ItemThrow.totemTarget;
				float step = speedToTotem * Time.deltaTime;
				transform.position = Vector3.MoveTowards(transform.position, target.position, step);
			}

			//PV.Controller.CustomProperties["dogValue"] = dogAtivo.Value;

			if (!dogAtivo.Value)
			{
				gameObject.GetComponent<PhotonView>().RPC("TransformaPet", RpcTarget.All, false);
			}

			else
			{
				gameObject.GetComponent<PhotonView>().RPC("TransformaPet", RpcTarget.All, true);
			}

		}
		

		
	}



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!PV.IsMine && PV != null) return;
		if (!dogAtivo.Value) return;
        if (collision.CompareTag("Pipa"))
        {
            if (efeitoCarro.ativa.Value == false && efeitoPipa.ativa.Value == false)
            {
                efeitoPipa.effectVar = 8;
                StartCoroutine(efeitoPipa.Enumerator(this));
                gameObject.GetComponent<PhotonView>().RPC("Pipa", RpcTarget.All);
                hitTotemPipa.Value = false;
            }
        }

        if (collision.CompareTag("Carrinho"))
        {
            if (efeitoCarro.ativa.Value == false && efeitoPipa.ativa.Value == false)
            {
                efeitoCarro.effectTime = 6;
                StartCoroutine(efeitoCarro.Enumerator(this));
                gameObject.GetComponent<PhotonView>().RPC("Carro", RpcTarget.All);
                hitTotemCarro.Value = false;
            }
        }
    }

    [PunRPC]
    public void Carro()
    {
        carroActive.Value = true;
        carroSom.Play();
        dogAtivo.Value = false;
        carrinhoObj.SetActive(true);
        //playerModel.SetActive(false);
        poderEstaAtivo = true;
        tokenAudioEvent.Play(tokenSom);
        StartCoroutine(TempoParaDesativar(6f));
    }



    [PunRPC]
    public void Pipa()
    {
        pipaActive.Value = true;
        pipaSom.Play();
        dogAtivo.Value = false;
        pipaObj.SetActive(true);
        //playerModel.SetActive(false);
        poderEstaAtivo = true;
        tokenAudioEvent.Play(tokenSom);
        StartCoroutine(TempoParaDesativar(6f));
    }

    [PunRPC]
    public void DesativaPowerUps()
    {


        if (efeitoPipa.ativa.Value == false || efeitoPipa.ativa.Value == true)
        {
            pipaActive.Value = false;
            pipaObj.SetActive(false);
            playerModel.SetActive(true);
        }
        if (efeitoCarro.ativa.Value == false || efeitoCarro.ativa.Value == true)
        {
            carroActive.Value = false;
            carrinhoObj.SetActive(false);
            playerModel.SetActive(true);
        }
    }

    [PunRPC]
    private void TransformaPet(bool isDog)
    {
        Pet.SetActive(isDog);
        if (!isDog)
        {
            Pet.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, Pet.transform.position.z);
        }
    }

    private IEnumerator TempoParaDesativar(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        poderEstaAtivo = false;
        dogAtivo.Value = true;
    }

	
}
