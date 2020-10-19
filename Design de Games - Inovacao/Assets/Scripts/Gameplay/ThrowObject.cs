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
		public int poolSize;
		private GameObject[] bulletPool;

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

        #region Unity Function

        private void Awake()
        {
            textoAtivo = Resources.Load<BoolVariable>("TextoAtivo");

            photonView = gameObject.GetComponent<PhotonView>();
            //anim = GetComponent<Player2DAnimations>();
            dogController = GetComponent<DogController>();
            playerAnim = GetComponent<PlayerAnimController>();

            tiroImage = tiroButton.GetComponent<Image>();
            PoolInitialize();


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

        #endregion

        #region Public Functions

        //Sendo chamado pelo script ButtonA
        public void Atirou()
        {
            atirou = true;
        }

        #endregion

        #region Private Functions

        [PunRPC]
        void Shoot()
        {
            Debug.Log("Atirou");
            //if (GameManager.inRoom && !(bool)photonView.Owner.CustomProperties["dogValue"])return;
            GameObject bullet;
            int i = 0;
            while (i <= bulletPool.Length - 1 && bulletPool[i].activeSelf)
            {

                i++;
            }
            if (i > bulletPool.Length - 1)
            {
                bulletPool[i] = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            }
            bullet = bulletPool[i];// as GameObject;
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;
            bullet.SetActive(true);
            shootAnim = false;
            playerAnim.dogButtonAnim = shootAnim;

            bullet.GetComponent<ItemThrow>().InitializeBullet(photonView.Owner);
            StartCoroutine("CooldownEffect");
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

        private void PoolInitialize()
        {
            bulletPool = new GameObject[poolSize];
            for (int i = 0; i < poolSize - 1; i++)
            {
                bulletPool[i] = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                bulletPool[i].SetActive(false);
            }
        }

        #endregion
    }
}
