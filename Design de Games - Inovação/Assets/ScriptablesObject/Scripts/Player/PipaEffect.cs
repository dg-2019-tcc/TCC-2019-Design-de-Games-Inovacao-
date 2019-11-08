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
    public float effectVar;

    public override IEnumerator Enumerator(MonoBehaviour runner)
    {
        runner.GetComponent<Rigidbody2D>().gravityScale = 0.3f;

        ativa.Value = true;
        stat.speed = pipaSpeed;
        dog.Value = false;

        for (int i = 0; i < effectVar; i++)
        {

            runner.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, pipaForce.Value), ForceMode2D.Impulse);
            yield return new WaitForSeconds(effectTime);
        }

        //yield return new WaitForSeconds(effectTime);


        dog.Value = true;
        stat.speed = playerSpeed;
        ativa.Value = false;

        runner.GetComponent<Rigidbody2D>().gravityScale = 0.7f;

        
    }
}
