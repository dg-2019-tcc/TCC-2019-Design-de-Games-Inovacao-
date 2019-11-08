using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Gerador/Tokens/PipaEffect")]
public class PipaEffect : TokenEffect
{
    public PlayerStat stat;
    public FloatVariable playerSpeed;
    public FloatVariable pipaSpeed;
    public FloatVariable pipaForce;
    public BoolVariable ativa;
    public BoolVariable dog;
    public float effectTime;

    public override IEnumerator Enumerator(MonoBehaviour runner)
    {
        ativa.Value = true;
        runner.GetComponent<Rigidbody2D>().AddForce(new Vector3(0,0,0));
        runner.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, pipaForce.Value), ForceMode2D.Impulse);
        stat.speed = pipaSpeed;
        dog.Value = false;
        yield return new WaitForSeconds(effectTime);
        dog.Value = true;
        stat.speed = playerSpeed;
        ativa.Value = false;
    }
}
