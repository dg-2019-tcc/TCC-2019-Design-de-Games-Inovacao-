using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

namespace Complete
{
    public class TriggerCollisionsController : RaycastController
    {

        public TriggerCollisionInfo collisions;
        public PlayerThings playerThings;
        private DogController dogController;
        private HandVolei handVolei;
        private FutebolPlayer futebolPlayer;


        public bool isBallGame;
        public bool isVolei;
        public bool isFut;


        public float hitLenght = 3f;

        //CAMera
        private GameObject cam;
        private Transform playerTransform;
        private Vector3 initialPos;

        //Verifica Sons
        public bool isCaixaDaguaSound = true;
        public bool isDogNormal = true;
        public bool isBallKicked;
        public bool isShotRecived;

        MotoChangeSpeed motoSpd;

        public bool rightRay;
        public bool leftRay;

        public BoolVariable hitCarro;
        public BoolVariable hitPipa;

        public override void Start()
        {
            base.Start();
            cam = transform.parent.gameObject.transform.GetChild(0).gameObject;
            playerThings = GetComponent<PlayerThings>();
            dogController = GetComponent<DogController>();

            hitCarro = Resources.Load<BoolVariable>("HitCarro");
            hitPipa = Resources.Load<BoolVariable>("HitPipa");

            isCaixaDaguaSound = true;
            isBallKicked = true;
            isShotRecived = true;

            motoSpd = GetComponent<MotoChangeSpeed>();

            if (isVolei)
            {
                handVolei = GetComponent<HandVolei>();
            }

            if (isFut)
            {
                futebolPlayer = GetComponent<FutebolPlayer>();
            }

        }






        public void MoveDirection(Vector2 dir)
        {
            UpdateRaycastOrigins();
            collisions.Reset();

            RightCollisions();
            LeftCollisions();
            UpCollisions();
            DownCollisions();
        }


        void RightCollisions()
        {
            float directionX = 1;
            float rayLenght;


            //if (isBallGame && PlayerThings.rightDir &&futebolPlayer.kicked == true || handVolei.cortou)
            if (rightRay)
            {

                rayLenght = hitLenght + skinWidth;


            }
            else
            {
                rayLenght = 0.1f + skinWidth;
            }

            for (int i = 0; i < horizontalRayCount; i++)
            {
                Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
                rayOrigin += Vector2.up * (horizontalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLenght, collisionMask);

                //Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.red);

                if (hit)
                {
                    if (hit.collider.CompareTag("TV"))
                    {
                        hit.collider.GetComponent<TextBoxActivate>().PlayerHit();
                        collisions.hitTV = true;
                    }

                    if (hit.collider.CompareTag("Dog"))
                    {
                        dogController.sequestrado = false;
                        dogController.ChangeState("IdleState");
                        //hit.collider.gameObject.SetActive(false);
                        DogColetaTutorial dogTutorial = hit.collider.GetComponent<DogColetaTutorial>();
                        dogTutorial.AtivaDog();
                    }

                    if (hit.collider.CompareTag("Pet"))
                    {
                        if (hit.distance == 0)
                        {
                            collisions.hitDog = true;
                        }
                    }
                    if (hit.collider.CompareTag("AjusteCam"))
                    {
                        CorridaAjustaCamera ajustaCamera = hit.collider.GetComponent<CorridaAjustaCamera>();
                        //ajustaCamera.cam = cam;
                        ajustaCamera.initialPos = this.gameObject.transform.position;
                        ajustaCamera.playerTransform = this.gameObject.transform;
                    }

                    if (hit.collider.CompareTag("Boost"))
                    {
                        collisions.boostMoto = true;
                    }

                    if (hit.collider.CompareTag("Porta"))
                    {
                        collisions.isDoor = true;
                        PortaManager porta = hit.collider.GetComponent<PortaManager>();
                        porta.OpenDoor();
                        return;
                    }

                    if (hit.collider.CompareTag("PortaTutorial"))
                    {
                        collisions.isDoor = true;
                        PortaTutorial porta = hit.collider.GetComponent<PortaTutorial>();
                        porta.abriPorta = true;
                        return;
                    }
                    if (hit.collider.CompareTag("TextBox"))
                    {
                        hit.collider.GetComponent<TextBoxActivate>().PlayerHit();
                    }

                    if (hit.collider.CompareTag("Coletavel"))
                    {
                        DestroyColetavel2D coletavel2D = hit.collider.GetComponent<DestroyColetavel2D>();
                        coletavel2D.PegouColetavel(true);
                        playerThings.PV.RPC("SendOnlineCollisions", RpcTarget.Others);
                        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Feedback/Coletaveis/ColetaDoOsso");

                        Scored();

                    }
                    if (hit.collider.CompareTag("Moedinha"))
                    {
                        //hit.collider.gameObject.SendMessage("Coleta");
                        CoinObject coin = hit.collider.GetComponent<CoinObject>();
                        coin.PegouMoeda();
                    }

                    if (hit.collider.CompareTag("Carrinho"))
                    {
                        //dogController.Carro();
                        dogController.ChangeState("CarroState");
                        hitCarro.Value = false;
                        if (isDogNormal == true)
                        {
                            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Feedback/Coletaveis/TransformaDog");
                            isDogNormal = false;
                        }
                    }

                    if (hit.collider.CompareTag("Pipa"))
                    {
                        //dogController.Pipa();
                        dogController.ChangeState("PipaState");
                        hitPipa.Value = false;
                        if (isDogNormal == true)
                        {
                            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Feedback/Coletaveis/TransformaDog");
                            isDogNormal = false;
                        }
                    }

                    if (hit.collider.CompareTag("DogTiro"))
                    {
                        playerThings.StartCoroutine("LevouDogada");
                        if (isShotRecived)
                        {
                            StartCoroutine(TocaSomTiroDoDog());
                        }
                    }

                    if (hit.collider.CompareTag("Volei"))
                    {
                        if (PlayerThings.rightDir)
                        {
                            collisions.cortaBola = true;
                            if (isBallKicked)
                            {
                                StartCoroutine(TocaSomChutaBola());
                            }
                        }
                        if (PlayerThings.leftDir)
                        {
                            collisions.tocouBola = true;
                            if (isBallKicked)
                            {
                                StartCoroutine(TocaSomChutaBola());
                            }
                        }
                    }

                    if (hit.collider.CompareTag("Futebol"))
                    {
                        if (PlayerThings.rightDir)
                        {
                            collisions.chutouBola = true;
                            if (isBallKicked)
                            {
                                StartCoroutine(TocaSomChutaBola());
                            }
                        }
                        if (PlayerThings.leftDir)
                        {
                            if (hit.distance == 0)
                            {
                                collisions.tocouBola = true;
                                if (isBallKicked)
                                {
                                    StartCoroutine(TocaSomChutaBola());
                                }
                            }
                        }
                    }
                    if (hit.collider.CompareTag("LinhaDeChegada"))
                    {
                        if (hit.distance == 0)
                        {
                            hit.collider.gameObject.GetComponent<LinhaDeChegada>().Colidiu(gameObject);
                            if (motoSpd != null)
                            {
                                motoSpd.CarEngine.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                            }
                        }
                    }
                }
            }
        }

        void LeftCollisions()
        {
            float directionX = -1;
            float rayLenght;
            //if (isBallGame && PlayerThings.leftDir&& futebolPlayer.kicked == true || handVolei.cortou)
            if (leftRay)
            {

                rayLenght = hitLenght + skinWidth;

            }
            else
            {
                rayLenght = 0.1f + skinWidth;
            }

            for (int i = 0; i < horizontalRayCount; i++)
            {
                Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
                rayOrigin += Vector2.up * (horizontalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLenght, collisionMask);

                //Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.red);

                if (hit)
                {
                    if (hit.collider.CompareTag("TV"))
                    {
                        hit.collider.GetComponent<TextBoxActivate>().PlayerHit();
                        collisions.hitTV = true;
                    }

                    if (hit.collider.CompareTag("Dog"))
                    {
                        DogColetaTutorial dogTutorial = hit.collider.GetComponent<DogColetaTutorial>();
                        dogTutorial.AtivaDog();
                    }
                    if (hit.collider.CompareTag("Pet"))
                    {
                        if (hit.distance == 0)
                        {
                            collisions.hitDog = true;
                        }
                    }
                    if (hit.collider.CompareTag("Boost"))
                    {
                        collisions.boostMoto = true;
                    }

                    if (hit.collider.CompareTag("PortaTutorial"))
                    {
                        collisions.isDoor = true;
                        PortaTutorial porta = hit.collider.GetComponent<PortaTutorial>();
                        porta.abriPorta = true;
                        return;
                    }
                    if (hit.collider.CompareTag("TextBox"))
                    {
                        hit.collider.GetComponent<TextBoxActivate>().PlayerHit();
                    }
                    if (hit.collider.CompareTag("Porta"))
                    {
                        collisions.isDoor = true;
                        hit.collider.GetComponent<PortaManager>().OpenDoor();
                        continue;
                    }

                    if (hit.collider.CompareTag("Coletavel"))
                    {
                        DestroyColetavel2D coletavel2D = hit.collider.GetComponent<DestroyColetavel2D>();
                        coletavel2D.PegouColetavel(true);
                        playerThings.PV.RPC("SendOnlineCollisions", RpcTarget.Others);
                        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Feedback/Coletaveis/ColetaDoOsso");

                        Scored();

                    }
                    if (hit.collider.CompareTag("Moedinha"))
                    {
                        //hit.collider.gameObject.SendMessage("Coleta");
                        CoinObject coin = hit.collider.GetComponent<CoinObject>();
                        coin.PegouMoeda();
                    }

                    if (hit.collider.CompareTag("Carrinho"))
                    {
                        //dogController.Carro();
                        dogController.ChangeState("CarroState");
                        hitCarro.Value = false;
                        if (isDogNormal == true)
                        {
                            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Feedback/Coletaveis/TransformaDog");
                            isDogNormal = false;
                        }
                    }

                    if (hit.collider.CompareTag("Pipa"))
                    {
                        //dogController.Pipa();
                        dogController.ChangeState("PipaState");
                        hitPipa.Value = false;
                        if (isDogNormal == true)
                        {
                            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Feedback/Coletaveis/TransformaDog");
                            isDogNormal = false;
                        }
                    }

                    if (hit.collider.CompareTag("DogTiro"))
                    {
                        playerThings.StartCoroutine("LevouDogada");
                        if (isShotRecived)
                        {
                            StartCoroutine(TocaSomTiroDoDog());
                        }
                    }

                    if (hit.collider.CompareTag("Volei"))
                    {
                        if (PlayerThings.rightDir)
                        {
                            collisions.tocouBola = true;
                            if (isBallKicked)
                            {
                                StartCoroutine(TocaSomChutaBola());
                            }
                        }

                        if (PlayerThings.leftDir)
                        {
                            collisions.cortaBola = true;
                            if (isBallKicked)
                            {
                                StartCoroutine(TocaSomChutaBola());
                            }
                        }
                    }

                    if (hit.collider.CompareTag("Futebol"))
                    {
                        if (PlayerThings.rightDir)
                        {
                            if (hit.distance == 0)
                            {
                                collisions.tocouBola = true;
                                if (isBallKicked)
                                {
                                    StartCoroutine(TocaSomChutaBola());
                                }
                            }
                        }

                        if (PlayerThings.leftDir)
                        {
                            collisions.chutouBola = true;
                            if (isBallKicked)
                            {
                                StartCoroutine(TocaSomChutaBola());
                            }
                        }
                    }
                    if (hit.collider.CompareTag("LinhaDeChegada"))
                    {
                        hit.collider.gameObject.GetComponent<LinhaDeChegada>().Colidiu(gameObject);
                    }
                }
            }
        }

        void UpCollisions()
        {
            float directionY = 1;
            float rayLenght = 0.1f + skinWidth;

            for (int i = 0; i < verticalRayCount; i++)
            {
                Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
                rayOrigin += Vector2.right * (verticalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLenght, collisionMask);

                //Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.red);

                if (hit)
                {
                    if (hit.collider.CompareTag("TV"))
                    {
                        hit.collider.GetComponent<TextBoxActivate>().PlayerHit();
                        collisions.hitTV = true;
                    }

                    if (hit.collider.CompareTag("Dog"))
                    {
                        DogColetaTutorial dogTutorial = hit.collider.GetComponent<DogColetaTutorial>();
                        dogTutorial.AtivaDog();
                    }
                    if (hit.collider.CompareTag("Boost"))
                    {
                        collisions.boostMoto = true;
                    }

                    if (hit.collider.CompareTag("PortaTutorial"))
                    {
                        collisions.isDoor = true;
                        PortaTutorial porta = hit.collider.GetComponent<PortaTutorial>();
                        porta.abriPorta = true;
                        return;
                    }
                    if (hit.collider.CompareTag("TextBox"))
                    {
                        hit.collider.GetComponent<TextBoxActivate>().PlayerHit();
                    }

                    if (hit.collider.CompareTag("Porta"))
                    {
                        collisions.isDoor = true;
                        hit.collider.GetComponent<PortaManager>().OpenDoor();
                        continue;
                    }

                    if (hit.collider.CompareTag("Coletavel"))
                    {
                        DestroyColetavel2D coletavel2D = hit.collider.GetComponent<DestroyColetavel2D>();
                        coletavel2D.PegouColetavel(true);
                        playerThings.PV.RPC("SendOnlineCollisions", RpcTarget.Others);
                        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Feedback/Coletaveis/ColetaDoOsso");

                        Scored();

                    }
                    if (hit.collider.CompareTag("Moedinha"))
                    {
                        //hit.collider.gameObject.SendMessage("Coleta");
                        CoinObject coin = hit.collider.GetComponent<CoinObject>();
                        coin.PegouMoeda();
                    }

                    if (hit.collider.CompareTag("Carrinho"))
                    {
                        //dogController.Carro();
                        dogController.ChangeState("CarroState");
                        hitCarro.Value = false;
                        if (isDogNormal == true)
                        {
                            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Feedback/Coletaveis/TransformaDog");
                            isDogNormal = false;
                        }
                    }

                    if (hit.collider.CompareTag("Pipa"))
                    {
                        //dogController.Pipa();
                        dogController.ChangeState("PipaState");
                        hitPipa.Value = false;
                        if (isDogNormal == true)
                        {
                            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Feedback/Coletaveis/TransformaDog");
                            isDogNormal = false;
                        }
                    }

                    if (hit.collider.CompareTag("Volei"))
                    {
                        collisions.cabecaBola = true;
                        if (isBallKicked)
                        {
                            StartCoroutine(TocaSomChutaBola());
                        }
                    }

                    if (hit.collider.CompareTag("Futebol"))
                    {
                        collisions.cabecaBola = true;
                        if (isBallKicked)
                        {
                            StartCoroutine(TocaSomChutaBola());
                        }
                    }
                }
            }
        }

        void DownCollisions()
        {
            float directionY = -1;
            float rayLenght = 0.1f + skinWidth;

            for (int i = 0; i < verticalRayCount; i++)
            {
                Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
                rayOrigin += Vector2.right * (verticalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLenght, collisionMask);

                //Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.red);

                if (hit)
                {
                    if (hit.collider.CompareTag("SlowFall"))
                    {
                        collisions.slowTime = true;
                    }

                    if (hit.collider.CompareTag("TV"))
                    {
                        hit.collider.GetComponent<TextBoxActivate>().PlayerHit();
                        collisions.hitTV = true;
                    }

                    if (hit.collider.CompareTag("Dog"))
                    {
                        DogColetaTutorial dogTutorial = hit.collider.GetComponent<DogColetaTutorial>();
                        dogTutorial.AtivaDog();
                    }
                    if (hit.collider.tag == "Boost")
                    {
                        collisions.boostMoto = true;
                    }

                    if (hit.collider.CompareTag("PortaTutorial"))
                    {
                        collisions.isDoor = true;
                        PortaTutorial porta = hit.collider.GetComponent<PortaTutorial>();
                        porta.abriPorta = true;
                        return;
                    }
                    if (hit.collider.CompareTag("TextBox"))
                    {
                        hit.collider.GetComponent<TextBoxActivate>().PlayerHit();
                    }

                    if (hit.collider.CompareTag("Porta"))
                    {
                        collisions.isDoor = true;
                        hit.collider.GetComponent<PortaManager>().OpenDoor();
                        continue;
                    }

                    if (hit.collider.CompareTag("Coletavel"))
                    {
                        DestroyColetavel2D coletavel2D = hit.collider.GetComponent<DestroyColetavel2D>();
                        coletavel2D.PegouColetavel(true);
                        playerThings.PV.RPC("SendOnlineCollisions", RpcTarget.Others);
                        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Feedback/Coletaveis/ColetaDoOsso");

                        Scored();

                    }

                    if (hit.collider.CompareTag("Moedinha"))
                    {
                        //hit.collider.gameObject.SendMessage("Coleta");
                        //Destroy(hit.collider.gameObject);
                        CoinObject coin = hit.collider.GetComponent<CoinObject>();
                        coin.PegouMoeda();
                    }

                    if (hit.collider.CompareTag("Carrinho"))
                    {
                        //dogController.Carro();
                        dogController.ChangeState("CarroState");
                        hitCarro.Value = false;
                        if (isDogNormal == true)
                        {
                            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Feedback/Coletaveis/TransformaDog");
                            isDogNormal = false;
                        }
                    }

                    if (hit.collider.CompareTag("Pipa"))
                    {
                        //dogController.Pipa();
                        dogController.ChangeState("PipaState");
                        hitPipa.Value = false;
                        if (isDogNormal == true)
                        {
                            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Feedback/Coletaveis/TransformaDog");
                            isDogNormal = false;
                        }
                    }

                    if (hit.collider.CompareTag("Futebol"))
                    {
                        collisions.cabecaBola = true;
                    }

                    if (hit.collider.CompareTag("GolSelect"))
                    {
                        GolSelect select = hit.collider.GetComponent<GolSelect>();
                        select.jogador = playerThings.GetComponent<PlayerThings>();
                        //hit.collider.GetComponent<BoxCollider2D>().enabled = false;
                        select.photonView.RPC("DesabilitaColider", RpcTarget.All);
                    }

                    if (hit.collider.CompareTag("CaixaDagua"))
                    {
                        if (isCaixaDaguaSound == true)
                        {
                            StartCoroutine(TocaSomCaixaDeAgua());
                        }
                        collisions.caixaDagua = true;
                    }
                }
            }
        }

        [PunRPC]
        public void SendOnlineCollisions()
        {
            DestroyColetavel2D coletavel2D = FindObjectOfType<DestroyColetavel2D>();
            coletavel2D.PegouColetavel(false);
        }

        public void Scored()
        {
            playerThings.PV.Owner.AddScore(1);

        }

        public struct TriggerCollisionInfo
        {
            public bool isDoor, boostMoto;

            public bool cortaBola, tocouBola;

            public bool cabecaBola, chutouBola;

            public bool caixaDagua;

            public bool hitDog;

            public bool hitTV;

            public bool slowTime;

            public Vector2 direction;


            //Booleans para fazer verificação de som 


            public void Reset()
            {
                isDoor = boostMoto = false;
                cortaBola = tocouBola = false;
                cabecaBola = tocouBola = false;
                caixaDagua = false;
                hitDog = false;
                slowTime = false;
                hitTV = false;
                direction.x = direction.y = 0;
            }
        }

        IEnumerator TocaSomCaixaDeAgua()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Objetos/CaixaDeAgua");
            isCaixaDaguaSound = false;
            yield return new WaitForSeconds(0.5f);
            isCaixaDaguaSound = true;
        }

        IEnumerator TocaSomTiroDoDog()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player/Impacto");
            isShotRecived = false;
            yield return new WaitForSeconds(0.5f);
            isShotRecived = true;
        }
        IEnumerator TocaSomChutaBola()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Objetos/Bola");
            isBallKicked = false;
            yield return new WaitForSeconds(0.5f);
            isBallKicked = true;
        }
    }
}
