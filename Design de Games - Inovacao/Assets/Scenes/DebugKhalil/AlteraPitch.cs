using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlteraPitch : MonoBehaviour
{
    public AudioSource audio;

    private void Update()
    {
        audio.pitch *= 3;
    }

}
