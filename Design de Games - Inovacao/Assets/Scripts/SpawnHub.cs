using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHub : MonoBehaviour
{
    public Transform[] spawnPoints;
    public FloatVariable spawnHUBPoints;
    private float indexFloat;
    private int index;

    public GameObject player;


    void Awake()
    {
        index = Mathf.RoundToInt(spawnHUBPoints.Value);
        player.transform.position = spawnPoints[index].position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
