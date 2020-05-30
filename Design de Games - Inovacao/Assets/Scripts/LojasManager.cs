using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LojasManager : MonoBehaviour
{
    public int nextIndex;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Fase", nextIndex);
    }

}
