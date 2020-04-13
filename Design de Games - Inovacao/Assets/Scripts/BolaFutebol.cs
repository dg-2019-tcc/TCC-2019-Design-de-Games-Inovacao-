using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaFutebol : MonoBehaviour
{
    private SpriteRenderer bolaSprite;

    public bool normal;
    public bool kick;
    public bool superKick;

    public float bolaTimer;

    // Start is called before the first frame update
    void Start()
    {
        bolaSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (normal)
        {
            bolaSprite.color = Color.blue;

            bolaTimer += Time.deltaTime;
        }

        else if (kick)
        {
            bolaSprite.color = Color.yellow;

            normal = false;

            bolaTimer += Time.deltaTime;
        }

        else if (superKick)
        {
            bolaSprite.color = Color.red;

            normal = false;
            kick = false;

            bolaTimer += Time.deltaTime;
        }

        else
        {
            bolaTimer = 0f;
            bolaSprite.color = Color.white;
        }

        if(bolaTimer >= 3f)
        {
            normal = false;
            kick = false;
            superKick = false;
        }
    }
    

}
