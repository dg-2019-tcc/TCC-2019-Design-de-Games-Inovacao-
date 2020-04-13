using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaFutebol : MonoBehaviour
{
    private SpriteRenderer bolaSprite;

    public bool normal;
    public bool kick;
    public bool superKick;

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
        }

        else if (kick)
        {
            bolaSprite.color = Color.yellow;
        }

        else if (superKick)
        {
            bolaSprite.color = Color.red;
        }
    }
    

}
