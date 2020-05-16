using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollisionsController : RaycastController
{

    public TriggerCollisionInfo collisions;
    public PlayerThings playerThings;
    private DogController dogController;
    private HandVolei handVolei;

    public bool isBallGame;

    public float hitLenght = 5f;

    //CAMera
    private GameObject cam;
    private Transform playerTransform;
    private Vector3 initialPos;


    public override void Start()
    {
        base.Start();
        cam = transform.parent.gameObject.transform.GetChild(0).gameObject;
        playerThings = GetComponent<PlayerThings>();
        dogController = GetComponent<DogController>();
    }


    public void MoveDirection(Vector2 dir)
    {
        UpdateRaycastOrigins();
        collisions.Reset();

        if (isBallGame)
        {
            RightCollisions();
            LeftCollisions();
            UpCollisions();
            DownCollisions();
        }

        else
        {

            if (dir.x > 0)
            {
                RightCollisions();
            }
            else if (dir.x < 0)
            {
                LeftCollisions();
            }
            else if (dir.y > 0)
            {
                UpCollisions();
            }
            else if(dir.y <0)
            {
                DownCollisions();
            }
        }
    }

    void RightCollisions()
    {
        float directionX = 1;
        float rayLenght;
        if (isBallGame && PlayerThings.rightDir && FutebolPlayer.kicked == true && HandVolei.cortou)
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
                if(hit.collider.tag == "AjusteCam")
                {
                    CorridaAjustaCamera ajustaCamera = hit.collider.GetComponent<CorridaAjustaCamera>();
                    //ajustaCamera.cam = cam;
                    ajustaCamera.initialPos = this.gameObject.transform.position;
                    ajustaCamera.playerTransform = this.gameObject.transform;
                }

                if (hit.collider.tag == "Boost")
                {
                    collisions.boostMoto = true;
                }

                if (hit.collider.tag == "Porta")
                {
                    collisions.isDoor = true;
                    PortaManager porta = hit.collider.GetComponent<PortaManager>();
                    porta.OpenDoor();
                    return;
                }

                if (hit.collider.tag == "PortaTutorial")
                {
                    Debug.Log("Colidiu");
                    collisions.isDoor = true;
                    PortaTutorial porta = hit.collider.GetComponent<PortaTutorial>();
                    porta.abriPorta = true;
                    return;
                }

                if (hit.collider.tag == "Coletavel")
                {
                    DestroyColetavel2D coletavel2D = hit.collider.GetComponent<DestroyColetavel2D>();
                    coletavel2D.PegouColetavel(true);
					playerThings.PV.RPC("SendOnlineCollisions", RpcTarget.Others);

					Scored();

                }
				if (hit.collider.tag == "Moedinha")
				{
					//Debug.Log("colidiu com a moeda");
					hit.collider.gameObject.SendMessage("Coleta");
				}

				if (hit.collider.tag == "Carrinho")
                {
                    dogController.Carro();
                }

                if(hit.collider.tag == "Pipa")
                {
                    dogController.Pipa();
                }

				if (hit.collider.tag == "DogTiro")
				{
					playerThings.StartCoroutine("LevouDogada");
				}

				if (hit.collider.tag == "Volei")
                {
                    if (PlayerThings.rightDir)
                    {
                        collisions.cortaBola = true;
                    }
                    if (PlayerThings.leftDir)
                    {
                        collisions.tocouBola = true;
                    }
                }

                if(hit.collider.tag == "Futebol")
                {
                    if (PlayerThings.rightDir)
                    {
                        collisions.chutouBola = true;
                    }
                    if (PlayerThings.leftDir)
                    {
                        collisions.cabecaBola = true;
                    }
                }
				if (hit.collider.tag == "LinhaDeChegada")
				{
					hit.collider.gameObject.GetComponent<LinhaDeChegada>().Colidiu(gameObject);
				}
            }
        }
    }

    void LeftCollisions()
    {
        float directionX = -1;
        float rayLenght;
        if (isBallGame && PlayerThings.leftDir && FutebolPlayer.kicked == true || HandVolei.cortou)
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
                if (hit.collider.tag == "Boost")
                {
                    collisions.boostMoto = true;
                }

                if (hit.collider.tag == "PortaTutorial")
                {
                    collisions.isDoor = true;
                    PortaTutorial porta = hit.collider.GetComponent<PortaTutorial>();
                    porta.abriPorta = true;
                    return;
                }
                if (hit.collider.tag == "Porta")
                {
                    collisions.isDoor = true;
                    hit.collider.GetComponent<PortaManager>().OpenDoor();
                    continue;
                }

                if (hit.collider.tag == "Coletavel")
                {
                    DestroyColetavel2D coletavel2D = hit.collider.GetComponent<DestroyColetavel2D>();
                    coletavel2D.PegouColetavel(true);
					playerThings.PV.RPC("SendOnlineCollisions", RpcTarget.Others);

					Scored();

                }
				if (hit.collider.tag == "Moedinha")
				{
					Debug.Log("colidiu com a moeda");
					hit.collider.gameObject.SendMessage("Coleta");
				}

				if (hit.collider.tag == "Carrinho")
                {
                    dogController.Carro();
                }

				if (hit.collider.tag == "Pipa")
				{
					dogController.Pipa();
				}

				if (hit.collider.tag == "DogTiro")
				{
					playerThings.StartCoroutine("LevouDogada");
				}

				if (hit.collider.tag == "Volei")
                {
                    if (PlayerThings.rightDir)
                    {
                        collisions.tocouBola = true;
                    }

                    if (PlayerThings.leftDir)
                    {
                        collisions.cortaBola = true;
                    }
                }

                if (hit.collider.tag == "Futebol")
                {
                    if (PlayerThings.rightDir)
                    {
                        collisions.cabecaBola = true;
                    }

                    if (PlayerThings.leftDir)
                    {
                        collisions.chutouBola = true;
                    }
                }
				if (hit.collider.tag == "LinhaDeChegada")
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
                if (hit.collider.tag == "Boost")
                {
                    collisions.boostMoto = true;
                }

                if (hit.collider.tag == "PortaTutorial")
                {
                    collisions.isDoor = true;
                    PortaTutorial porta = hit.collider.GetComponent<PortaTutorial>();
                    porta.abriPorta = true;
                    return;
                }

                if (hit.collider.tag == "Porta")
                {
                    collisions.isDoor = true;
                    hit.collider.GetComponent<PortaManager>().OpenDoor();
                    continue;
                }

                if (hit.collider.tag == "Coletavel")
                {
                    DestroyColetavel2D coletavel2D = hit.collider.GetComponent<DestroyColetavel2D>();
                    coletavel2D.PegouColetavel(true);
					playerThings.PV.RPC("SendOnlineCollisions", RpcTarget.Others);

					Scored();

                }
				if (hit.collider.tag == "Moedinha")
				{
					Debug.Log("colidiu com a moeda");
					hit.collider.gameObject.SendMessage("Coleta");
				}

				if (hit.collider.tag == "Carrinho")
                {
                    dogController.Carro();
                }

                if (hit.collider.tag == "Pipa")
                {
                    dogController.Pipa();
                }

                if (hit.collider.tag == "Volei")
                {
                    collisions.tocouBola = true;
                }

                if(hit.collider.tag == "Futebol")
                {
                    collisions.cabecaBola = true;
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

            Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.red);

            if (hit)
            {
                if(hit.collider.tag == "Boost")
                {
                    collisions.boostMoto = true;
                }

                if (hit.collider.tag == "PortaTutorial")
                {
                    collisions.isDoor = true;
                    PortaManager porta = hit.collider.GetComponent<PortaManager>();
                    porta.abriPorta = true;
                    return;
                }

                if (hit.collider.tag == "Porta")
                {
                    collisions.isDoor = true;
                    hit.collider.GetComponent<PortaManager>().OpenDoor();
                    continue;
                }

                if (hit.collider.tag == "Coletavel")
                {
                    DestroyColetavel2D coletavel2D = hit.collider.GetComponent<DestroyColetavel2D>();
                    coletavel2D.PegouColetavel(true);
					playerThings.PV.RPC("SendOnlineCollisions", RpcTarget.Others);

					Scored();

                }

				if (hit.collider.tag == "Moedinha")
				{
					Debug.Log("colidiu com a moeda");
					hit.collider.gameObject.SendMessage("Coleta");
				}

                if(hit.collider.tag == "Carrinho")
                {
                    dogController.Carro();
                }

                if (hit.collider.tag == "Pipa")
                {
                    dogController.Pipa();
                }

                if (hit.collider.tag == "Futebol")
                {
                    collisions.cabecaBola = true;
                }

                if(hit.collider.tag == "GolSelect")
                {
                    GolSelect select = hit.collider.GetComponent<GolSelect>();
                    select.jogador = playerThings.GetComponent<PlayerThings>();
                    hit.collider.GetComponent<BoxCollider2D>().enabled = false;
                }

                if(hit.collider.tag == "CaixaDagua")
                {
                    collisions.direction = new Vector2(0f, 1);
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

        public Vector2 direction;


        public void Reset()
        {
            isDoor = boostMoto = false;
            cortaBola = tocouBola = false;
            cabecaBola = tocouBola = false;
            caixaDagua = false;
            direction.x = direction.y = 0;
        }
    }
}
