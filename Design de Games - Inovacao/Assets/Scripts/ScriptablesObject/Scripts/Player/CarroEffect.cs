using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Gerador/Tokens/CarroEffect")]
public class CarroEffect : TokenEffect
{
    public PlayerStat stat;
    public FloatVariable playerSpeed;
    public FloatVariable statChange;
    public BoolVariable ativa;
    public BoolVariable dog;
    public float effectTime;



    public override IEnumerator Enumerator(MonoBehaviour runner)
    {
        stat.speed = statChange;
        ativa.Value = true;
        dog.Value = false;
        yield return new WaitForSeconds(effectTime);
        stat.speed = playerSpeed;
        ativa.Value = false;
        dog.Value = true;
    }
}
