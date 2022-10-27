using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PlacarBot : MonoBehaviour
{

    public TextMeshProUGUI placarText;
    public FloatVariable botScore;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        placarText.text = botScore.Value.ToString();
    }
}
