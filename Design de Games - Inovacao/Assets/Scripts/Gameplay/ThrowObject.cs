using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

namespace Complete {
    public class ThrowObject : MonoBehaviour
    {
        [Header("Alvo e Prefab")]
        public Transform firePoint;
        public GameObject bulletPrefab;

        public static Vector2 direction;

        public bool shouldShoot;

        public Button tiroButton;
        private Image tiroImage;
        public Image dogImage;

        [Header("Cooldown em segundos")]
        public float cooldown;
        private float cooldownDelta;

        public GameObject EfeitoDeCooldown;

        public BoolVariable desativaPower;
        public BoolVariable carroActive;
        public BoolVariable pipaActive;
        public BoolVariable dogAtivo;


        [HideInInspector]
        public PhotonView photonView;

        //public Player2DAnimations anim;
        public PlayerAnimController playerAnim;

        public AudioSource tiroSom;

        public static bool dirRight;

        public static bool dirLeft;

        public bool atirou;

        public bool atirando;

        public bool shootAnim;

        public BoolVariable textoAtivo;
        public bool passouTexto;

        public DogMovement dogMove;
        private DogController dogController;
        private bool isOnline;

        private void Awake()
        {
            textoAtivo = Resources.Load<BoolVariable>("TextoAtivo");

            photonView = gameObject.GetComponent<PhotonView>();
            //anim = GetComponent<Player2DAnimations>();
            dogController = GetComponent<DogController>();
            playerAnim = GetComponent<PlayerAnimController>();

            tiroImage = tiroButton.GetComponent<Image>();

            if (PhotonNetwork.InRoom)
            {
                isOnline = true;
            }
            else
            {
                isOnline = false;
            }
        }


        void Update()
        {
            if (photonView.IsMine == true || !PhotonNetwork.InRoom)
            {
                if (atirou == true)
                {
                    atirando = true;
                    StartCoroutine("StartTiro");
                    atirou = false;

                }
            }


            if (atirando)
            {
                tiroButton.enabled = false;
            }

            else
            {
                tiroButton.enabled = true;
            }
        }

        //Sendo chamado pelo script ButtonA
        public void Atirou()
        {
            atirou = true;
        }


        [PunRPC]
        void Shoot()
        {
            Debug.Log("Atirou");
            //if (GameManager.inRoom && !(bool)photonView.Owner.CustomProperties["dogValue"])return;
            GameObject bullet;
            bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);// as GameObject;
            shootAnim = false;
            //anim.DogButtonAnim(shootAnim);
            //anim.dogButtonAnim = shootAnim;
            playerAnim.dogButtonAnim = shootAnim;

            bullet.GetComponent<ItemThrow>().InitializeBullet(photonView.Owner);
            StartCoroutine("CooldownEffect");
            //photonView.Owner.CustomProperties["atirou"] = true;
        }

        IEnumerator StartTiro()
        {
            shootAnim = true;
            //anim.dogButtonAnim = shootAnim;
            playerAnim.dogButtonAnim = shootAnim;
            atirou = false;
            yield return new WaitForSeconds(0.5f);
            shootAnim = false;
            //anim.dogButtonAnim = shootAnim;
            playerAnim.dogButtonAnim = shootAnim;
            if (isOnline)
            {
                Debug.Log("Atirou");
                photonView.RPC("Shoot", RpcTarget.All);
            }
            else
            {
                Debug.Log("Atirou");
                Shoot();
            }
        }



        IEnumerator CooldownEffect()
        {
            if (GameManager.inRoom)
            {
                if (photonView.Owner.IsLocal)
                {
                    //dogAtivo.Value = false;
                    dogController.ChangeState("TiroState");
                }
            }
            else
            {
                dogController.ChangeState("TiroState");
            }
            yield return new WaitForSeconds(cooldown);

            if (GameManager.inRoom)
            {
                if (photonView.Owner.IsLocal)
                {
                    {
                        //dogAtivo.Value = true;
                        dogController.ChangeState("IdleState");
                    }
                }
            }
            else
            {
                dogController.ChangeState("IdleState");
            }
            //gameObject.GetComponent<PhotonView>().RPC("TransformaPet", RpcTarget.All, false);
            atirando = false;
        }
    }
}
