using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;


public class VoleiManager : MonoBehaviour
{
    PhotonView playerView;

    public int index;

    public float maxPoints;

    public GameObject bola;

    public Transform bolaSpawnPoint;
    public FloatVariable botScore;

    public TextMeshProUGUI placarText;

    private void Update()
    {
        if (botScore.Value >= maxPoints)
        {
            PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 0;
            LevelManager.Instance.GoPodium();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Volei"))
        {
            bola = other.gameObject;

            GolSelect playerGol = GetComponentInParent<GolSelect>();
            index++;
            playerGol.jogador.PV.Owner.AddScore(1);

            Debug.Log(index);

            RecomecaVolei();
            placarText.text = playerGol.jogador.PV.Owner.GetScore().ToString();

            if (playerGol.jogador.PV.Owner.GetScore() >= 7)
            {
                PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 1;
                LevelManager.Instance.GoPodium();
            }

        }

    }

    [PunRPC]
    public void RecomecaVolei()
    {
        StartCoroutine("ResetaBola");
    }

    IEnumerator ResetaBola()
    {
        bola.SetActive(false);

        //bola.GetComponent<BolaFutebol>().bolaTimer += 5f;

        bola.GetComponent<Rigidbody2D>().isKinematic = true;

        bola.transform.position = bolaSpawnPoint.position;

        index++;

        yield return new WaitForSeconds(3f);
        bola.SetActive(true);

        bola.GetComponent<Rigidbody2D>().isKinematic = false;

    }
}
