using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TokenEffect : ScriptableObject
{
    public abstract IEnumerator Enumerator(MonoBehaviour runner);
}
