using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

namespace Complete
{
    public class ItemThrow : MonoBehaviour
    {

        public Photon.Realtime.Player Owner { get; private set; }

        public FloatVariable bulletSpeed;
        public Rigidbody2D rb;

        private Vector2 shootDirection;

        public float timeDestroy;

        public float speedPlayer = 1.0f;

        public static Transform totemTarget;

        public bool hit;

        public BoolVariable hitTotemCarro;
        public BoolVariable hitTotemPipa;
        public BoolVariable dog;


        private bool isLocal;



		private Vector3 startingPos;

        #region Unity Function

        private void OnEnable()
        {
            timeDestroy = 0;
        }
        private void Update()
        {
            rb.velocity = transform.right * bulletSpeed.Value * Time.deltaTime;
            rb.position += rb.velocity;

            timeDestroy += Time.deltaTime;
            if (timeDestroy >= 3f)
            {
                //if (PhotonNetwork.IsConnected)
                if (GameManager.inRoom)
                {
                    Owner.CustomProperties["dogValue"] = true;
                    Owner.CustomProperties["atirou"] = false;
                }
                transform.position = startingPos;
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("AI"))
            {
                AIMovement aiMove = collision.GetComponent<AIMovement>();
                aiMove.StartCoroutine("LevouDogada");
            }

            if (GameManager.inRoom)
            {
                if (collision.CompareTag("Pipa"))
                {
                    if (isLocal)
                    {
                        totemTarget = collision.transform;
                        hitTotemPipa.Value = true;
                        Owner.CustomProperties["dogPipa"] = true;
                        //tokenSom.Play();

                        Owner.CustomProperties["acertouTotem"] = true;
                    }
                    timeDestroy = 4.9f;
                }

                if (collision.CompareTag("Carrinho"))
                {
                    if (isLocal)
                    {
                        totemTarget = collision.transform;
                        hitTotemCarro.Value = true;
                        Owner.CustomProperties["dogCarro"] = true;
                        //tokenSom.Play();

                        Owner.CustomProperties["acertouTotem"] = true;
                    }
                    timeDestroy = 4.9f;

                }
            }

            else
            {
                if (collision.CompareTag("Pipa"))
                {
                    totemTarget = collision.transform;
                    hitTotemPipa.Value = true;
                    timeDestroy = 4.9f;
                }

                if (collision.CompareTag("Carrinho"))
                {
                    totemTarget = collision.transform;
                    hitTotemCarro.Value = true;
                    timeDestroy = 4.9f;

                }
            }

            if (collision.CompareTag("Player") && collision.GetComponent<PhotonView>().Owner != Owner)
            {
                PlayerThings jogador = collision.GetComponent<PlayerThings>();
                jogador.StartCoroutine("LevouDogada");
                //collision.GetComponent<PhotonView>().RPC("LevouDogada", RpcTarget.All); 

                timeDestroy = 4.9f;
            }

            if (collision.CompareTag("Coletavel"))
            {
                if (isLocal)
                {
                    DestroyColetavel2D coletavel = collision.GetComponent<DestroyColetavel2D>();
                    coletavel.PegouColetavel(true);
                    Owner.AddScore(1);
                }
            }
        }

        #endregion

        #region Public Functions

        public void InitializeBullet(Photon.Realtime.Player owner)
        {
            if (GameManager.inRoom)
            {
                Owner = owner;
                isLocal = owner.IsLocal;

                Owner.CustomProperties["atirou"] = true;
                Owner.CustomProperties["dogValue"] = false;
            }

            rb.position += rb.velocity;

            startingPos = transform.position;
        }

        #endregion

        #region Private Functions

        #endregion

    }
}