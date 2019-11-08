using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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
        runner.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        runner.GetComponent<Rigidbody2D>().gravityScale = 0.3f;

        ativa.Value = true;
        stat.speed = pipaSpeed;
		runner.GetComponent<PhotonView>().Owner.CustomProperties["dogValue"] = false;
        for (int i = 0; i < effectVar; i++)
        {

            runner.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, pipaForce.Value), ForceMode2D.Impulse);
            yield return new WaitForSeconds(effectTime);
        }
		runner.GetComponent<PhotonView>().Owner.CustomProperties["dogValue"] = true;
        stat.speed = playerSpeed;
        ativa.Value = false;

        runner.GetComponent<Rigidbody2D>().gravityScale = 0.7f;

        
    }
}
