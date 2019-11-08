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
    public float effectTime;



    public override IEnumerator Enumerator(MonoBehaviour runner)
    {
        stat.speed = statChange;
        ativa.Value = true;
        yield return new WaitForSeconds(effectTime);
        stat.speed = playerSpeed;
        ativa.Value = false;
    }
}
