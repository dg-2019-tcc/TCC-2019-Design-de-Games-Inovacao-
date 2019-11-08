using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="Gerador/Skill")]
public class Skill : ScriptableObject
{
    public PlayerStat stat;
    public FloatVariable statChange;
}
