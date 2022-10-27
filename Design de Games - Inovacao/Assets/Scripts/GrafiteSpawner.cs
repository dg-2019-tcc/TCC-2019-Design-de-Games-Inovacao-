using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrafiteSpawner : MonoBehaviour
{

	public GameObject[] grafites;

	private int index;

	public float delay;
    // Start is called before the first frame update
    void Start()
    {
		index = 0;
		StartCoroutine(Spawn());
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	private IEnumerator Spawn()
	{
		if (!grafites[grafites.Length - 1].activeSelf)
		{

			grafites[index].SetActive(true);
			index++;

			yield return new WaitForSeconds(delay);
			StartCoroutine(Spawn());
		}
		else
		{
			yield return new WaitForSeconds(0);
		}

	}
}
