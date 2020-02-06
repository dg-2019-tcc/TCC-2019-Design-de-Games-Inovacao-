using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="Gerador/Player")]
public class PlayerStat : ScriptableObject
{
    public FloatVariable speed;
    public FloatVariable jumpForce;
}
