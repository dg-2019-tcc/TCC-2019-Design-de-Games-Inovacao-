using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrucaoFirstTime : MonoBehaviour
{
    public BoolVariable dogAtivo;
    public GameObject textoInstrucao;
    public FloatVariable flowIndex;

    private void Start()
    {
        //acabou01 = Resources.Load<BoolVariable>("Acabou01");
        flowIndex = Resources.Load<FloatVariable>("FlowIndex");
    }

    void Update()
    {
        if (flowIndex.Value > 1)
        {
            Destroy(textoInstrucao);
            this.enabled = false;
        }

        else
        {
            if (dogAtivo.Value == false)
            {
                Ensina();
            }
            else
            {
                textoInstrucao.SetActive(false);
            }
        }
    }

    void Ensina()
    {

            textoInstrucao.SetActive(true);

    }
}
