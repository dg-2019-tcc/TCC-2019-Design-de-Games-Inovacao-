using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    public FloatVariable flowIndex;
    private float oldIndex;
    public GameObject[] fase;

    void Start()
    {
        if(flowIndex.Value != oldIndex)
        {
            if(flowIndex.Value == 1)
            {
                Fase01();
            }

            else if (flowIndex.Value == 2)
            {
                Fase01();
                Fase02();
            }
            else if (flowIndex.Value == 3)
            {
                Fase01();
                Fase02();
                Fase03();
            }
            else if (flowIndex.Value == 4)
            {
                Fase01();
                Fase02();
                Fase03();
                Fase04();
            }
        }
    }

    public void Fase01()
    {
        fase[1].SetActive(true);
    }

    public void Fase02()
    {
        fase[2].SetActive(true);
    }

    public void Fase03()
    {
        fase[3].SetActive(true);
    }

    public void Fase04()
    {
        fase[4].SetActive(true);
    }

    private void LateUpdate()
    {
        oldIndex = flowIndex.Value;
    }

}
