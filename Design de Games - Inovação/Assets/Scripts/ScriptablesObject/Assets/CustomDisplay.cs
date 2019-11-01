using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomDisplay : MonoBehaviour
{
    public PropsCustom leg;
    public PropsCustom shirt;
    public PropsCustom hair;

    public GameObject[] hairModels;

    public GameObject[] shirtModels;

    public GameObject[] legModels;

    public SkinnedMeshRenderer[] hairColor;

    public SkinnedMeshRenderer[] legsColor;

    public SkinnedMeshRenderer[] shirtsColor;

    private void Awake()
    {
        if (hair.propIndex == 0)
        {
            hairModels[hair.propIndex].SetActive(false);
        }
        else
        {
            hairModels[hair.propIndex].SetActive(true);
        }
        shirtModels[shirt.propIndex].SetActive(true);
        legModels[leg.propIndex].SetActive(true);

        hairColor[hair.propIndex].material = hair.color[0].corData[hair.colorIndex];
        shirtsColor[shirt.propIndex].material = shirt.color[shirt.propIndex].corData[shirt.colorIndex];
        legsColor[leg.propIndex].material = leg.color[leg.propIndex].corData[leg.colorIndex];
    }
}
