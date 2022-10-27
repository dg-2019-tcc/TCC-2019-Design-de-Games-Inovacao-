using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : MonoBehaviour
{
    public bool faloComTV;
    public bool precisaFalar;

    public ItemLocatorOnScreen pointer;

    public GameObject[] falas;

    public GameFlowController flowController;

    #region Unity Function

    void Start()
    {
        pointer = GetComponent<ItemLocatorOnScreen>();

        if (GameManager.historiaMode)
        {
            Debug.Log(PlayerPrefsManager.Instance.prefsVariables.falasIndex);
            if (CheckPointController.nextFalaIndex != 0)
            {
                CoisasAtivas(CheckPointController.nextFalaIndex, true);
            }
        }


        faloComTV = false;
    }
    #endregion

    #region Public Functions

    public void CoisasAtivas(int index, bool ativar)
    {
        if (!ativar)
        {
            falas[index].SetActive(ativar);
        }
        else
        {
            falas[CheckPointController.nextFalaIndex].SetActive(true);
        }
        pointer.enabled = ativar;
        precisaFalar = ativar;
    }

    public void FalouComTV()
    {
        pointer.enabled = false;
        precisaFalar = false;
        faloComTV = true;
        flowController.FlowHUB();
    }

    #endregion

    #region Private Functions

    #endregion

}
