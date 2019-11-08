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

    public override IEnumerator Enumerator(MonoBehaviour runner)
    {
        ativa.Value = true;
        runner.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, pipaForce.Value), ForceMode2D.Impulse);
        stat.speed = pipaSpeed;
		runner.GetComponent<PhotonView>().Owner.CustomProperties["dogValue"] = false;
        yield return new WaitForSeconds(effectTime);
		runner.GetComponent<PhotonView>().Owner.CustomProperties["dogValue"] = true;
        stat.speed = playerSpeed;
        ativa.Value = false;
    }
}
