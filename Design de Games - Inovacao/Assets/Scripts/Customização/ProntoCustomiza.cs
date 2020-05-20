using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProntoCustomiza : MonoBehaviour
{
    [SerializeField]
    private string nomeDoMenu;


    public void ComecaJogo()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Start", GetComponent<Transform>().position);
        SceneManager.LoadScene(nomeDoMenu);
    }
}
