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

    public GameObject dogSpawn;
    public GameObject dog;


    void Awake()
    {
        index = Mathf.RoundToInt(spawnHUBPoints.Value);
        player.transform.position = spawnPoints[index].position;
        dog.transform.position = dogSpawn.transform.position;
    }
}
