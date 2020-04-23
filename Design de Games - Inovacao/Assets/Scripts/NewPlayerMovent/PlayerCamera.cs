using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [HideInInspector]
    public PhotonView PV;

    [Header("Camera Manager")]
    public CameraManager cameraManager;

    private bool menuCustom;


    void Start()
    {
        PV = GetComponent<PhotonView>();

        menuCustom = false;

        if (SceneManager.GetActiveScene().name == "HUB" || !PhotonNetwork.InRoom)
        {
            menuCustom = true;
        }

        if (PV.IsMine)
        {
            cameraManager.SendMessage("ActivateCamera", true);
            //	rb2d.gravityScale = 0.7f;
        }
        else if (menuCustom)
        {
            cameraManager.SendMessage("ActivateCamera", true);
            //	rb2d.gravityScale = 0.7f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
