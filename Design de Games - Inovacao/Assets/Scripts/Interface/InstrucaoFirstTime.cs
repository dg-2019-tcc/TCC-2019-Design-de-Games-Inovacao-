using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrucaoFirstTime : MonoBehaviour
{
    public BoolVariable dogAtivo;
    public GameObject textoInstrucao;
    public FloatVariable flowIndex;
    public BoolVariableArray acabou01;

    private void Start()
    {
        acabou01 = Resources.Load<BoolVariableArray>("Acabou01");
        flowIndex = Resources.Load<FloatVariable>("FlowIndex");
    }

    void Update()
    {
        if (acabou01.Value[1] == true)
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
                if (textoInstrucao != null)
                {
                    textoInstrucao.SetActive(false);
                }
            }
        }
    }

    void Ensina()
    {

            textoInstrucao.SetActive(true);

    }
}
