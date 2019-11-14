using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragaoManager : MonoBehaviour
{

    [Header("Prefab")]

    [SerializeField]
    private GameObject nodeDoDragao;
    private GameObject nomeDoNodeDoDragao;
    [SerializeField]
    private Transform parentDragao;



    [Header("Variaveis")]

    private float numeroDeNodes;
    private float posicao;
    [SerializeField]
    private float angulo;
    [SerializeField]
    private float velocidadeDragao;





    private void Start()
    {
        InstantiateDragao();
    }



    private void Update()
    {
        MoveDragao();
    }



    void InstantiateDragao()
    {
        numeroDeNodes = 360 / angulo;
        for (int i = 0; i < numeroDeNodes; i++)
        {
            posicao = angulo * i;
            nomeDoNodeDoDragao = Instantiate(nodeDoDragao, transform.position, Quaternion.Euler(0, 0, posicao), parentDragao);
            if (i <= 9)
            {
                nomeDoNodeDoDragao.name = "Node_00" + i;
            }
            else
            {
                nomeDoNodeDoDragao.name = "Node_0" + i;
            }
        }
    }
    



    void MoveDragao()
    {
        parentDragao.Rotate(0, 0, velocidadeDragao);
    }
}
