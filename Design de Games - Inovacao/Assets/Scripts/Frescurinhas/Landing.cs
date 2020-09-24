using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landing : MonoBehaviour
{
	private Player2DAnimations p2Danim;
	private string oldAnimState;
	private ParticleSystem particleOneShot;
	public ParticleSystem particleLoop;
    public PlayerAnimationsDB animController;
    // Start is called before the first frame update
    void Start()
    {
        particleOneShot = GetComponent<ParticleSystem>();
		p2Danim = GetComponentInParent<Player2DAnimations>();
    }

    public void PlayLand()
    {
        particleOneShot.Play();
    }


    // Update is called once per frame
    /*void Update()
    {
        if(animController.stateMovement ==AnimStateMovement.Aterrisando )
        {
            particle.Play();
        }

    }*/
}
