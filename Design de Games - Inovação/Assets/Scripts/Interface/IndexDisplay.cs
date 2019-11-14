using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndexDisplay : MonoBehaviour
{
    public PropsCustom hair;
    public PropsCustom shirt;
    public PropsCustom legs;

    public Text[] indexText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        indexText[0].text = (hair.propIndex + 1) + "/4";
        indexText[1].text = (hair.colorIndex + 1) + "/10";
        indexText[2].text = (shirt.propIndex + 1) + "/3";
        indexText[3].text = (shirt.colorIndex + 1) + "/10";
        indexText[4].text = (legs.propIndex + 1) + "/3";
        indexText[5].text = (legs.colorIndex + 1) + "/10";
    }
}
