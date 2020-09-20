using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landing : MonoBehaviour
{
	private Player2DAnimations p2Danim;
	private string oldAnimState;
	private ParticleSystem particle;
    public PlayerAnimController animController;
    // Start is called before the first frame update
    void Start()
    {
		particle = GetComponent<ParticleSystem>();
		p2Danim = GetComponentInParent<Player2DAnimations>();
    }

    // Update is called once per frame
    void Update()
    {
        if(animController.nextAnimState04 == AnimStateMovement.Aterrisando || animController.nextAnimState04 == AnimStateMovement.Walk)
        {
            particle.Play();
        }

    }
}
