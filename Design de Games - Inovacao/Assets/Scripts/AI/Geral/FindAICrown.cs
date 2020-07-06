using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindAICrown : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		GameObject.FindObjectOfType<Coroa>().ganhador = transform;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
