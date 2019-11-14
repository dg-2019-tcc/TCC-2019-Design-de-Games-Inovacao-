using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragaoOrganizer : MonoBehaviour
{

    [Header("Array")]

    [SerializeField]
    private Transform[] nodes;



    [Header("Variaveis")]
    [SerializeField]
    private float posicao;
    [SerializeField]
    private float angulo;

    private void Start()
    {
        for (int i = 0; i < nodes.Length; i++)
        {
            posicao = angulo * i;
            nodes[i].eulerAngles = new Vector3(0, 0, posicao);
            Debug.Log(posicao);
        }
    }





}
