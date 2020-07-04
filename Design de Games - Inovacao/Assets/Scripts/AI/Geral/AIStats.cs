using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/AI/AIStats")]
public class AIStats : ScriptableObject
{
    public float moveSpeed = 1f;
    public float jumpForce = 5f;
    public float kickForceX = 5f;
    public float kickForceY = 5f;
}
