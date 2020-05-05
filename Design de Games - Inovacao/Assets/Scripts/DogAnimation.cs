using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class DogAnimation : MonoBehaviour
{
    public string idlePose = "0_Idle";
    [SerializeField]
    private UnityArmatureComponent dog;

    public BoolVariable dogAtivo;
    public BoolVariable carroActive;
    public BoolVariable pipaActive;

    public enum State { Idle, Pipa, Carrinho, Moto, Aviao}


    // Update is called once per frame
    void Update()
    {
        
    }

    public void Idle()
    {

    }

    public void Pipa()
    {

    }

    public void Carrinho()
    {

    }


}
