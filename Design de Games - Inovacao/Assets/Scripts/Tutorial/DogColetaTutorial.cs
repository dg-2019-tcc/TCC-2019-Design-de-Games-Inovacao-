using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Complete;

public class DogColetaTutorial : MonoBehaviour
{
	//	public GameSetupController gsp;
		public BoolVariable dogSpawn;

	private PhotonView pv;
	public GameObject coletavel;
    public DogController dogController;

	public bool dog;
	private bool araki = true;

    #region Unity Function

    void Start()
    {
        if (dog)
        {
            pv = GetComponent<PhotonView>();

            dogSpawn.Value = false;
        }
        coletavel.SetActive(false);
    }

    #endregion

    #region Public Functions

    public void AtivaDog()
    {
        if (dog)
        {
            dogController = FindObjectOfType<DogController>();
            dogController.sequestrado = false;
            GameManager.sequestrado = false;
            dogController.ChangeState("IdleState");
        }
        coletavel.SetActive(true);
        Destroy(gameObject);
    }

    #endregion

    #region Private Functions

    #endregion
}
