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

    /*public AudioSource tokenSom;
    public AudioSource carroSom;
    public AudioSource pipaSom;
    public SimpleAudioEvent tokenAudioEvent;*/


    [HideInInspector]
    private PhotonView PV;

    [Header("Skills")]
    //public PipaEffect efeitoPipa;
    public CarroEffect efeitoCarro;
    public BoolVariable hitTotemCarro;
    public BoolVariable hitTotemPipa;
    public BoolVariable carroActive;
    public BoolVariable pipaActive;
	public BoolVariable buttonPressed;

    //public Animator playerAC;




    void Start()
    {
        PV = gameObject.GetComponent<PhotonView>();
        efeitoCarro.ativa.Value = false;
        //efeitoPipa.ativa.Value = false;
		PV.Controller.CustomProperties["dogValue"] = true;
		dogAtivo.Value = true;
    }


    void Update()
    {
		if (PV == null || PV.IsMine)
		{


			if (poderEstaAtivo == false)
			{
				efeitoCarro.effectTime = 0;

				gameObject.GetComponent<PhotonView>().RPC("DesativaPowerUps", RpcTarget.All);
			}


			if (hitTotemCarro.Value == true || hitTotemPipa.Value == true)
			{
				target = ItemThrow.totemTarget;
				float step = speedToTotem * Time.deltaTime;
				transform.position = Vector3.MoveTowards(transform.position, target.position, step);
			}

			PV.Controller.CustomProperties["dogValue"] = dogAtivo.Value;

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


    [PunRPC]
    public void Carro()
    {
        if (carroActive.Value == false && pipaActive.Value == false)
        {
            Debug.Log("Carro");

            hitTotemCarro.Value = false;
            carroActive.Value = true;

            dogAtivo.Value = false;
            carrinhoObj.SetActive(true);

            poderEstaAtivo = true;

            StartCoroutine(TempoParaDesativar(6f));
        }

    }



    [PunRPC]
    public void Pipa()
    {
        if (carroActive.Value == false && pipaActive.Value == false)
        {

            hitTotemPipa.Value = false;
            pipaActive.Value = true;

            dogAtivo.Value = false;
            pipaObj.SetActive(true);

            poderEstaAtivo = true;
            //tokenAudioEvent.Play(tokenSom);
            StartCoroutine(TempoParaDesativar(6f));
        }
    }

    [PunRPC]
    public void DesativaPowerUps()
    {


            pipaActive.Value = false;
            pipaObj.SetActive(false);


            carroActive.Value = false;
            carrinhoObj.SetActive(false);

    }

    [PunRPC]
    private void TransformaPet(bool isDog)
    {
        Pet.SetActive(isDog);
        if (!isDog)
        {
            Pet.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, Pet.transform.position.z);
            Debug.Log("Pet");
			buttonPressed.Value = false;

		}
    }

    private IEnumerator TempoParaDesativar(float waitTime)
    {
        Debug.Log("VaiDesativar");
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Desativou");
        poderEstaAtivo = false;
        dogAtivo.Value = true;

    }

	
}
