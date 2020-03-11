using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogColetaTutorial : MonoBehaviour
{
	public GameSetupController gsp;
	public BoolVariable dogSpawn;
	public GameObject coletavel;
    // Start is called before the first frame update
    void Start()
    {
		// dogSpawn = gsp.PlayerInst.GetComponent<PhotonPlayer>().myAvatar.GetComponent<DogController>().Pet;
		dogSpawn.Value = false;
		coletavel.SetActive(false);
    }

    // Update is called once per frame
   /* void Update()
    {
        
    }*/


	private void OnTriggerEnter2D(Collider2D collision)
	{
		dogSpawn.Value = true;
		coletavel.SetActive(true);
	}
}
