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

    #region Unity Function

    void Start()
    {
        particleOneShot = GetComponent<ParticleSystem>();
        p2Danim = GetComponentInParent<Player2DAnimations>();
    }

    #endregion

    #region Public Functions

    public void PlayLand()
    {
        particleOneShot.Play();
    }

    public void CarPaticles()
    {
        var main = particleLoop.main;
        main.startSize = 0.75f;
        main.startLifetime = 0.4f;
        particleLoop.Play();
    }

    public void WalkParticles()
    {
        var main = particleLoop.main;
        main.startSize = 0.3f;
        main.startLifetime = 0.15f;
        particleLoop.Play();
    }

    #endregion

    #region Private Functions

    #endregion

}
