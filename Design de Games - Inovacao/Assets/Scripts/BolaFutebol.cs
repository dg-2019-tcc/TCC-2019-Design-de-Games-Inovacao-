using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaFutebol : MonoBehaviour
{
    private SpriteRenderer bolaSprite;

    public bool normal;
    public bool kick;
    public bool superKick;

    public float bolaTimer;

    // Start is called before the first frame update  
    void Start()
    {
        bolaSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame  
    void Update()
    {
        if (normal)
        {
            gameObject.GetComponent<PhotonView>().RPC("BolaAzul", RpcTarget.MasterClient);
        }

        else if (kick)
        {
            gameObject.GetComponent<PhotonView>().RPC("BolaAmarela", RpcTarget.MasterClient);
        }

        else if (superKick)
        {
            gameObject.GetComponent<PhotonView>().RPC("BolaVermelha", RpcTarget.MasterClient);
        }

        else
        {
            gameObject.GetComponent<PhotonView>().RPC("BolaBranca", RpcTarget.MasterClient);
        }

        if (bolaTimer >= 3f)
        {
            gameObject.GetComponent<PhotonView>().RPC("BolaBranca", RpcTarget.MasterClient);
        }
    }

    [PunRPC]
    void BolaBranca()
    {
        normal = false;
        kick = false;
        superKick = false;
        bolaTimer = 0f;
        bolaSprite.color = Color.white;
    }

    [PunRPC]
    void BolaAzul()
    {
        bolaSprite.color = Color.blue;

        bolaTimer += Time.deltaTime;
    }

    [PunRPC]
    void BolaAmarela()
    {
        bolaSprite.color = Color.yellow;

        normal = false;

        bolaTimer += Time.deltaTime;
    }

    [PunRPC]
    void BolaVermelha()
    {
        bolaSprite.color = Color.red;

        normal = false;
        kick = false;

        bolaTimer += Time.deltaTime;
    }
}
